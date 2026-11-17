/*using System;
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
}*/

/*
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
        private readonly IServicioGuardado _guardado;

        public PresentadorFin(EstadoJuego estado, IVistaFin vista, IServicioGuardado guardado, Action alReiniciar)
        {
            _estado = estado; _vista = vista; _guardado = guardado;
            _vista.AlReiniciar += () => alReiniciar();
        }

       /* public void Iniciar(Action alTerminar)
        {
            _guardado.Borrar();   // game over o victoria: no se puede continuar
            _vista.Mostrar(_estado.Resultado == ResultadoPartida.Victoria, _estado.Ronda);
        }//

        bool victoria = _estado.Resultado == ResultadoPartida.Victoria;
_vista.Mostrar(
    _loc.Texto(victoria ? "fin.victoria" : "fin.gameover", _estado.Ronda),
    _loc.Texto("fin.reiniciar"));
    }
}*/

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
        private readonly IServicioGuardado _guardado;
        private readonly IServicioLocalizacion _loc;

        public PresentadorFin(EstadoJuego estado, IVistaFin vista, IServicioGuardado guardado,
                              IServicioLocalizacion loc, Action alReiniciar)
        {
            _estado = estado; _vista = vista; _guardado = guardado; _loc = loc;
            _vista.AlReiniciar += () => alReiniciar();
        }

        public void Iniciar(Action alTerminar)
        {
            _guardado.Borrar();   // game over o victoria: no se puede continuar
            bool victoria = _estado.Resultado == ResultadoPartida.Victoria;
            _vista.Mostrar(
                _loc.Texto(victoria ? "fin.victoria" : "fin.gameover", _estado.Ronda),
                _loc.Texto("fin.reiniciar"));
        }
    }
}