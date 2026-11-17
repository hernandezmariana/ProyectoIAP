/*using System;
using ExplosionesIncrementales.Modelo;
using ExplosionesIncrementales.Vistas;

namespace ExplosionesIncrementales.Presentadores
{
    public class PresentadorMenu
    {
        private readonly IVistaMenu _vista;
        private readonly IServicioGuardado _guardado;
        private readonly IServicioLocalizacion _loc;
        private readonly Action _alNuevaPartida;
        private readonly Action<DatosGuardado> _alContinuar;

        public PresentadorMenu(IVistaMenu vista, IServicioGuardado guardado, IServicioLocalizacion loc,
                               Action alNuevaPartida, Action<DatosGuardado> alContinuar)
        {
            _vista = vista; _guardado = guardado; _loc = loc;
            _alNuevaPartida = alNuevaPartida; _alContinuar = alContinuar;

            _vista.AlNuevaPartida += NuevaPartida;
            _vista.AlContinuar += Continuar;
            _vista.AlCambiarIdioma += SiguienteIdioma;
            _loc.AlCambiarIdioma += () => _vista.ActualizarTextos(ArmarTextos());
        }

        public void Mostrar()
        {
            _vista.Mostrar(_guardado.HayGuardado, ArmarTextos());
        }

        private void NuevaPartida()
        {
            _guardado.Borrar();
            _vista.Ocultar();
            _alNuevaPartida();
        }

        private void Continuar()
        {
            var datos = _guardado.Cargar();
            if (datos == null) { NuevaPartida(); return; }   // guardado inválido: arranca de cero
            _vista.Ocultar();
            _alContinuar(datos);
        }

        private void SiguienteIdioma()
        {
            var lista = _loc.Idiomas;
            int actual = 0;
            for (int i = 0; i < lista.Count; i++) if (lista[i] == _loc.IdiomaActual) actual = i;
            _loc.CambiarIdioma(lista[(actual + 1) % lista.Count]);
        }

        private TextosMenu ArmarTextos()
        {
            return new TextosMenu
            {
                Titulo = _loc.Texto("menu.titulo"),
                NuevaPartida = _loc.Texto("menu.nueva"),
                Continuar = _loc.Texto("menu.continuar"),
                Idioma = _loc.Texto("menu.idioma")
            };
        }
    }
}*/

using System;
using ExplosionesIncrementales.Modelo;
using ExplosionesIncrementales.Vistas;

namespace ExplosionesIncrementales.Presentadores
{
    public class PresentadorMenu
    {
        private readonly IVistaMenu _vista;
        private readonly IServicioGuardado _guardado;
        private readonly IServicioLocalizacion _loc;
        private readonly Action _alNuevaPartida;
        private readonly Action<DatosGuardado> _alContinuar;

        public PresentadorMenu(IVistaMenu vista, IServicioGuardado guardado, IServicioLocalizacion loc,
                               Action alNuevaPartida, Action<DatosGuardado> alContinuar)
        {
            _vista = vista; _guardado = guardado; _loc = loc;
            _alNuevaPartida = alNuevaPartida; _alContinuar = alContinuar;

            _vista.AlNuevaPartida += NuevaPartida;
            _vista.AlContinuar += Continuar;
            _vista.AlCambiarIdioma += SiguienteIdioma;
            _loc.AlCambiarIdioma += () => _vista.ActualizarTextos(ArmarTextos());
        }

        public void Mostrar()
        {
            _vista.Mostrar(_guardado.HayGuardado, ArmarTextos());
        }

        private void NuevaPartida()
        {
            _guardado.Borrar();
            _vista.Ocultar();
            _alNuevaPartida();
        }

        private void Continuar()
        {
            var datos = _guardado.Cargar();
            if (datos == null) { NuevaPartida(); return; }
            _vista.Ocultar();
            _alContinuar(datos);
        }

        private void SiguienteIdioma()
        {
            var lista = _loc.Idiomas;
            int actual = 0;
            for (int i = 0; i < lista.Count; i++) if (lista[i] == _loc.IdiomaActual) actual = i;
            _loc.CambiarIdioma(lista[(actual + 1) % lista.Count]);
        }

        private TextosMenu ArmarTextos()
        {
            return new TextosMenu
            {
                Titulo = _loc.Texto("menu.titulo"),
                NuevaPartida = _loc.Texto("menu.nueva"),
                Continuar = _loc.Texto("menu.continuar"),
                Idioma = _loc.Texto("menu.idioma")
            };
        }
    }
}