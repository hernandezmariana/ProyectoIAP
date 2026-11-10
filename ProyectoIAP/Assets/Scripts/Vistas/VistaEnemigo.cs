using UnityEngine;
using ExplosionesIncrementales.Modelo;

namespace ExplosionesIncrementales.Vistas
{
    public class VistaEnemigo : MonoBehaviour
    {
        public ModeloEnemigo Modelo { get; private set; }

        public void Iniciar(ModeloEnemigo modelo, Sprite sprite, Color color)
        {
            Modelo = modelo;
            var render = gameObject.AddComponent<SpriteRenderer>();
            render.sprite = sprite;
            render.color = color;
            render.sortingOrder = 2;
            transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            Actualizar();
        }

        public void Actualizar()
        {
            transform.position = new Vector3(Modelo.Celda.x, Modelo.Celda.y, 0);
        }
    }
}