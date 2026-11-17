using UnityEngine;
using System;

namespace ExplosionesIncrementales.Modelo
{
    public class DefinicionEnemigo
    {
        public string Nombre;
        public int RondaDesbloqueo;
        public int Vida;
        public float IntervaloMovimiento;   // menor = más rápido
        public int Monedas;
        public int ValorMoneda;
        public Color Color;
        public float Escala;
        public bool EsCuadrado;             // el tanque usa cuadrado, el resto triángulo/rombo
        public Func<IComportamiento> CrearComportamiento;
    }
}