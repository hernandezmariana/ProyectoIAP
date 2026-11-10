namespace ExplosionesIncrementales.Modelo
{
    public class ModeloRonda
    {
        public int Numero { get; private set; } = 1;
        public float TiempoRestante { get; private set; }
        public bool Terminada => TiempoRestante <= 0f;

        private const float TiempoBase = 10f;

        public void Comenzar(int numero, float tiempoExtra = 0f)
        {
            Numero = numero;
            TiempoRestante = TiempoBase + tiempoExtra;
        }

        public void Avanzar(float deltaTiempo)
        {
            if (TiempoRestante > 0f)
                TiempoRestante -= deltaTiempo;
        }
    }
}