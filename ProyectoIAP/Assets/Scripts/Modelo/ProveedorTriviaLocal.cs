using System;
using System.Linq;
using UnityEngine;

namespace ExplosionesIncrementales.Modelo
{
    public class ProveedorTriviaLocal : IProveedorTrivia
    {
        private readonly PreguntaTrivia[] _preguntas;

        public ProveedorTriviaLocal()
        {
            var archivo = Resources.Load<TextAsset>("preguntas");
            _preguntas = archivo != null
                ? JsonUtility.FromJson<ListaPreguntas>(archivo.text).preguntas
                : new PreguntaTrivia[0];
        }

        public void ObtenerPregunta(int dificultad, Action<PreguntaTrivia> alTerminar)
        {
            if (_preguntas.Length == 0) { alTerminar(PreguntaDeEmergencia()); return; }

            // Toma las 3 preguntas más cercanas a la dificultad pedida y elige una al azar
            var cercanas = _preguntas
                .OrderBy(p => Mathf.Abs(p.dificultad - dificultad))
                .Take(3)
                .ToArray();

            alTerminar(cercanas[UnityEngine.Random.Range(0, cercanas.Length)]);
        }

        // Por si falta el archivo JSON: el juego nunca se bloquea
        private PreguntaTrivia PreguntaDeEmergencia()
        {
            return new PreguntaTrivia
            {
                enunciado = "¿Cuánto es 2 + 2?",
                opciones = new[] { "3", "4", "5", "6" },
                indiceCorrecto = 1,
                dificultad = 1
            };
        }
    }
}