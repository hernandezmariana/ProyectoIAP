using System;
using System.Collections.Generic;

namespace ExplosionesIncrementales.Modelo
{
    public interface IServicioLocalizacion
    {
        string IdiomaActual { get; }
        IReadOnlyList<string> Idiomas { get; }
        event Action AlCambiarIdioma;
        string Texto(string clave);
        void CambiarIdioma(string codigo);

        string Texto(string clave, params object[] argumentos);
    }
}