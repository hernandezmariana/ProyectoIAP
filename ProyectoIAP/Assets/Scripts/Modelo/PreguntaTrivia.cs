using System;

namespace ExplosionesIncrementales.Modelo
{
    [Serializable]
    public class PreguntaTrivia
    {
        public string enunciado;
        public string[] opciones;
        public int indiceCorrecto;
        public int dificultad; // 1 a 50
    }

    [Serializable]
    public class ListaPreguntas
    {
        public PreguntaTrivia[] preguntas;
    }
}