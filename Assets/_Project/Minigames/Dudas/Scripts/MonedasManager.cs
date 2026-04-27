using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MonedasManager : MonoBehaviour
{

    public static MonedasManager Instance;
    public Text textoMonedas;

    private string baseUrl = "http://127.0.0.1:5000/users/monedas/";

    private int monedasFallback = 0;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(GetMonedas());
    }

    public void getMonedas()
    {
        StartCoroutine(GetMonedas());
    }

    IEnumerator GetMonedas()
    {
  
        int userId = PlayerPrefs.GetInt("user_id", 0);


        if (userId == 0)
        {
            Debug.LogError("No hay user_id guardado");
            textoMonedas.text = "Monedas: 0";
            yield break;
        }

        string url = baseUrl + userId;

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