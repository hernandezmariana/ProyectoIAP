using System;
using System.Collections.Generic;
using ExplosionesIncrementales.Modelo;

namespace ExplosionesIncrementales.Vistas
{
    public struct DatosMejora
    {
        public TipoMejora Tipo;
        public string Nombre;
        public int Nivel;
        public int Costo;
        public bool Alcanza;
    }

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
        void Mostrar(bool victoria, int ronda);
    }
}