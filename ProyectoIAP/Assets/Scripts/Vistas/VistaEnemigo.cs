/*using UnityEngine;
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
}*/

using UnityEngine;
using ExplosionesIncrementales.Modelo;

namespace ExplosionesIncrementales.Vistas
{
    public class VistaEnemigo : MonoBehaviour
    {
        public ModeloEnemigo Modelo { get; private set; }
        private SpriteRenderer _render;
        private Color _colorBase;

        public void Iniciar(ModeloEnemigo modelo, Sprite sprite)
        {
            Modelo = modelo;
            _render = gameObject.AddComponent<SpriteRenderer>();
            _render.sprite = sprite;
            _colorBase = modelo.Definicion.Color;
            _render.color = _colorBase;
            _render.sortingOrder = 2;
            float e = modelo.Definicion.Escala;
            transform.localScale = new Vector3(e, e, 1f);
            Actualizar();
        }

        public void Actualizar()
        {
            transform.position = new Vector3(Modelo.Celda.x, Modelo.Celda.y, 0);
        }

        private void Update()
        {
            // El fantasma se vuelve invisible 2 de cada 5 segundos
            if (Modelo.Definicion.Nombre == "Fantasma")
            {
                bool invisible = (Time.time % 5f) > 3f;
                _render.color = invisible ? new Color(_colorBase.r, _colorBase.g, _colorBase.b, 0f) : _colorBase;
            }
        }
    }
}