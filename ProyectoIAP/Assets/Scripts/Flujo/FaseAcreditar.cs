using System;
using ExplosionesIncrementales.Modelo;

namespace ExplosionesIncrementales.Flujo
{
    public class FaseAcreditar : IFase
    {
        private readonly EstadoJuego _estado;
        public FaseAcreditar(EstadoJuego estado) { _estado = estado; }

        public void Iniciar(Action alTerminar)
        {
            _estado.AcreditarGanancias();
            alTerminar();
        }
    }
}