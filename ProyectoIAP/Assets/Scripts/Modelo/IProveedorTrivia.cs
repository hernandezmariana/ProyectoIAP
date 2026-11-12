using System;

namespace ExplosionesIncrementales.Modelo
{
    public interface IProveedorTrivia
    {
        // Devuelve la pregunta por callback. Así el proveedor puede ser instantáneo (local) o tardar (API).
        void ObtenerPregunta(int dificultad, Action<PreguntaTrivia> alTerminar);
    }
}