using UnityEngine;

namespace ExplosionesIncrementales.Vistas
{
    public class EntradaTeclado : IEntradaDireccion
    {
        public Vector2Int LeerDireccion()
        {
            int x = (int)Input.GetAxisRaw("Horizontal");
            int y = (int)Input.GetAxisRaw("Vertical");

            if (x != 0) return new Vector2Int(x, 0);   // sin diagonales
            if (y != 0) return new Vector2Int(0, y);
            return Vector2Int.zero;
        }
    }
}