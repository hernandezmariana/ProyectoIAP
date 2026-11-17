/*using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ExplosionesIncrementales.Vistas
{
    public class VistaMenu : MonoBehaviour, IVistaMenu
    {
        [SerializeField] private TMP_Text textoTitulo;
        [SerializeField] private Button botonNueva;
        [SerializeField] private Button botonContinuar;
        [SerializeField] private Button botonIdioma;

        public event Action AlNuevaPartida;
        public event Action AlContinuar;
        public event Action AlCambiarIdioma;

        private void Awake()
        {
            botonNueva.onClick.AddListener(() => { if (AlNuevaPartida != null) AlNuevaPartida(); });
            botonContinuar.onClick.AddListener(() => { if (AlContinuar != null) AlContinuar(); });
            botonIdioma.onClick.AddListener(() => { if (AlCambiarIdioma != null) AlCambiarIdioma(); });
        }

        public void Mostrar(bool hayGuardado, TextosMenu textos)
        {
            gameObject.SetActive(true);
            botonContinuar.gameObject.SetActive(hayGuardado);
            ActualizarTextos(textos);
        }

        public void ActualizarTextos(TextosMenu textos)
        {
            textoTitulo.text = textos.Titulo;
            botonNueva.GetComponentInChildren<TMP_Text>().text = textos.NuevaPartida;
            botonContinuar.GetComponentInChildren<TMP_Text>().text = textos.Continuar;
            botonIdioma.GetComponentInChildren<TMP_Text>().text = textos.Idioma;
        }

        public void Ocultar() { gameObject.SetActive(false); }
    }
}*/


using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ExplosionesIncrementales.Vistas
{
    public class VistaMenu : MonoBehaviour, IVistaMenu
    {
        [SerializeField] private TMP_Text textoTitulo;
        [SerializeField] private Button botonNueva;
        [SerializeField] private Button botonContinuar;
        [SerializeField] private Button botonIdioma;

        public event Action AlNuevaPartida;
        public event Action AlContinuar;
        public event Action AlCambiarIdioma;

        private void Awake()
        {
            botonNueva.onClick.AddListener(() => { if (AlNuevaPartida != null) AlNuevaPartida(); });
            botonContinuar.onClick.AddListener(() => { if (AlContinuar != null) AlContinuar(); });
            botonIdioma.onClick.AddListener(() => { if (AlCambiarIdioma != null) AlCambiarIdioma(); });
        }

        public void Mostrar(bool hayGuardado, TextosMenu textos)
        {
            gameObject.SetActive(true);
            botonContinuar.gameObject.SetActive(hayGuardado);
            ActualizarTextos(textos);
        }

        public void ActualizarTextos(TextosMenu textos)
        {
            textoTitulo.text = textos.Titulo;
            botonNueva.GetComponentInChildren<TMP_Text>().text = textos.NuevaPartida;
            botonContinuar.GetComponentInChildren<TMP_Text>().text = textos.Continuar;
            botonIdioma.GetComponentInChildren<TMP_Text>().text = textos.Idioma;
        }

        public void Ocultar() { gameObject.SetActive(false); }
    }
}