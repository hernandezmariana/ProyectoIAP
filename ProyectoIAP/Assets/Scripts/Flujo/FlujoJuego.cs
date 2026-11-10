using System.Collections.Generic;
using ExplosionesIncrementales.Modelo;

namespace ExplosionesIncrementales.Flujo
{
    public class FlujoJuego
    {
        private readonly EstadoJuego _estado;
        private readonly List<IFase> _fasesDeRonda;
        private readonly IFase _faseFin;
        private int _indice;

        public FlujoJuego(EstadoJuego estado, List<IFase> fasesDeRonda, IFase faseFin)
        {
            _estado = estado; _fasesDeRonda = fasesDeRonda; _faseFin = faseFin;
        }

        public void Comenzar() { _indice = 0; SiguienteFase(); }

        private void SiguienteFase()
        {
            if (_estado.Resultado != ResultadoPartida.EnCurso)
            {
                _faseFin.Iniciar(() => { });
                return;
            }
            if (_indice >= _fasesDeRonda.Count)
            {
                _estado.Ronda++;
                Comenzar();
                return;
            }
            _fasesDeRonda[_indice++].Iniciar(SiguienteFase);
        }
    }
}