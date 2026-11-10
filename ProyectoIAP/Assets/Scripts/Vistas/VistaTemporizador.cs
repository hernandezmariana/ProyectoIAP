using UnityEngine;
using UnityEngine.UI;
using TMPro; // 1. Cambiamos UnityEngine.UI por TMPro

namespace ExplosionesIncrementales.Vistas
{
    public class VistaTemporizador : MonoBehaviour
    {
[SerializeField] private TMP_Text textoTiempo;
       public void Mostrar(int ronda, float tiempo, int dinero)
        {
            textoTiempo.text = "RONDA " + ronda
                + "   T: " + Mathf.CeilToInt(Mathf.Max(0, tiempo))
                + "   $" + dinero;
        }
    }
}


