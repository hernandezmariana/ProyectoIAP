using System;
using ExplosionesIncrementales.Flujo;
using ExplosionesIncrementales.Modelo;
using ExplosionesIncrementales.Vistas;

namespace ExplosionesIncrementales.Presentadores
{
    public class PresentadorFin : IFase
    {
        private readonly EstadoJuego _estado;
        private readonly IVistaFin _vista;

        public PresentadorFin(EstadoJuego estado, IVistaFin vista, Action alReiniciar)
        {
            _estado = estado; _vista = vista;
            _vista.AlReiniciar += () => alReiniciar();
        }

        public void Iniciar(Action alTerminar)
        {
            _vista.Mostrar(_estado.Resultado == ResultadoPartida.Victoria, _estado.Ronda);
        }
    }
}