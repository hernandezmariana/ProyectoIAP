using System;

namespace ExplosionesIncrementales.Modelo
{
    public class Mejora
    {
        public TipoMejora Tipo { get; }
        public string Nombre { get; }
        public int Nivel { get; private set; }

        private readonly int _costoBase;
        private readonly double _crecimiento;

        public Mejora(TipoMejora tipo, string nombre, int costoBase, double crecimiento = 1.6)
        {
            Tipo = tipo; Nombre = nombre;
            _costoBase = costoBase; _crecimiento = crecimiento;
        }

        // El precio crece exponencialmente con cada compra
        public int Costo => (int)Math.Round(_costoBase * Math.Pow(_crecimiento, Nivel));

        public void Subir() { Nivel++; }
    }
}