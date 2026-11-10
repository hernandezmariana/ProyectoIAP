/*using UnityEngine;
using ExplosionesIncrementales.Modelo;
using ExplosionesIncrementales.Vistas;

namespace ExplosionesIncrementales.Arranque
{
    public class ArranqueJuego : MonoBehaviour
    {
        [SerializeField] private VistaGrilla vistaGrilla;
        [SerializeField] private VistaJugador vistaJugador;
        [SerializeField] private VistaExplosion vistaExplosion;
        [SerializeField] private VistaTemporizador vistaTemporizador;

        private ModeloGrilla _grilla;
        private ModeloExplosion _explosion;
        private ModeloRonda _ronda;
        private float _cuentaExplosion;

        private void Start()
        {
            _grilla = new ModeloGrilla(15, 15);
            _explosion = new ModeloExplosion();
            _ronda = new ModeloRonda();

            vistaGrilla.Dibujar(_grilla);
            vistaJugador.Iniciar(_grilla, new Vector2Int(7, 7));
            _ronda.Comenzar(1);
            _cuentaExplosion = _explosion.Enfriamiento;
        }

        private void Update()
        {
            if (_ronda.Terminada) return;

            _ronda.Avanzar(Time.deltaTime);
            vistaTemporizador.Mostrar(_ronda.Numero, _ronda.TiempoRestante);

            _cuentaExplosion -= Time.deltaTime;
            if (_cuentaExplosion <= 0f)
            {
                var casillas = _explosion.CalcularCasillas(vistaJugador.Celda, _grilla);
                vistaExplosion.Mostrar(casillas);
                _cuentaExplosion = _explosion.Enfriamiento;
            }
        }
    }
}*/

/*
using System.Collections.Generic;
using UnityEngine;
using ExplosionesIncrementales.Modelo;
using ExplosionesIncrementales.Vistas;

namespace ExplosionesIncrementales.Arranque
{
    public class ArranqueJuego : MonoBehaviour
    {
        [Header("Vistas")]
        [SerializeField] private VistaGrilla vistaGrilla;
        [SerializeField] private VistaJugador vistaJugador;
        [SerializeField] private VistaExplosion vistaExplosion;
        [SerializeField] private VistaTemporizador vistaTemporizador;

        [Header("Sprites (assets del Project)")]
        [SerializeField] private Sprite spriteTriangulo;
        [SerializeField] private Sprite spriteCirculo;

        [Header("Prueba")]
        [SerializeField] private int enemigosPorHorda = 5;

        private ModeloGrilla _grilla;
        private ModeloExplosion _explosion;
        private ModeloRonda _ronda;
        private float _cuentaExplosion;
        private int _dineroRonda; // ganancias de la ronda actual

        private readonly List<VistaEnemigo> _enemigos = new List<VistaEnemigo>();
        private readonly List<VistaMoneda> _monedas = new List<VistaMoneda>();
        private readonly Dictionary<VistaEnemigo, float> _proximoPaso = new Dictionary<VistaEnemigo, float>();

        private void Start()
        {
            _grilla = new ModeloGrilla(15, 15);
            _explosion = new ModeloExplosion();
            _ronda = new ModeloRonda();

            vistaGrilla.Dibujar(_grilla);
            vistaJugador.Iniciar(_grilla, new Vector2Int(7, 7));
            _ronda.Comenzar(1);
            _cuentaExplosion = _explosion.Enfriamiento;
            GenerarHorda();
        }

        private void Update()
        {
            if (_ronda.Terminada) return;

            _ronda.Avanzar(Time.deltaTime);
            vistaTemporizador.Mostrar(_ronda.Numero, _ronda.TiempoRestante, _dineroRonda);

            ActualizarExplosion();
            MoverEnemigos();
            RecogerMonedas();

            if (_enemigos.Count == 0) GenerarHorda(); // al eliminar una horda, aparece otra
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
            for (int i = 0; i < enemigosPorHorda; i++)
            {
                Vector2Int celda;
                int intentos = 0;
                do
                {
                    celda = new Vector2Int(Random.Range(0, _grilla.Ancho), Random.Range(0, _grilla.Alto));
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

                // Nunca dos enemigos en la misma casilla
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
            var libres = CeldasLibresCerca(enemigo.Celda, enemigo.MonedasAlMorir);
            foreach (var celda in libres)
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
            {
                for (int dx = -radio; dx <= radio && resultado.Count < cantidad; dx++)
                {
                    for (int dy = -radio; dy <= radio && resultado.Count < cantidad; dy++)
                    {
                        var celda = centro + new Vector2Int(dx, dy);
                        if (_grilla.EstaDentro(celda) && !HayMoneda(celda) && !resultado.Contains(celda))
                            resultado.Add(celda);
                    }
                }
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
}*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ExplosionesIncrementales.Flujo;
using ExplosionesIncrementales.Modelo;
using ExplosionesIncrementales.Presentadores;
using ExplosionesIncrementales.Vistas;

namespace ExplosionesIncrementales.Arranque
{
    public class ArranqueJuego : MonoBehaviour
    {
        [SerializeField] private VistaGrilla vistaGrilla;
        [SerializeField] private FaseCombate faseCombate;
        [SerializeField] private VistaTienda vistaTienda;
        [SerializeField] private VistaImpuesto vistaImpuesto;
        [SerializeField] private VistaFin vistaFin;

        private void Start()
        {
            var estado = new EstadoJuego();
            var grilla = new ModeloGrilla(15, 15);

            vistaGrilla.Dibujar(grilla);
            faseCombate.Configurar(estado, grilla);

            var fases = new List<IFase>
            {
                faseCombate,
                new FaseAcreditar(estado),
                // acá va la trivia más adelante
                new PresentadorTienda(estado, vistaTienda),
                new PresentadorImpuesto(estado, vistaImpuesto)
            };

            var fin = new PresentadorFin(estado, vistaFin,
                () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));

            new FlujoJuego(estado, fases, fin).Comenzar();
        }
    }
}