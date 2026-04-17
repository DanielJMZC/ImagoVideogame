using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MonedasManager : MonoBehaviour
{
    public Text textoMonedas;

    private string url = "http://localhost:5000/users/monedas/1";

    void Update()
    {
        StartCoroutine(GetMonedas());
    }

    IEnumerator GetMonedas()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;

            Debug.Log(json);

            Monedas data = JsonUtility.FromJson<Monedas>(json);

            textoMonedas.text = "Monedas: " +data.monedas.ToString();
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }
}
