/*using System;
using System.Collections;
using UnityEngine;
using ExplosionesIncrementales.Modelo;

namespace ExplosionesIncrementales.Servicios
{
    [Serializable] public class PedidoTrivia { public int dificultad; public string idioma; }

    // Pide la pregunta al proxy (que tiene la clave). Devuelve null si algo sale mal.
    public class ProveedorTriviaIA : IProveedorTrivia
    {
        private const float TimeoutSeg = 6f;
        private readonly ClienteHttp _http;
        private readonly string _url;
        private readonly IServicioLocalizacion _loc;

        public ProveedorTriviaIA(ClienteHttp http, string url, IServicioLocalizacion loc)
        {
            _http = http; _url = url; _loc = loc;
        }

        public void ObtenerPregunta(int dificultad, Action<PreguntaTrivia> alTerminar)
        {
            _http.StartCoroutine(Pedir(dificultad, alTerminar));
        }

        private IEnumerator Pedir(int dificultad, Action<PreguntaTrivia> alTerminar)
        {
            string cuerpo = JsonUtility.ToJson(new PedidoTrivia
            {
                dificultad = Mathf.Clamp(dificultad, 1, 50),
                idioma = _loc.IdiomaActual
            });

            string respuesta = null;
            yield return _http.Post(_url, cuerpo, TimeoutSeg, r => respuesta = r);
            alTerminar(Validar(respuesta, dificultad));
        }

        // Nunca se acepta una pregunta mal formada
        private static PreguntaTrivia Validar(string json, int dificultad)
        {
            if (string.IsNullOrEmpty(json)) return null;
            try
            {
                var p = JsonUtility.FromJson<PreguntaTrivia>(json);
                if (p == null || string.IsNullOrWhiteSpace(p.enunciado)) return null;
                if (p.opciones == null || p.opciones.Length != 4) return null;
                foreach (var o in p.opciones) if (string.IsNullOrWhiteSpace(o)) return null;
                if (p.indiceCorrecto < 0 || p.indiceCorrecto > 3) return null;
                p.dificultad = dificultad;
                return p;
            }
            catch (Exception) { return null; }
        }
    }
}*/


using System;
using System.Collections;
using UnityEngine;
using ExplosionesIncrementales.Modelo;

namespace ExplosionesIncrementales.Servicios
{
    [Serializable] public class PedidoTrivia { public int dificultad; public string idioma; }

    // Pide la pregunta al proxy (que tiene la clave). Devuelve null si algo sale mal.
    public class ProveedorTriviaIA : IProveedorTrivia
    {
        private const float TimeoutSeg = 6f;
        private readonly ClienteHttp _http;
        private readonly string _url;
        private readonly IServicioLocalizacion _loc;

        public ProveedorTriviaIA(ClienteHttp http, string url, IServicioLocalizacion loc)
        {
            _http = http; _url = url; _loc = loc;
        }

        public void ObtenerPregunta(int dificultad, Action<PreguntaTrivia> alTerminar)
        {
            _http.StartCoroutine(Pedir(dificultad, alTerminar));
        }

        private IEnumerator Pedir(int dificultad, Action<PreguntaTrivia> alTerminar)
        {
            string cuerpo = JsonUtility.ToJson(new PedidoTrivia
            {
                dificultad = Mathf.Clamp(dificultad, 1, 50),
                idioma = _loc.IdiomaActual
            });

            Debug.Log("[Trivia IA] Pidiendo pregunta a " + _url + " con " + cuerpo);

            string respuesta = null;
            yield return _http.Post(_url, cuerpo, TimeoutSeg, r => respuesta = r);

            Debug.Log("[Trivia IA] Respuesta: " + (respuesta ?? "(ninguna)"));
            alTerminar(Validar(respuesta, dificultad));
        }

        // Nunca se acepta una pregunta mal formada
        private static PreguntaTrivia Validar(string json, int dificultad)
        {
            if (string.IsNullOrEmpty(json)) return null;
            try
            {
                var p = JsonUtility.FromJson<PreguntaTrivia>(json);
                if (p == null || string.IsNullOrWhiteSpace(p.enunciado)
                    || p.opciones == null || p.opciones.Length != 4
                    || p.indiceCorrecto < 0 || p.indiceCorrecto > 3)
                {
                    Debug.LogWarning("[Trivia IA] Formato inválido: " + json);
                    return null;
                }
                foreach (var o in p.opciones)
                {
                    if (string.IsNullOrWhiteSpace(o))
                    {
                        Debug.LogWarning("[Trivia IA] Opción vacía: " + json);
                        return null;
                    }
                }
                p.dificultad = dificultad;
                return p;
            }
            catch (Exception e)
            {
                Debug.LogWarning("[Trivia IA] No se pudo leer la respuesta (" + e.Message + "): " + json);
                return null;
            }
        }
    }
}