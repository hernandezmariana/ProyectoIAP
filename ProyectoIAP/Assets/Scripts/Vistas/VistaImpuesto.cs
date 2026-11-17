/*using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ExplosionesIncrementales.Vistas
{
    public class VistaImpuesto : MonoBehaviour, IVistaImpuesto
    {
        [SerializeField] private TMP_Text texto;
        [SerializeField] private Button botonPagar;

        public event Action AlPagar;

        private void Awake()
        {
            botonPagar.onClick.AddListener(() => { if (AlPagar != null) AlPagar(); });
        }

        /*public void Mostrar(int ronda, int monto, int dinero)
        {
            texto.text = "IMPUESTO - Ronda " + ronda
                + "\nSe cobra: $" + monto + "\nTu dinero: $" + dinero;
            gameObject.SetActive(true);
        }//

        public void Mostrar(string texto, string textoBoton)
{
    this.texto.text = texto;
    botonPagar.GetComponentInChildren<TMP_Text>().text = textoBoton;
    gameObject.SetActive(true);
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
    public class VistaImpuesto : MonoBehaviour, IVistaImpuesto
    {
        [SerializeField] private TMP_Text texto;
        [SerializeField] private Button botonPagar;

        public event Action AlPagar;

        private void Awake()
        {
            botonPagar.onClick.AddListener(() => { if (AlPagar != null) AlPagar(); });
        }

        public void Mostrar(string textoImpuesto, string textoBoton)
        {
            texto.text = textoImpuesto;
            botonPagar.GetComponentInChildren<TMP_Text>().text = textoBoton;
            gameObject.SetActive(true);
        }

        public void Ocultar() { gameObject.SetActive(false); }
    }
}