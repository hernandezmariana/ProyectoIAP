using UnityEngine;

namespace ExplosionesIncrementales.Modelo
{
    public class ModeloEnemigo
    {
        public Vector2Int Celda { get; set; }
        public int Vida { get; private set; }
        public float IntervaloMovimiento { get; }   // segundos entre pasos (mayor = más lento)
        public int MonedasAlMorir { get; }
        public IComportamiento Comportamiento { get; }
        public bool EstaMuerto => Vida <= 0;

        public ModeloEnemigo(Vector2Int celda, int vida, float intervalo, int monedas, IComportamiento comportamiento)
        {
            Celda = celda;
            Vida = vida;
            IntervaloMovimiento = intervalo;
            MonedasAlMorir = monedas;
            Comportamiento = comportamiento;
        }

        public void RecibirDano(int dano) => Vida -= dano;
    }
}