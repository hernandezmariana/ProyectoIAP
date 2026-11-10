using UnityEngine;

namespace ExplosionesIncrementales.Modelo
{
    public class ModeloMoneda
    {
        public Vector2Int Celda { get; }
        public int Valor { get; }

        public ModeloMoneda(Vector2Int celda, int valor)
        {
            Celda = celda;
            Valor = valor;
        }
    }
}