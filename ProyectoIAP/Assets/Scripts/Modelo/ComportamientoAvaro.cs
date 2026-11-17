using System.Collections.Generic;
using UnityEngine;

namespace ExplosionesIncrementales.Modelo
{
    // Quien sepa dónde hay monedas implementa estola fase de combate
    public interface IFuenteMonedas
    {
        IEnumerable<Vector2Int> CeldasConMonedas();
        void ConsumirMonedaEn(Vector2Int celda);
    }

    public class ComportamientoAvaro : IComportamiento
    {
        public static IFuenteMonedas Fuente;   // la asigna FaseCombate
        private readonly ComportamientoDeambular _deambular = new ComportamientoDeambular();

        public Vector2Int ProximaCelda(ModeloEnemigo enemigo, ModeloGrilla grilla, Vector2Int celdaJugador)
        {
            if (Fuente == null) return _deambular.ProximaCelda(enemigo, grilla, celdaJugador);

            // Si está sobre una moneda, la consume
            Fuente.ConsumirMonedaEn(enemigo.Celda);

            bool encontrada = false;
            Vector2Int masCercana = enemigo.Celda;
            int mejorDistancia = int.MaxValue;
            foreach (var celda in Fuente.CeldasConMonedas())
            {
                int d = Mathf.Abs(celda.x - enemigo.Celda.x) + Mathf.Abs(celda.y - enemigo.Celda.y);
                if (d < mejorDistancia) {
                     mejorDistancia = d;
                     masCercana = celda; 
                     encontrada = true; }
            }

            if (!encontrada) return _deambular.ProximaCelda(enemigo, grilla, celdaJugador);

            Vector2Int diferencia = masCercana - enemigo.Celda;
            Vector2Int paso = Mathf.Abs(diferencia.x) >= Mathf.Abs(diferencia.y)
                ? new Vector2Int((int)Mathf.Sign(diferencia.x), 0)
                : new Vector2Int(0, (int)Mathf.Sign(diferencia.y));
            return enemigo.Celda + paso;
        }
    }
}