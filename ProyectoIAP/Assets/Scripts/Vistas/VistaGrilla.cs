using UnityEngine;
using ExplosionesIncrementales.Modelo;

namespace ExplosionesIncrementales.Vistas
{
    public class VistaGrilla : MonoBehaviour
    {
        [SerializeField] private Sprite spriteCuadrado;
        [SerializeField] private Color colorLinea = new Color(0.2f, 0.2f, 0.25f);

        public void Dibujar(ModeloGrilla grilla)
        {
            // Líneas verticales
            for (int x = 0; x <= grilla.Ancho; x++)
                CrearLinea(new Vector2(x - 0.5f, (grilla.Alto - 1) / 2f),
                           new Vector2(0.03f, grilla.Alto));

            // Líneas horizontales
            for (int y = 0; y <= grilla.Alto; y++)
                CrearLinea(new Vector2((grilla.Ancho - 1) / 2f, y - 0.5f),
                           new Vector2(grilla.Ancho, 0.03f));
        }

        private void CrearLinea(Vector2 posicion, Vector2 escala)
        {
            var objeto = new GameObject("Linea");
            objeto.transform.SetParent(transform);
            objeto.transform.position = posicion;
            objeto.transform.localScale = escala;

            var render = objeto.AddComponent<SpriteRenderer>();
            render.sprite = spriteCuadrado;
            render.color = colorLinea;
            render.sortingOrder = -10; // detrás de todo
        }
    }
}