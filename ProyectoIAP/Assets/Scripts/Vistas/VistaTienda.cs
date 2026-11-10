using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ExplosionesIncrementales.Modelo;

namespace ExplosionesIncrementales.Vistas
{
    public class VistaTienda : MonoBehaviour, IVistaTienda
    {
        [SerializeField] private TMP_Text textoInfo;
        [SerializeField] private Transform contenedor;
        [SerializeField] private Button plantillaMejora;
        [SerializeField] private Button botonContinuar;

        public event Action<TipoMejora> AlComprar;
        public event Action AlContinuar;

        private void Awake()
        {
            plantillaMejora.gameObject.SetActive(false);
            botonContinuar.onClick.AddListener(() => { if (AlContinuar != null) AlContinuar(); });
        }

        public void Mostrar() { gameObject.SetActive(true); }
        public void Ocultar() { gameObject.SetActive(false); }

        public void ActualizarDatos(int ronda, int dinero, int montoImpuesto, int rondasFaltantes, List<DatosMejora> mejoras)
        {
            textoInfo.text = "TIENDA - Ronda " + ronda + "\nDinero: $" + dinero
                + "\nPróximo impuesto: $" + montoImpuesto
                + " (en " + rondasFaltantes + " rondas)";

            foreach (Transform hijo in contenedor)
                if (hijo != plantillaMejora.transform) Destroy(hijo.gameObject);

            foreach (var datos in mejoras)
            {
                var boton = Instantiate(plantillaMejora, contenedor);
                boton.gameObject.SetActive(true);
                boton.GetComponentInChildren<TMP_Text>().text =
                    datos.Nombre + " (Nv " + datos.Nivel + ")  -  $" + datos.Costo;
                boton.interactable = datos.Alcanza;
                var tipo = datos.Tipo;
                boton.onClick.AddListener(() => { if (AlComprar != null) AlComprar(tipo); });
            }
        }
    }
}