using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExplosionesIncrementales.Modelo
{
    public static class CatalogoEnemigos
    {
        private static readonly List<DefinicionEnemigo> Todos = new List<DefinicionEnemigo>
        {
            new DefinicionEnemigo { Nombre = "Basico", RondaDesbloqueo = 1, Vida = 2,
                IntervaloMovimiento = 0.8f, Monedas = 3, ValorMoneda = 1,
                Color = Color.red, Escala = 0.7f,
                CrearComportamiento = () => new ComportamientoDeambular() },

            new DefinicionEnemigo { Nombre = "Evasivo", RondaDesbloqueo = 6, Vida = 3,
                IntervaloMovimiento = 0.5f, Monedas = 4, ValorMoneda = 1,
                Color = Color.yellow, Escala = 0.5f,
                CrearComportamiento = () => new ComportamientoEvasivo() },

            new DefinicionEnemigo { Nombre = "Avaro", RondaDesbloqueo = 11, Vida = 4,
                IntervaloMovimiento = 0.6f, Monedas = 4, ValorMoneda = 2,
                Color = new Color(0.7f, 0.2f, 1f), Escala = 0.7f,
                CrearComportamiento = () => new ComportamientoAvaro() },

            new DefinicionEnemigo { Nombre = "Tanque", RondaDesbloqueo = 16, Vida = 12,
                IntervaloMovimiento = 1.4f, Monedas = 8, ValorMoneda = 2,
                Color = new Color(0f, 0.4f, 0.1f), Escala = 0.95f, EsCuadrado = true,
                CrearComportamiento = () => new ComportamientoDeambular() },

            new DefinicionEnemigo { Nombre = "Fantasma", RondaDesbloqueo = 20, Vida = 5,
                IntervaloMovimiento = 0.6f, Monedas = 5, ValorMoneda = 2,
                Color = new Color(0f, 1f, 1f, 0.3f), Escala = 0.7f,
                CrearComportamiento = () => new ComportamientoDeambular() },
        };

        // Todos los tipos ya desbloqueados en esta ronda
        public static List<DefinicionEnemigo> Disponibles(int ronda)
        {
            return Todos.Where(d => d.RondaDesbloqueo <= ronda).ToList();
        }

        public static DefinicionEnemigo ElegirAlAzar(int ronda)
        {
            var disponibles = Disponibles(ronda);
            return disponibles[Random.Range(0, disponibles.Count)];
        }
    }
}