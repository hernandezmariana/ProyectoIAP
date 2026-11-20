using System;
using System.Collections.Generic;
using ExplosionesIncrementales.Modelo;

namespace ExplosionesIncrementales.Vistas
{
    public struct FilaMejora
    {
        public TipoMejora Tipo;
        public string Texto;
        public bool Alcanza;
    }

    public struct TextosMenu
    {
        public string Titulo, NuevaPartida, Continuar, Idioma;
    }

    public interface IVistaTienda
    {
        event Action<TipoMejora> AlComprar;
        event Action AlContinuar;
        void Mostrar();
        void Ocultar();
        void ActualizarDatos(string info, string textoContinuar, List<FilaMejora> filas);
    }

    public interface IVistaImpuesto
    {
        event Action AlPagar;
        void Mostrar(string texto, string textoBoton);
        void Ocultar();
    }

    public interface IVistaFin
    {
        event Action AlReiniciar;
        void Mostrar(string textoResultado, string textoBoton);
    }

    public interface IVistaTrivia
    {
        event Action<bool> AlDecidir;
        event Action<int> AlResponder;
        event Action AlContinuar;

        void MostrarDecision(string texto, string arriesgar, string conservar);
        void MostrarPregunta(string texto, string[] opciones);
        void MostrarResultado(string texto, string textoContinuar);
        void Ocultar();

        //para la trivia IA
        public void MostrarCargando(string texto);
    
    }

    public interface IVistaMenu
    {
        event Action AlNuevaPartida;
        event Action AlContinuar;
        event Action AlCambiarIdioma;
        void Mostrar(bool hayGuardado, TextosMenu textos);
        void ActualizarTextos(TextosMenu textos);
        void Ocultar();
    }
}



/*
 ULTIMO 
using System;
using System.Collections.Generic;
using ExplosionesIncrementales.Modelo;

namespace ExplosionesIncrementales.Vistas
{

    public struct TextosMenu
        {
            public string Titulo, NuevaPartida, Continuar, Idioma;
        }

        public interface IVistaMenu
        {
            event Action AlNuevaPartida;
            event Action AlContinuar;
            event Action AlCambiarIdioma;
            void Mostrar(bool hayGuardado, TextosMenu textos);
            void ActualizarTextos(TextosMenu textos);
            void Ocultar();
        }
    public struct DatosMejora
    {
        public TipoMejora Tipo;
        public string Nombre;
        public int Nivel;
        public int Costo;
        public bool Alcanza;
    }

    public struct FilaMejora
{
    public TipoMejora Tipo;
    public string Texto;
    public bool Alcanza;
}

public interface IVistaTienda
{
    event Action<TipoMejora> AlComprar;
    event Action AlContinuar;
    void Mostrar();
    void Ocultar();
    void ActualizarDatos(string info, string textoContinuar, List<FilaMejora> filas);
}

public interface IVistaImpuesto
{
    event Action AlPagar;
    void Mostrar(string texto, string textoBoton);
    void Ocultar();
}

public interface IVistaFin
{
    event Action AlReiniciar;
    void Mostrar(string textoResultado, string textoBoton);
}

public interface IVistaTrivia
{
    event Action<bool> AlDecidir;
    event Action<int> AlResponder;
    event Action AlContinuar;

    void MostrarDecision(string texto, string arriesgar, string conservar);
    void MostrarPregunta(string texto, string[] opciones);
    void MostrarResultado(string texto, string textoContinuar);
    void Ocultar();
}
*/

/*
    public interface IVistaTienda
    {
        event Action<TipoMejora> AlComprar;
        event Action AlContinuar;
        void Mostrar();
        void Ocultar();
        void ActualizarDatos(int ronda, int dinero, int montoImpuesto, int rondasFaltantes, List<DatosMejora> mejoras);
    }

    public interface IVistaImpuesto
    {
        event Action AlPagar;
        void Mostrar(int ronda, int monto, int dinero);
        void Ocultar();
    }

    public interface IVistaFin
    {
        event Action AlReiniciar;
      //  void Mostrar(bool victoria, int ronda);
          void Mostrar(string textoResultado, string textoBoton);

    }

    public interface IVistaTrivia
    {
        event Action<bool> AlDecidir;      // true = participa
        event Action<int> AlResponder;     // índice de la opción elegida
        event Action AlContinuar;

      //  void MostrarDecision(int ganancias);
        //void MostrarPregunta(PreguntaTrivia pregunta, int ganancias);
        
        //void MostrarResultado(ResultadoTrivia resultado, int antes, int despues);
        
        //void Ocultar();


          void MostrarDecision(string texto, string arriesgar, string conservar);
            void MostrarPregunta(string texto, string[] opciones);
            void MostrarResultado(string texto, string textoContinuar);
            void Ocultar();
     }}*/
