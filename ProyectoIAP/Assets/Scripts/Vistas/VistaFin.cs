using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ExplosionesIncrementales.Vistas
{
    public class VistaFin : MonoBehaviour, IVistaFin
    {
        [SerializeField] private TMP_Text texto;
        [SerializeField] private Button botonReiniciar;

        public event Action AlReiniciar;

        private void Awake()
        {
            botonReiniciar.onClick.AddListener(() => { if (AlReiniciar != null) AlReiniciar(); });
        }

        public void Mostrar(bool victoria, int ronda)
        {
            texto.text = victoria
                ? "¡VICTORIA!\nPagaste el impuesto de la ronda " + ronda
                : "GAME OVER\nBancarrota en la ronda " + ronda;
            gameObject.SetActive(true);
        }
    }
}