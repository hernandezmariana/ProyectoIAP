using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using ExplosionesIncrementales.Modelo;

namespace ExplosionesIncrementales.Servicios
{
    [Serializable] public class TablaTraduccion { public string[] claves; public string[] valores; }
    [Serializable] public class DatosMyMemory { public string translatedText; }
    [Serializable] public class RespuestaMyMemory { public DatosMyMemory responseData; }

    // Envuelve a LocalizacionLocal: es/en funcionan sin internet; los idiomas "de API" se traducen
    // desde el español, se guardan en caché y, si algo falla, se ve el texto local.
    public class LocalizacionConApi : IServicioLocalizacion
    {
        private const string UrlBase = "https://api.mymemory.translated.net/get";
        private const float TimeoutSeg = 6f;
        private const string PrefijoCache = "trad_v1_";   // subir el número si agregás textos nuevos
        private static readonly string Email = "";        // opcional: con un mail, MyMemory sube el límite diario

        private static readonly Dictionary<string, string> Nombres =
            new Dictionary<string, string> { { "pt", "Português" } };

        private readonly LocalizacionLocal _local;
        private readonly ClienteHttp _http;
        private readonly List<string> _idiomas = new List<string>();
        private readonly HashSet<string> _idiomasApi = new HashSet<string>();
        private readonly Dictionary<string, Dictionary<string, string>> _traducidos =
            new Dictionary<string, Dictionary<string, string>>();

        private string _idiomaApiActual;
        private string _idiomaEnProceso;
        private bool _traduciendo;

        public event Action AlCambiarIdioma;
        public string IdiomaActual { get { return _idiomaApiActual ?? _local.IdiomaActual; } }
        public IReadOnlyList<string> Idiomas { get { return _idiomas; } }

        public LocalizacionConApi(LocalizacionLocal local, ClienteHttp http, IEnumerable<string> idiomasApi)
        {
            _local = local;
            _http = http;
            foreach (var i in _local.Idiomas) _idiomas.Add(i);
            foreach (var i in idiomasApi) { _idiomasApi.Add(i); _idiomas.Add(i); }
        }

        public string Texto(string clave)
        {
            if (_idiomaEnProceso != null && clave == "menu.idioma")
                return "Idioma: " + NombreDe(_idiomaEnProceso) + " ...";

            if (_idiomaApiActual != null)
            {
                if (clave == "menu.idioma") return "Idioma: " + NombreDe(_idiomaApiActual);
                string valor;
                if (_traducidos[_idiomaApiActual].TryGetValue(clave, out valor)) return valor;
            }
            return _local.Texto(clave);   // si falta la traducción, se ve el texto local
        }

        public string Texto(string clave, params object[] argumentos)
        {
            try { return string.Format(Texto(clave), argumentos); }
            catch (FormatException) { return Texto(clave); }
        }

        public void CambiarIdioma(string codigo)
        {
            if (_traduciendo) return;

            if (!_idiomasApi.Contains(codigo))
            {
                _idiomaApiActual = null;
                _local.CambiarIdioma(codigo);
                Avisar();
                return;
            }
            if (codigo == _idiomaApiActual) return;

            Dictionary<string, string> tabla;
            if (!_traducidos.TryGetValue(codigo, out tabla))
            {
                tabla = CargarCache(codigo);
                if (tabla != null) _traducidos[codigo] = tabla;
            }
            if (tabla != null) { Activar(codigo); return; }

            _traduciendo = true;
            _idiomaEnProceso = codigo;
            Avisar();   // el menú muestra "Idioma: Português ..." mientras traduce
            _http.StartCoroutine(Traducir(codigo));
        }

        private IEnumerator Traducir(string codigo)
        {
            var resultado = new Dictionary<string, string>();
            int pendientes = 0;
            bool fallo = false;

            foreach (var par in _local.TextosDe("es"))
            {
                if (par.Key == "menu.idioma") continue;   // el nombre del idioma no se traduce
                pendientes++;
                string clave = par.Key;
                string origen = par.Value;

                _http.StartCoroutine(_http.Get(ArmarUrl(origen, codigo), TimeoutSeg, json =>
                {
                    string traducido = ExtraerTraduccion(json);
                    if (traducido == null) fallo = true;
                    else if (PlaceholdersIntactos(origen, traducido)) resultado[clave] = traducido;
                    pendientes--;
                }));
            }

            while (pendientes > 0) yield return null;

            _traduciendo = false;
            _idiomaEnProceso = null;

            if (fallo || resultado.Count == 0)
            {
                Debug.LogWarning("Traducción no disponible: se usa el idioma local.");
                _idiomaApiActual = null;
                _local.CambiarIdioma("es");
                Avisar();
                yield break;
            }

            Guardar(codigo, resultado);
            _traducidos[codigo] = resultado;
            Activar(codigo);
        }

        private void Activar(string codigo)
        {
            _local.CambiarIdioma("es");   // base de respaldo: lo que falte se ve en español
            _idiomaApiActual = codigo;
            Avisar();
        }

        private void Avisar() { if (AlCambiarIdioma != null) AlCambiarIdioma(); }

        private static string NombreDe(string codigo)
        {
            string nombre;
            return Nombres.TryGetValue(codigo, out nombre) ? nombre : codigo;
        }

        private static string ArmarUrl(string texto, string destino)
        {
            string url = UrlBase + "?q=" + UnityWebRequest.EscapeURL(texto)
                + "&langpair=" + UnityWebRequest.EscapeURL("es|" + destino);
            if (!string.IsNullOrEmpty(Email)) url += "&de=" + UnityWebRequest.EscapeURL(Email);
            return url;
        }

        private static string ExtraerTraduccion(string json)
        {
            if (string.IsNullOrEmpty(json)) return null;
            try
            {
                var r = JsonUtility.FromJson<RespuestaMyMemory>(json);
                if (r == null || r.responseData == null) return null;
                string t = r.responseData.translatedText;
                if (string.IsNullOrEmpty(t)) return null;

                t = System.Net.WebUtility.HtmlDecode(t);
                string mayus = t.ToUpperInvariant();
                // Mensajes de error/límite que MyMemory devuelve como si fueran traducciones
                if (mayus.Contains("MYMEMORY WARNING") || mayus.Contains("QUERY LENGTH LIMIT")) return null;
                return t;
            }
            catch (Exception) { return null; }
        }

        // Una traducción que rompe {0}, {1}... se descarta (ese texto queda en español)
        private static bool PlaceholdersIntactos(string origen, string traducido)
        {
            if (Contar(origen, '{') != Contar(traducido, '{')) return false;
            if (Contar(origen, '}') != Contar(traducido, '}')) return false;
            for (int n = 0; n < 6; n++)
            {
                string marca = "{" + n + "}";
                if (origen.Contains(marca) && !traducido.Contains(marca)) return false;
            }
            return true;
        }

        private static int Contar(string texto, char c)
        {
            int total = 0;
            foreach (char x in texto) if (x == c) total++;
            return total;
        }

        // ---------- Caché en PlayerPrefs ----------
        private void Guardar(string codigo, Dictionary<string, string> tabla)
        {
            var datos = new TablaTraduccion
            {
                claves = new string[tabla.Count],
                valores = new string[tabla.Count]
            };
            int i = 0;
            foreach (var par in tabla) { datos.claves[i] = par.Key; datos.valores[i] = par.Value; i++; }
            PlayerPrefs.SetString(PrefijoCache + codigo, JsonUtility.ToJson(datos));
            PlayerPrefs.Save();
        }

        private Dictionary<string, string> CargarCache(string codigo)
        {
            string json = PlayerPrefs.GetString(PrefijoCache + codigo, "");
            if (string.IsNullOrEmpty(json)) return null;
            try
            {
                var datos = JsonUtility.FromJson<TablaTraduccion>(json);
                if (datos == null || datos.claves == null || datos.valores == null
                    || datos.claves.Length == 0 || datos.claves.Length != datos.valores.Length) return null;

                var tabla = new Dictionary<string, string>();
                for (int i = 0; i < datos.claves.Length; i++) tabla[datos.claves[i]] = datos.valores[i];
                return tabla;
            }
            catch (Exception) { return null; }
        }
    }
}