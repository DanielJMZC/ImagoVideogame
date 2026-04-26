using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MonedasManager : MonoBehaviour
{
    public Text textoMonedas;

    private string url = "http://localhost:5000/users/monedas/1";

    private int monedasFallback = 0;

    void Start()
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

            Monedas data = JsonUtility.FromJson<Monedas>(json);

            monedasFallback = data.monedas; 
        }
        else
        {
            Debug.LogWarning("Usando monedas locales (sin conexión)");
        }


        textoMonedas.text = "Monedas: " + monedasFallback;
    }
}