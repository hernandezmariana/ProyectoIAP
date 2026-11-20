using System;
using UnityEngine;
using ExplosionesIncrementales.Modelo;

namespace ExplosionesIncrementales.Servicios
{
    // Decorador: prueba el proveedor principal y, si devuelve null, usa el de respaldo
    public class ProveedorTriviaConRespaldo : IProveedorTrivia
    {
        private readonly IProveedorTrivia _principal;
        private readonly IProveedorTrivia _respaldo;

        public ProveedorTriviaConRespaldo(IProveedorTrivia principal, IProveedorTrivia respaldo)
        {
            _principal = principal; _respaldo = respaldo;
        }

        public void ObtenerPregunta(int dificultad, Action<PreguntaTrivia> alTerminar)
        {
            _principal.ObtenerPregunta(dificultad, pregunta =>
            {
                if (pregunta != null) { alTerminar(pregunta); return; }
                Debug.LogWarning("Trivia con IA no disponible: se usa el banco local.");
                _respaldo.ObtenerPregunta(dificultad, alTerminar);
            });
        }
    }
}