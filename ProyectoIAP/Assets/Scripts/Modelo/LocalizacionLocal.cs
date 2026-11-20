/*using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExplosionesIncrementales.Modelo
{
    public class LocalizacionLocal : IServicioLocalizacion
    {
        private const string ClaveIdioma = "idioma";
        private readonly string[] _idiomas = { "es", "en" };

        private readonly Dictionary<string, Dictionary<string, string>> _textos =
            new Dictionary<string, Dictionary<string, string>>
        {
            { "es", new Dictionary<string, string> {
                { "menu.titulo", "EXPLOSIONES INCREMENTALES" },
                { "menu.nueva", "Nueva partida" },
                { "menu.continuar", "Continuar" },
                { "menu.idioma", "Idioma: Español" } } },
            { "en", new Dictionary<string, string> {
                { "menu.titulo", "INCREMENTAL EXPLOSIONS" },
                { "menu.nueva", "New game" },
                { "menu.continuar", "Continue" },
                { "menu.idioma", "Language: English" } } }
        };

        public string IdiomaActual { get; private set; }
        public IReadOnlyList<string> Idiomas { get { return _idiomas; } }
        public event Action AlCambiarIdioma;

        public LocalizacionLocal()
        {
            string guardado = PlayerPrefs.GetString(ClaveIdioma, "es");
            IdiomaActual = _textos.ContainsKey(guardado) ? guardado : "es";
        }

        public string Texto(string clave)
        {
            Dictionary<string, string> tabla;
            string valor;
            if (_textos.TryGetValue(IdiomaActual, out tabla) && tabla.TryGetValue(clave, out valor))
                return valor;
            return clave;   // nunca rompe la UI: muestra la clave si falta la traducción
        }

        //para el idioma 
        public string Texto(string clave, params object[] argumentos)
        {
            return string.Format(Texto(clave), argumentos);
        }

        public void CambiarIdioma(string codigo)
        {
            if (!_textos.ContainsKey(codigo) || codigo == IdiomaActual) return;
            IdiomaActual = codigo;
            PlayerPrefs.SetString(ClaveIdioma, codigo);
            PlayerPrefs.Save();
            if (AlCambiarIdioma != null) AlCambiarIdioma();
        }
    }
}*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExplosionesIncrementales.Modelo
{
    public class LocalizacionLocal : IServicioLocalizacion
    {
        private const string ClaveIdioma = "idioma";
        private readonly string[] _idiomas = { "es", "en" };

        private readonly Dictionary<string, Dictionary<string, string>> _textos =
            new Dictionary<string, Dictionary<string, string>>
        {
            { "es", new Dictionary<string, string> {
                { "menu.titulo", "EXPLOSIONES INCREMENTALES" },
                { "menu.nueva", "Nueva partida" },
                { "menu.continuar", "Continuar" },
                { "menu.idioma", "Idioma: Español" },

                { "trivia.decision", "TRIVIA\nGanaste ${0} esta ronda.\n\nSi respondés bien: ${1}\nSi fallás: ${2}\n\n¿Querés arriesgar?" },
                { "trivia.pregunta", "En juego: ${0}\n\n{1}" },
                { "trivia.correcta", "¡CORRECTA!" },
                { "trivia.incorrecta", "INCORRECTA" },
                { "trivia.noarriesgo", "No arriesgaste" },
                { "trivia.resultado", "{0}\n\nGanancias de la ronda: ${1} → ${2}" },
                { "trivia.arriesgar", "Arriesgar" },
                { "trivia.conservar", "Conservar" },
                { "comun.continuar", "Continuar" },
                { "tienda.info", "TIENDA - Ronda {0}\nDinero: ${1}\nPróximo impuesto: ${2} (en {3} rondas)" },
                { "mejora.Radio", "Radio" },
                { "mejora.Tiempo", "Tiempo" },
                { "mejora.Velocidad", "Velocidad" },
                { "mejora.Dano", "Daño" },
                { "mejora.Suerte", "Suerte" },
                { "mejora.fila", "{0} (Nv {1})  -  ${2}" },
                { "impuesto.info", "IMPUESTO - Ronda {0}\nSe cobra: ${1}\nTu dinero: ${2}" },
                { "impuesto.pagar", "Pagar" },
                { "fin.victoria", "¡VICTORIA!\nPagaste el impuesto de la ronda {0}" },
                { "fin.gameover", "GAME OVER\nBancarrota en la ronda {0}" },
                { "fin.reiniciar", "Jugar de nuevo" },
                { "trivia.cargando", "Buscando una pregunta..." },
                { "hud.ronda", "RONDA {0}   T: {1}   ${2}" } } },

            { "en", new Dictionary<string, string> {
                { "menu.titulo", "INCREMENTAL EXPLOSIONS" },
                { "menu.nueva", "New game" },
                { "menu.continuar", "Continue" },
                { "menu.idioma", "Language: English" },

                { "trivia.decision", "TRIVIA\nYou earned ${0} this round.\n\nIf you answer right: ${1}\nIf you fail: ${2}\n\nDo you want to risk it?" },
                { "trivia.pregunta", "At stake: ${0}\n\n{1}" },
                { "trivia.correcta", "CORRECT!" },
                { "trivia.incorrecta", "WRONG" },
                { "trivia.noarriesgo", "You played it safe" },
                { "trivia.resultado", "{0}\n\nRound earnings: ${1} → ${2}" },
                { "trivia.arriesgar", "Risk it" },
                { "trivia.conservar", "Keep it" },
                { "comun.continuar", "Continue" },
                { "tienda.info", "SHOP - Round {0}\nMoney: ${1}\nNext tax: ${2} (in {3} rounds)" },
                { "mejora.Radio", "Radius" },
                { "mejora.Tiempo", "Time" },
                { "mejora.Velocidad", "Speed" },
                { "mejora.Dano", "Damage" },
                { "mejora.Suerte", "Luck" },
                { "mejora.fila", "{0} (Lv {1})  -  ${2}" },
                { "impuesto.info", "TAX - Round {0}\nDue: ${1}\nYour money: ${2}" },
                { "impuesto.pagar", "Pay" },
                { "fin.victoria", "VICTORY!\nYou paid the tax of round {0}" },
                { "fin.gameover", "GAME OVER\nBankrupt in round {0}" },
                { "fin.reiniciar", "Play again" },
                { "trivia.cargando", "Looking for a question..." },
                { "hud.ronda", "ROUND {0}   T: {1}   ${2}" } } }
        };

        public string IdiomaActual { get; private set; }
        public IReadOnlyList<string> Idiomas { get { return _idiomas; } }
        public event Action AlCambiarIdioma;

        public LocalizacionLocal()
        {
            string guardado = PlayerPrefs.GetString(ClaveIdioma, "es");
            IdiomaActual = _textos.ContainsKey(guardado) ? guardado : "es";
        }

        public string Texto(string clave)
        {
            Dictionary<string, string> tabla;
            string valor;
            if (_textos.TryGetValue(IdiomaActual, out tabla) && tabla.TryGetValue(clave, out valor))
                return valor;
            return clave;   // nunca rompe la UI: muestra la clave si falta la traducción
        }

        public string Texto(string clave, params object[] argumentos)
        {
            return string.Format(Texto(clave), argumentos);
        }

        public void CambiarIdioma(string codigo)
        {
            if (!_textos.ContainsKey(codigo) || codigo == IdiomaActual) return;
            IdiomaActual = codigo;
            PlayerPrefs.SetString(ClaveIdioma, codigo);
            PlayerPrefs.Save();
            if (AlCambiarIdioma != null) AlCambiarIdioma();
        }

        // Lo usa la localización con API para saber qué textos traducir
        public IEnumerable<KeyValuePair<string, string>> TextosDe(string codigo)
        {
            Dictionary<string, string> tabla;
            if (_textos.TryGetValue(codigo, out tabla)) return tabla;
            return new Dictionary<string, string>();
        }
    }
}