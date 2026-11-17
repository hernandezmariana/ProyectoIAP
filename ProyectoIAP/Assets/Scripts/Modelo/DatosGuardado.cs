using System;

namespace ExplosionesIncrementales.Modelo
{
    [Serializable]
    public class DatosGuardado
    {
        public int ronda;
        public int dinero;
        public int proximoImpuesto;
        public int[] nivelesMejoras;   // indexado por (int)TipoMejora
    }
}