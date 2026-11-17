using UnityEngine;

namespace ExplosionesIncrementales.Modelo
{
    public class ComportamientoEvasivo : IComportamiento
    {
        private const int RadioDeteccion = 5;
        private readonly ComportamientoDeambular _deambular = new ComportamientoDeambular();

        public Vector2Int ProximaCelda(ModeloEnemigo enemigo, ModeloGrilla grilla, Vector2Int celdaJugador)
        {
            Vector2Int diferencia = enemigo.Celda - celdaJugador;
            int distancia = Mathf.Abs(diferencia.x) + Mathf.Abs(diferencia.y);

            if (distancia > RadioDeteccion)
                return _deambular.ProximaCelda(enemigo, grilla, celdaJugador);

            // Se aleja por el eje donde está más cerca del jugador
            Vector2Int paso = Mathf.Abs(diferencia.x) >= Mathf.Abs(diferencia.y)
                ? new Vector2Int((int)Mathf.Sign(diferencia.x == 0 ? 1 : diferencia.x), 0)
                : new Vector2Int(0, (int)Mathf.Sign(diferencia.y == 0 ? 1 : diferencia.y));

            Vector2Int destino = enemigo.Celda + paso;
            // Si choca con el borde, prueba el otro eje
            if (!grilla.EstaDentro(destino))
                destino = enemigo.Celda + new Vector2Int(paso.y, paso.x);
            return destino;
        }
    }
}