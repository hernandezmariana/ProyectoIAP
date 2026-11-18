using UnityEngine;
using UnityEngine.EventSystems;

namespace ExplosionesIncrementales.Vistas
{
    public class BotonDireccion : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField] private Vector2Int direccion;

        public Vector2Int Direccion { get { return direccion; } }
        public bool Presionado { get; private set; }

        public void OnPointerDown(PointerEventData datos) { Presionado = true; }
        public void OnPointerUp(PointerEventData datos) { Presionado = false; }
        public void OnPointerExit(PointerEventData datos) { Presionado = false; }
        private void OnDisable() { Presionado = false; }
    }
}