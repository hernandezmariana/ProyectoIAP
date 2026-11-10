using UnityEngine;
using ExplosionesIncrementales.Modelo;

namespace ExplosionesIncrementales.Vistas
{
    public class VistaMoneda : MonoBehaviour
    {
        public ModeloMoneda Modelo { get; private set; }

        public void Iniciar(ModeloMoneda modelo, Sprite sprite)
        {
            Modelo = modelo;
            var render = gameObject.AddComponent<SpriteRenderer>();
            render.sprite = sprite;
            render.color = new Color(1f, 0.85f, 0.1f); // dorado
            render.sortingOrder = 1;
            transform.position = new Vector3(modelo.Celda.x, modelo.Celda.y, 0);
            transform.localScale = new Vector3(0.4f, 0.4f, 1f);
        }
    }
}