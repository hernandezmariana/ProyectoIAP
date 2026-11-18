using UnityEngine;

namespace ExplosionesIncrementales.Vistas
{
    [RequireComponent(typeof(Camera))]
    public class CamaraAdaptable : MonoBehaviour
    {
        [SerializeField] private int ancho = 15;
        [SerializeField] private int alto = 15;
        [SerializeField] private float margen = 1f;
        [SerializeField] private float espacioInferior = 5f;   // unidades libres abajo (vertical) para los controles

        private Camera _camara;
        private int _anchoPantalla, _altoPantalla;

        private void Awake()
        {
            _camara = GetComponent<Camera>();
            _camara.orthographic = true;
            Ajustar();
        }

        private void Update()
        {
            if (Screen.width != _anchoPantalla || Screen.height != _altoPantalla) Ajustar();
        }

        private void Ajustar()
        {
            _anchoPantalla = Screen.width;
            _altoPantalla = Screen.height;

            float aspecto = (float)_anchoPantalla / _altoPantalla;
            bool vertical = aspecto < 1f;
            float extraAbajo = vertical ? espacioInferior : 0f;

            float sizePorAlto = (alto + margen * 2f + extraAbajo) / 2f;
            float sizePorAncho = (ancho + margen * 2f) / (2f * aspecto);
            float size = Mathf.Max(sizePorAlto, sizePorAncho);
            _camara.orthographicSize = size;

            float x = (ancho - 1) / 2f;
            float y = (alto - 1) / 2f;
            if (vertical)
            {
                // Grilla pegada arriba; el espacio sobrante queda abajo para los controles
                float techoGrilla = alto - 0.5f + margen;
                y = techoGrilla - size;
            }
            transform.position = new Vector3(x, y, -10f);
        }
    }
}