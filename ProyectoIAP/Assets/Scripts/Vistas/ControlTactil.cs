using UnityEngine;

namespace ExplosionesIncrementales.Vistas
{
    public class ControlTactil : MonoBehaviour, IEntradaDireccion
    {
        [SerializeField] private BotonDireccion[] botones;

        public Vector2Int LeerDireccion()
        {
            foreach (var boton in botones)
                if (boton.Presionado) return boton.Direccion;
            return Vector2Int.zero;
        }
    }
}