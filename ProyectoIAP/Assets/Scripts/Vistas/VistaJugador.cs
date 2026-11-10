using UnityEngine;
using ExplosionesIncrementales.Modelo;

namespace ExplosionesIncrementales.Vistas
{
    public class VistaJugador : MonoBehaviour
    {
        [SerializeField] private float intervaloMovimiento = 0.12f; // segundos entre pasos

        private ModeloGrilla _grilla;
        private float _temporizador;

        public Vector2Int Celda { get; private set; }

        public bool Habilitado { get; set; } = true;   // junto a los otros campos


        public void Iniciar(ModeloGrilla grilla, Vector2Int celdaInicial)
        {
            _grilla = grilla;
            PonerEnCelda(celdaInicial);
        }

        private void Update()
        {
               if (!Habilitado) return;                    // primera línea del Update


            _temporizador -= Time.deltaTime;
            if (_temporizador > 0f) return;

            Vector2Int direccion = LeerDireccion();
            if (direccion == Vector2Int.zero) return;

            Vector2Int destino = Celda + direccion;
            if (_grilla.EstaDentro(destino))
                PonerEnCelda(destino);

            _temporizador = intervaloMovimiento;
        }

        private Vector2Int LeerDireccion()
        {
            int x = (int)Input.GetAxisRaw("Horizontal");
            int y = (int)Input.GetAxisRaw("Vertical");

            if (x != 0) return new Vector2Int(x, 0); // sin diagonales
            if (y != 0) return new Vector2Int(0, y);
            return Vector2Int.zero;
        }

        private void PonerEnCelda(Vector2Int celda)
        {
            Celda = celda;
            transform.position = new Vector3(celda.x, celda.y, 0);
        }
    }
}