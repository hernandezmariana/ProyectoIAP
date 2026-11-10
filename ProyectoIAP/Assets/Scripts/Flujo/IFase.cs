using System;

namespace ExplosionesIncrementales.Flujo
{
    public interface IFase
    {
        void Iniciar(Action alTerminar);
    }
}