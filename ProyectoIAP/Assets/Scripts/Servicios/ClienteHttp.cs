/*using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace ExplosionesIncrementales.Servicios
{
    // Corrutinas de red con timeout propio (en WebGL UnityWebRequest.timeout no funciona)
    public class ClienteHttp : MonoBehaviour
    {
        public IEnumerator Get(string url, float timeoutSeg, Action<string> alTerminar)
        {
            using (var pedido = UnityWebRequest.Get(url))
            {
                yield return Esperar(pedido, timeoutSeg, alTerminar);
            }
        }

        public IEnumerator Post(string url, string json, float timeoutSeg, Action<string> alTerminar)
        {
            using (var pedido = new UnityWebRequest(url, "POST"))
            {
                pedido.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
                pedido.downloadHandler = new DownloadHandlerBuffer();
                pedido.SetRequestHeader("Content-Type", "application/json");
                yield return Esperar(pedido, timeoutSeg, alTerminar);
            }
        }

        // Devuelve el texto de la respuesta, o null si hubo error o se agotó el tiempo
        private IEnumerator Esperar(UnityWebRequest pedido, float timeoutSeg, Action<string> alTerminar)
        {
            pedido.SendWebRequest();

            float transcurrido = 0f;
            while (!pedido.isDone && transcurrido < timeoutSeg)
            {
                transcurrido += Time.unscaledDeltaTime;
                yield return null;
            }

            if (!pedido.isDone) { pedido.Abort(); alTerminar(null); yield break; }
            if (pedido.result != UnityWebRequest.Result.Success) { alTerminar(null); yield break; }
            alTerminar(pedido.downloadHandler.text);
        }
    }
}*/

using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace ExplosionesIncrementales.Servicios
{
    // Corrutinas de red con timeout propio (en WebGL UnityWebRequest.timeout no funciona)
    public class ClienteHttp : MonoBehaviour
    {
        public IEnumerator Get(string url, float timeoutSeg, Action<string> alTerminar)
        {
            using (var pedido = UnityWebRequest.Get(url))
            {
                yield return Esperar(pedido, timeoutSeg, alTerminar);
            }
        }

        public IEnumerator Post(string url, string json, float timeoutSeg, Action<string> alTerminar)
        {
            using (var pedido = new UnityWebRequest(url, "POST"))
            {
                pedido.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
                pedido.downloadHandler = new DownloadHandlerBuffer();
                pedido.SetRequestHeader("Content-Type", "application/json");
                yield return Esperar(pedido, timeoutSeg, alTerminar);
            }
        }

        // Devuelve el texto de la respuesta, o null si hubo error o se agotó el tiempo
        private IEnumerator Esperar(UnityWebRequest pedido, float timeoutSeg, Action<string> alTerminar)
        {
            pedido.SendWebRequest();

            float transcurrido = 0f;
            while (!pedido.isDone && transcurrido < timeoutSeg)
            {
                transcurrido += Time.unscaledDeltaTime;
                yield return null;
            }

            if (!pedido.isDone)
            {
                Debug.LogWarning("[HTTP] Tiempo agotado (" + timeoutSeg + " s) en " + pedido.url);
                pedido.Abort();
                alTerminar(null);
                yield break;
            }
            if (pedido.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning("[HTTP] Error " + pedido.responseCode + " (" + pedido.result + ") en "
                    + pedido.url + " | " + pedido.error + " | " + pedido.downloadHandler.text);
                alTerminar(null);
                yield break;
            }
            alTerminar(pedido.downloadHandler.text);
        }
    }
}