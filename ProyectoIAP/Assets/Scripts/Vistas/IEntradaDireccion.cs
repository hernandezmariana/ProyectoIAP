using UnityEngine;

namespace ExplosionesIncrementales.Vistas
{
    public interface IEntradaDireccion
    {
        Vector2Int LeerDireccion();   // (0,0) si no hay nada presionado
    }
}