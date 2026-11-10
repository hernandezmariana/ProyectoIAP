using System.Collections.Generic;
using UnityEngine;

namespace ExplosionesIncrementales.Vistas
{
    public class VistaExplosion : MonoBehaviour
    {
        [SerializeField] private Sprite spriteCuadrado;
        [SerializeField] private Color colorPulso = new Color(0.4f, 0.9f, 1f, 0.8f);
        [SerializeField] private float duracion = 0.25f;

        public void Mostrar(List<Vector2Int> casillas)
        {
            foreach (var casilla in casillas)
            {
                var objeto = new GameObject("Pulso");
                objeto.transform.position = new Vector3(casilla.x, casilla.y, 0);
                objeto.transform.localScale = new Vector3(0.9f, 0.9f, 1f);

                var render = objeto.AddComponent<SpriteRenderer>();
                render.sprite = spriteCuadrado;
                render.color = colorPulso;
                render.sortingOrder = 5;

                Destroy(objeto, duracion);
            }
        }
    }
}