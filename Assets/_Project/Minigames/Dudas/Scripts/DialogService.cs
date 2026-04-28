using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class DialogService : MonoBehaviour
{
    public static DialogService Instance;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator GetDialogos(int npcId, System.Action<List<Dialogo>> callback)
    {
        //string url = $"http://127.0.0.1:5530/npc/{npcId}/dialogos";
        string url = $"http://10.14.255.43:5530/npc/{npcId}/dialogos";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al obtener dialogos");
            callback(null);
        }
        else
        {
            string json = request.downloadHandler.text;

            string wrapped = "{ \"data\": " + json + "}";

            DialogoList lista = JsonUtility.FromJson<DialogoList>(wrapped);

            callback(lista.data);
        }
    }

    public IEnumerator GetPreguntas(int npcId, System.Action<List<Pregunta>> callback)
    {
        //string url = $"http://127.0.0.1:5530/npc/{npcId}/preguntas";
        string url = $"http://10.14.255.43:5530/npc/{npcId}/preguntas";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al obtener preguntas");
            callback(null);
        }
        else
        {
            string json = request.downloadHandler.text;

            string wrapped = "{ \"data\": " + json + "}";

            PreguntaList lista = JsonUtility.FromJson<PreguntaList>(wrapped);

            callback(lista.data);
        }
    }


}

[System.Serializable]
public class PreguntaList
{
    public List<Pregunta> data;
}

[System.Serializable]
public class DialogoList
{
    public List<Dialogo> data;
}
