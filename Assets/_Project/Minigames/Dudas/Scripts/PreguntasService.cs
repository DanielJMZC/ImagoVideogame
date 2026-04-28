using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
public class PreguntasService
{
    public IEnumerator GetPreguntas(int npcId, System.Action<List<Pregunta>> callback)
{
    string url = $"http://127.0.0.1:5530/npc/{npcId}/preguntas";

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

[System.Serializable]
public class PreguntaList
{
    public List<Pregunta> data;
}
}
