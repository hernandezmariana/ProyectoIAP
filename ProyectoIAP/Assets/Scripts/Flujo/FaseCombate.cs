using System;
using System.Collections.Generic;
using UnityEngine;
using ExplosionesIncrementales.Modelo;
using ExplosionesIncrementales.Vistas;

namespace ExplosionesIncrementales.Flujo
{
    public class FaseCombate : MonoBehaviour, IFase
    {
        [Header("Vistas")]
        [SerializeField] private VistaJugador vistaJugador;
        [SerializeField] private VistaExplosion vistaExplosion;
        [SerializeField] private VistaTemporizador vistaTemporizador;

        [Header("Sprites (assets del Project)")]
        [SerializeField] private Sprite spriteTriangulo;
        [SerializeField] private Sprite spriteCirculo;

        [Header("Balance")]
        [SerializeField] private int enemigosPorHorda = 5;

        private EstadoJuego _estado;
        private ModeloGrilla _grilla;
        private readonly ModeloExplosion _explosion = new ModeloExplosion();
        private readonly ModeloRonda _ronda = new ModeloRonda();
        private float _cuentaExplosion;
        private int _dineroRonda;
        private bool _activa;
        private Action _alTerminar;

        private readonly List<VistaEnemigo> _enemigos = new List<VistaEnemigo>();
        private readonly List<VistaMoneda> _monedas = new List<VistaMoneda>();
        private readonly Dictionary<VistaEnemigo, float> _proximoPaso = new Dictionary<VistaEnemigo, float>();

        public void Configurar(EstadoJuego estado, ModeloGrilla grilla)
        {
            _estado = estado;
            _grilla = grilla;
        }

        public void Iniciar(Action alTerminar)
        {
            _alTerminar = alTerminar;
            _explosion.AplicarMejoras(_estado);
            _ronda.Comenzar(_estado.Ronda, _estado.TiempoExtra);
            _dineroRonda = 0;
            _cuentaExplosion = _explosion.Enfriamiento;

            vistaJugador.Iniciar(_grilla, new Vector2Int(7, 7));
            vistaJugador.Habilitado = true;
            GenerarHorda();
            _activa = true;
        }

        private void Update()
        {
            if (!_activa) return;

            _ronda.Avanzar(Time.deltaTime);
            vistaTemporizador.Mostrar(_ronda.Numero, _ronda.TiempoRestante, _dineroRonda);

            if (_ronda.Terminada) { Terminar(); return; }

            ActualizarExplosion();
            MoverEnemigos();
            RecogerMonedas();

            if (_enemigos.Count == 0) GenerarHorda();
        }

        private void Terminar()
        {
            _activa = false;
            vistaJugador.Habilitado = false;
            LimpiarEscena();
            _estado.GananciasRonda = _dineroRonda;
            _alTerminar();
        }

        private void LimpiarEscena()
        {
            foreach (var e in _enemigos) Destroy(e.gameObject);
            foreach (var m in _monedas) Destroy(m.gameObject);
            _enemigos.Clear();
            _monedas.Clear();
            _proximoPaso.Clear();
        }

        // ---------- Explosión ----------
        private void ActualizarExplosion()
        {
            _cuentaExplosion -= Time.deltaTime;
            if (_cuentaExplosion > 0f) return;

            var casillas = _explosion.CalcularCasillas(vistaJugador.Celda, _grilla);
            vistaExplosion.Mostrar(casillas);
            _cuentaExplosion = _explosion.Enfriamiento;

            var muertos = new List<VistaEnemigo>();
            foreach (var vista in _enemigos)
            {
                if (casillas.Contains(vista.Modelo.Celda))
                {
                    vista.Modelo.RecibirDano(_explosion.Dano);
                    if (vista.Modelo.EstaMuerto) muertos.Add(vista);
                }
            }
            foreach (var vista in muertos) MatarEnemigo(vista);
        }

        private void MatarEnemigo(VistaEnemigo vista)
        {
            SoltarMonedas(vista.Modelo);
            _enemigos.Remove(vista);
            _proximoPaso.Remove(vista);
            Destroy(vista.gameObject);
        }

        // ---------- Enemigos ----------
        private void GenerarHorda()
        {
            int cantidad = Mathf.Min(enemigosPorHorda + _estado.Ronda / 2, 20);
            for (int i = 0; i < cantidad; i++)
            {
                Vector2Int celda;
                int intentos = 0;
                do
                {
                    celda = new Vector2Int(UnityEngine.Random.Range(0, _grilla.Ancho),
                                           UnityEngine.Random.Range(0, _grilla.Alto));
                    intentos++;
                } while (intentos < 50 && (CeldaOcupadaPorEnemigo(celda) || DistanciaAlJugador(celda) < 3));

                if (intentos >= 50) continue;

                var modelo = new ModeloEnemigo(celda, 2, 0.8f, 3, new ComportamientoDeambular());
                var objeto = new GameObject("EnemigoBasico");
                var vista = objeto.AddComponent<VistaEnemigo>();
                vista.Iniciar(modelo, spriteTriangulo, Color.red);
                _enemigos.Add(vista);
                _proximoPaso[vista] = Time.time + modelo.IntervaloMovimiento;
            }
        }

        private void MoverEnemigos()
        {
            foreach (var vista in _enemigos)
            {
                if (Time.time < _proximoPaso[vista]) continue;

                var modelo = vista.Modelo;
                var destino = modelo.Comportamiento.ProximaCelda(modelo, _grilla, vistaJugador.Celda);

                if (_grilla.EstaDentro(destino) && !CeldaOcupadaPorEnemigo(destino))
                {
                    modelo.Celda = destino;
                    vista.Actualizar();
                }
                _proximoPaso[vista] = Time.time + modelo.IntervaloMovimiento;
            }
        }

        private bool CeldaOcupadaPorEnemigo(Vector2Int celda)
        {
            foreach (var vista in _enemigos)
                if (vista.Modelo.Celda == celda) return true;
            return false;
        }

        private int DistanciaAlJugador(Vector2Int celda)
        {
            return Mathf.Abs(celda.x - vistaJugador.Celda.x) + Mathf.Abs(celda.y - vistaJugador.Celda.y);
        }

        // ---------- Monedas ----------
        private void SoltarMonedas(ModeloEnemigo enemigo)
        {
            foreach (var celda in CeldasLibresCerca(enemigo.Celda, enemigo.MonedasAlMorir))
            {
                var modelo = new ModeloMoneda(celda, 1);
                var objeto = new GameObject("Moneda");
                var vista = objeto.AddComponent<VistaMoneda>();
                vista.Iniciar(modelo, spriteCirculo);
                _monedas.Add(vista);
            }
        }

        private List<Vector2Int> CeldasLibresCerca(Vector2Int centro, int cantidad)
        {
            var resultado = new List<Vector2Int>();
            for (int radio = 0; radio <= 3 && resultado.Count < cantidad; radio++)
                for (int dx = -radio; dx <= radio && resultado.Count < cantidad; dx++)
                    for (int dy = -radio; dy <= radio && resultado.Count < cantidad; dy++)
                    {
                        var celda = centro + new Vector2Int(dx, dy);
                        if (_grilla.EstaDentro(celda) && !HayMoneda(celda) && !resultado.Contains(celda))
                            resultado.Add(celda);
                    }
            return resultado;
        }

        private bool HayMoneda(Vector2Int celda)
        {
            foreach (var vista in _monedas)
                if (vista.Modelo.Celda == celda) return true;
            return false;
        }

        private void RecogerMonedas()
        {
            for (int i = _monedas.Count - 1; i >= 0; i--)
            {
                if (_monedas[i].Modelo.Celda == vistaJugador.Celda)
                {
                    _dineroRonda += _monedas[i].Modelo.Valor;
                    Destroy(_monedas[i].gameObject);
                    _monedas.RemoveAt(i);
                }
            }
        }
    }
}