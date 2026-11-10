using System.Collections.Generic;
using UnityEngine;

namespace ExplosionesIncrementales.Modelo
{
    public class ModeloExplosion
    {
        public int Radio { get; set; } = 3;
        public float Enfriamiento { get; set; } = 1.5f; // segundos entre explosiones
        public int Dano { get; set; } = 1;

        // Devuelve todas las casillas que toca la cruz, recortadas a los bordes
        public List<Vector2Int> CalcularCasillas(Vector2Int centro, ModeloGrilla grilla)
        {
            var casillas = new List<Vector2Int> { centro };
            Vector2Int[] direcciones =
            {
                Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
            };

            foreach (var direccion in direcciones)
            {
                for (int i = 1; i <= Radio; i++)
                {
                    Vector2Int casilla = centro + direccion * i;
                    if (grilla.EstaDentro(casilla))
                        casillas.Add(casilla);
                }
            }
            return casillas;
        }

        public void AplicarMejoras(EstadoJuego estado)
        {
            Radio = 3 + estado.Nivel(TipoMejora.Radio);
            Enfriamiento = Mathf.Max(0.3f, 1.5f - 0.1f * estado.Nivel(TipoMejora.Velocidad));
            Dano = 1 + estado.Nivel(TipoMejora.Dano);
        }
    }
}