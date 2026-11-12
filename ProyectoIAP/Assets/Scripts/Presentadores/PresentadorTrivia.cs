using System;
using ExplosionesIncrementales.Flujo;
using ExplosionesIncrementales.Modelo;
using ExplosionesIncrementales.Vistas;

namespace ExplosionesIncrementales.Presentadores
{
    public class PresentadorTrivia : IFase
    {
        private readonly EstadoJuego _estado;
        private readonly IVistaTrivia _vista;
        private readonly IProveedorTrivia _proveedor;
        private PreguntaTrivia _pregunta;
        private Action _alTerminar;

        public PresentadorTrivia(EstadoJuego estado, IVistaTrivia vista, IProveedorTrivia proveedor)
        {
            _estado = estado; _vista = vista; _proveedor = proveedor;
            _vista.AlDecidir += Decidir;
            _vista.AlResponder += Responder;
            _vista.AlContinuar += Continuar;
        }

        public void Iniciar(Action alTerminar)
        {
            _alTerminar = alTerminar;
            _pregunta = null;

            // Sin ganancias no hay nada en juego
            if (_estado.GananciasRonda <= 0) { alTerminar(); return; }

            _vista.MostrarDecision(_estado.GananciasRonda);
        }

        private void Decidir(bool participa)
        {
            if (!participa) { Finalizar(ResultadoTrivia.NoRespondio); return; }

            _proveedor.ObtenerPregunta(_estado.Ronda, pregunta =>
            {
                _pregunta = pregunta;
                _vista.MostrarPregunta(pregunta, _estado.GananciasRonda);
            });
        }

        private void Responder(int indice)
        {
            if (_pregunta == null) return;
            Finalizar(indice == _pregunta.indiceCorrecto
                ? ResultadoTrivia.Correcta
                : ResultadoTrivia.Incorrecta);
        }

        private void Finalizar(ResultadoTrivia resultado)
        {
            int antes = _estado.GananciasRonda;
            int despues = ReglasTrivia.Resolver(antes, resultado);
            _estado.GananciasRonda = despues;
            _pregunta = null;
            _vista.MostrarResultado(resultado, antes, despues);
        }

        private void Continuar()
        {
            _vista.Ocultar();
            _alTerminar();
        }
    }
}