using UnityEngine;

namespace ExplosionesIncrementales.Modelo
{
    public class ModeloGrilla
    {
        public int Ancho { get; }
        public int Alto { get; }

        public ModeloGrilla(int ancho, int alto)
        {
            Ancho = ancho;
            Alto = alto;
        }

        public bool EstaDentro(Vector2Int celda)
        {
            return celda.x >= 0 && celda.x < Ancho && celda.y >= 0 && celda.y < Alto;
        }
    }
}