using UnityEngine;

namespace ExplosionesIncrementales.Modelo
{
    public interface IComportamiento
    {
        Vector2Int ProximaCelda(ModeloEnemigo enemigo, ModeloGrilla grilla, Vector2Int celdaJugador);
    }
}