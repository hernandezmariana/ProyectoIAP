using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ExplosionesIncrementales.Modelo;

namespace ExplosionesIncrementales.Vistas
{
    public class VistaTrivia : MonoBehaviour, IVistaTrivia
    {
        [SerializeField] private TMP_Text textoPrincipal;

        [Header("Grupos (se muestra uno por vez)")]
        [SerializeField] private GameObject grupoDecision;
        [SerializeField] private GameObject grupoPregunta;
        [SerializeField] private GameObject grupoResultado;

        [Header("Botones")]
        [SerializeField] private Button botonParticipar;
        [SerializeField] private Button botonNoParticipar;
        [SerializeField] private Button[] botonesOpcion; // 4 botones
        [SerializeField] private Button botonContinuar;

        public event Action<bool> AlDecidir;
        public event Action<int> AlResponder;
        public event Action AlContinuar;

        private void Awake()
        {
            botonParticipar.onClick.AddListener(() => { if (AlDecidir != null) AlDecidir(true); });
            botonNoParticipar.onClick.AddListener(() => { if (AlDecidir != null) AlDecidir(false); });
            botonContinuar.onClick.AddListener(() => { if (AlContinuar != null) AlContinuar(); });

            for (int i = 0; i < botonesOpcion.Length; i++)
            {
                int indice = i; // copia para el closure
                botonesOpcion[i].onClick.AddListener(() => { if (AlResponder != null) AlResponder(indice); });
            }
        }

        public void MostrarDecision(int ganancias)
        {
            gameObject.SetActive(true);
            textoPrincipal.text = "TRIVIA\nGanaste $" + ganancias + " esta ronda.\n\n"
                + "Si respondés bien: $" + (ganancias * 2) + "\n"
                + "Si fallás: $" + (ganancias / 2) + "\n\n¿Querés arriesgar?";
            ActivarSoloGrupo(grupoDecision);
        }

        public void MostrarPregunta(PreguntaTrivia pregunta, int ganancias)
        {
            textoPrincipal.text = "En juego: $" + ganancias + "\n\n" + pregunta.enunciado;

            for (int i = 0; i < botonesOpcion.Length; i++)
            {
                bool hayOpcion = i < pregunta.opciones.Length;
                botonesOpcion[i].gameObject.SetActive(hayOpcion);
                if (hayOpcion)
                    botonesOpcion[i].GetComponentInChildren<TMP_Text>().text = pregunta.opciones[i];
            }
            ActivarSoloGrupo(grupoPregunta);
        }

        public void MostrarResultado(ResultadoTrivia resultado, int antes, int despues)
        {
            string titulo;
            switch (resultado)
            {
                case ResultadoTrivia.Correcta:   titulo = "¡CORRECTA!"; break;
                case ResultadoTrivia.Incorrecta: titulo = "INCORRECTA"; break;
                default:                         titulo = "No arriesgaste"; break;
            }
            textoPrincipal.text = titulo + "\n\nGanancias de la ronda: $" + antes + " → $" + despues;
            ActivarSoloGrupo(grupoResultado);
        }

        public void Ocultar() { gameObject.SetActive(false); }

        private void ActivarSoloGrupo(GameObject grupo)
        {
            grupoDecision.SetActive(grupo == grupoDecision);
            grupoPregunta.SetActive(grupo == grupoPregunta);
            grupoResultado.SetActive(grupo == grupoResultado);
        }
    }
}