using System.Collections.Generic;

namespace ExplosionesIncrementales.Modelo
{
    public enum ResultadoPartida { EnCurso, Bancarrota, Victoria }

    public class EstadoJuego
    {
        public int Ronda { get; set; } = 1;
        public int Dinero { get; private set; }=500;
        public int GananciasRonda { get; set; }
        public ResultadoPartida Resultado { get; set; } = ResultadoPartida.EnCurso;
        public SistemaImpuestos Impuestos { get; } = new SistemaImpuestos();

        private readonly Dictionary<TipoMejora, Mejora> _mejoras = new Dictionary<TipoMejora, Mejora>();
        public IEnumerable<Mejora> Mejoras { get { return _mejoras.Values; } }

        public EstadoJuego()
        {
            Agregar(new Mejora(TipoMejora.Radio, "Radio", 20));
            Agregar(new Mejora(TipoMejora.Tiempo, "Tiempo", 15));
            Agregar(new Mejora(TipoMejora.Velocidad, "Velocidad", 25));
            Agregar(new Mejora(TipoMejora.Dano, "Daño", 30));
            Agregar(new Mejora(TipoMejora.Suerte, "Suerte", 20));
        }

        private void Agregar(Mejora m) { _mejoras[m.Tipo] = m; }

        public int Nivel(TipoMejora tipo) { return _mejoras[tipo].Nivel; }
        public float TiempoExtra { get { return 3f * Nivel(TipoMejora.Tiempo); } }

        public void AcreditarGanancias()
        {
            Dinero += GananciasRonda;
            GananciasRonda = 0;
        }

        public bool TryComprar(TipoMejora tipo)
        {
            var mejora = _mejoras[tipo];
            if (Dinero < mejora.Costo) return false;
            Dinero -= mejora.Costo;
            mejora.Subir();
            return true;
        }

        public bool TryPagarImpuesto()
        {
            if (Dinero < Impuestos.MontoProximo) return false;
            Dinero -= Impuestos.MontoProximo;
            Impuestos.SubirMonto();
            return true;
        }
    }
}