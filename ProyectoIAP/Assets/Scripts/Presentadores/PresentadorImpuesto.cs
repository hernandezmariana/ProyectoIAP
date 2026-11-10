using System;
using ExplosionesIncrementales.Flujo;
using ExplosionesIncrementales.Modelo;
using ExplosionesIncrementales.Vistas;

namespace ExplosionesIncrementales.Presentadores
{
    public class PresentadorImpuesto : IFase
    {
        private readonly EstadoJuego _estado;
        private readonly IVistaImpuesto _vista;
        private Action _alTerminar;

        public PresentadorImpuesto(EstadoJuego estado, IVistaImpuesto vista)
        {
            _estado = estado; _vista = vista;
            _vista.AlPagar += Pagar;
        }

        public void Iniciar(Action alTerminar)
        {
            if (!_estado.Impuestos.EsRondaDeImpuesto(_estado.Ronda)) { alTerminar(); return; }
            _alTerminar = alTerminar;
            _vista.Mostrar(_estado.Ronda, _estado.Impuestos.MontoProximo, _estado.Dinero);
        }

        private void Pagar()
        {
            _vista.Ocultar();
            if (!_estado.TryPagarImpuesto())
                _estado.Resultado = ResultadoPartida.Bancarrota;
            else if (_estado.Ronda >= SistemaImpuestos.RondaFinal)
                _estado.Resultado = ResultadoPartida.Victoria;
            _alTerminar();
        }
    }
}