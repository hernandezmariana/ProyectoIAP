using System;
using UnityEngine;

namespace ExplosionesIncrementales.Modelo
{
    public class ServicioGuardadoPlayerPrefs : IServicioGuardado
    {
        private const string Clave = "guardado_v1";
        private const int CantidadMejoras = 5;

        public bool HayGuardado
        {
            get { return Cargar() != null; }
        }

        public void Guardar(DatosGuardado datos)
        {
            PlayerPrefs.SetString(Clave, JsonUtility.ToJson(datos));
            PlayerPrefs.Save();
        }

        public DatosGuardado Cargar()
        {
            if (!PlayerPrefs.HasKey(Clave)) return null;
            try
            {
                var datos = JsonUtility.FromJson<DatosGuardado>(PlayerPrefs.GetString(Clave));
                return EsConsistente(datos) ? datos : null;
            }
            catch (Exception) { return null; }
        }

        public void Borrar()
        {
            PlayerPrefs.DeleteKey(Clave);
            PlayerPrefs.Save();
        }

        // Garantiza que un guardado corrupto nunca rompa la partida
        private bool EsConsistente(DatosGuardado d)
        {
            if (d == null || d.nivelesMejoras == null) return false;
            if (d.nivelesMejoras.Length != CantidadMejoras) return false;
            if (d.ronda < 1 || d.ronda > SistemaImpuestos.RondaFinal) return false;
            if (d.dinero < 0 || d.proximoImpuesto <= 0) return false;
            foreach (int nivel in d.nivelesMejoras) if (nivel < 0) return false;
            return true;
        }
    }
}