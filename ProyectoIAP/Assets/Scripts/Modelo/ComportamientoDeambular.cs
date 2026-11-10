using UnityEngine;

namespace ExplosionesIncrementales.Modelo
{
    public class ComportamientoDeambular : IComportamiento
    {
        private static readonly Vector2Int[] Direcciones =
        {
            Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right, Vector2Int.zero
        };

        public Vector2Int ProximaCelda(ModeloEnemigo enemigo, ModeloGrilla grilla, Vector2Int celdaJugador)
        {
            var direccion = Direcciones[Random.Range(0, Direcciones.Length)];
            return enemigo.Celda + direccion;
        }
    }
}