using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MonedasManager : MonoBehaviour
{
    public static MonedasManager Instance;

    [Header("API")]
    private string baseUrl = "http://127.0.0.1:5000/users/monedas/";

    [Header("Estado")]
    private int monedas;

    public int GetMonedas()
    {
        return monedas;
    }

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

    private void Start()
    {
        StartCoroutine(LoadMonedasFromAPI());
    }

    public void RefreshMonedas()
    {
        StartCoroutine(LoadMonedasFromAPI());
    }

    IEnumerator LoadMonedasFromAPI()
    {
        int userId = PlayerPrefs.GetInt("user_id", 0);

        if (userId == 0)
        {
            Debug.LogError("No hay user_id");
            yield break;
        }

        string url = baseUrl + userId;

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Monedas data = JsonUtility.FromJson<Monedas>(request.downloadHandler.text);

            monedas = data.monedas;

            PlayerPrefs.SetInt("User_Monedas", monedas);
            PlayerPrefs.Save();

            Debug.Log("Monedas actualizadas: " + monedas);
            
            FindAnyObjectByType<MonedasUI>()?.Actualizar();
        }
        else
        {
            Debug.LogWarning("Error API, usando cache local");

            monedas = PlayerPrefs.GetInt("User_Monedas", 0);
        }
    }
}