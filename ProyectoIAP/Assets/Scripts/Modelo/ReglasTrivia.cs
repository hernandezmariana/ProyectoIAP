namespace ExplosionesIncrementales.Modelo
{
    public static class ReglasTrivia
    {
        public static int Resolver(int ganancias, ResultadoTrivia resultado)
        {
            switch (resultado)
            {
                case ResultadoTrivia.Correcta:   return ganancias * 2;
                case ResultadoTrivia.Incorrecta: return ganancias / 2;
                default:                         return ganancias;
            }
        }
    }
}