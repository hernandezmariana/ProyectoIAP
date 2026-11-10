namespace ExplosionesIncrementales.Modelo
{
    public class SistemaImpuestos
    {
        public const int Intervalo = 5;
        public const int RondaFinal = 50;

        public int MontoProximo { get; private set; } = 100;

        public bool EsRondaDeImpuesto(int ronda) { return ronda % Intervalo == 0; }

        public int RondasHastaImpuesto(int ronda)
        {
            int resto = ronda % Intervalo;
            return resto == 0 ? 0 : Intervalo - resto;
        }

        public void SubirMonto() { MontoProximo = (int)(MontoProximo * 1.5f); }
    }
}