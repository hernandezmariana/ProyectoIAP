/*using System;
using System.Collections.Generic;
using ExplosionesIncrementales.Flujo;
using ExplosionesIncrementales.Modelo;
using ExplosionesIncrementales.Vistas;

namespace ExplosionesIncrementales.Presentadores
{
    public class PresentadorTienda : IFase
    {
        private readonly EstadoJuego _estado;
        private readonly IVistaTienda _vista;
        private Action _alTerminar;

        public PresentadorTienda(EstadoJuego estado, IVistaTienda vista)
        {
            _estado = estado; _vista = vista;
            _vista.AlComprar += Comprar;
            _vista.AlContinuar += Continuar;
        }

        public void Iniciar(Action alTerminar)
        {
            _alTerminar = alTerminar;
            _vista.Mostrar();
            Refrescar();
        }

        private void Comprar(TipoMejora tipo)
        {
            _estado.TryComprar(tipo);
            Refrescar();
        }

        private void Continuar()
        {
            _vista.Ocultar();
            _alTerminar();
        }

        /*private void Refrescar()
        {
            var lista = new List<DatosMejora>();
            foreach (var m in _estado.Mejoras)
            {
                lista.Add(new DatosMejora
                {
                    Tipo = m.Tipo, Nombre = m.Nombre, Nivel = m.Nivel,
                    Costo = m.Costo, Alcanza = _estado.Dinero >= m.Costo
                });
            }
            _vista.ActualizarDatos(_estado.Ronda, _estado.Dinero,
                _estado.Impuestos.MontoProximo,
                _estado.Impuestos.RondasHastaImpuesto(_estado.Ronda), lista);
        }//
        private void Refrescar()
{
    var filas = new List<FilaMejora>();
    foreach (var m in _estado.Mejoras)
    {
        filas.Add(new FilaMejora
        {
            Tipo = m.Tipo,
            Texto = _loc.Texto("mejora.fila", _loc.Texto("mejora." + m.Tipo), m.Nivel, m.Costo),
            Alcanza = _estado.Dinero >= m.Costo
        });
    }
    string info = _loc.Texto("tienda.info", _estado.Ronda, _estado.Dinero,
        _estado.Impuestos.MontoProximo, _estado.Impuestos.RondasHastaImpuesto(_estado.Ronda));
    _vista.ActualizarDatos(info, _loc.Texto("comun.continuar"), filas);
}
    }
}*/


using System;
using System.Collections.Generic;
using ExplosionesIncrementales.Flujo;
using ExplosionesIncrementales.Modelo;
using ExplosionesIncrementales.Vistas;

namespace ExplosionesIncrementales.Presentadores
{
    public class PresentadorTienda : IFase
    {
        private readonly EstadoJuego _estado;
        private readonly IVistaTienda _vista;
        private readonly IServicioLocalizacion _loc;
        private Action _alTerminar;

        public PresentadorTienda(EstadoJuego estado, IVistaTienda vista, IServicioLocalizacion loc)
        {
            _estado = estado; _vista = vista; _loc = loc;
            _vista.AlComprar += Comprar;
            _vista.AlContinuar += Continuar;
        }

        public void Iniciar(Action alTerminar)
        {
            _alTerminar = alTerminar;
            _vista.Mostrar();
            Refrescar();
        }

        private void Comprar(TipoMejora tipo)
        {
            _estado.TryComprar(tipo);
            Refrescar();
        }

        private void Continuar()
        {
            _vista.Ocultar();
            _alTerminar();
        }

        private void Refrescar()
        {
            var filas = new List<FilaMejora>();
            foreach (var m in _estado.Mejoras)
            {
                filas.Add(new FilaMejora
                {
                    Tipo = m.Tipo,
                    Texto = _loc.Texto("mejora.fila", _loc.Texto("mejora." + m.Tipo), m.Nivel, m.Costo),
                    Alcanza = _estado.Dinero >= m.Costo
                });
            }
            string info = _loc.Texto("tienda.info", _estado.Ronda, _estado.Dinero,
                _estado.Impuestos.MontoProximo, _estado.Impuestos.RondasHastaImpuesto(_estado.Ronda));
            _vista.ActualizarDatos(info, _loc.Texto("comun.continuar"), filas);
        }
    }
}