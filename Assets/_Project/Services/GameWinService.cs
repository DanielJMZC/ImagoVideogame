using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GameWinService : MonoBehaviour
{
    public static GameWinService Instance;

    private string url = "http://127.0.0.1:5530/users/gamewin";
    //private string url = "http://10.14.255.43:5530/users/gamewin";

    void Awake()
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

    public void EnviarGameWin(int gameId, int monedas)
    {
        int playerId = PlayerPrefs.GetInt("user_id", 0);

        if (playerId == 0)
        {
            Debug.LogError("No hay user_id");
            return;
        }

        StartCoroutine(PostGameWin(gameId, playerId, monedas));
    }

    IEnumerator PostGameWin(int gameId, int playerId, int monedas)
    {
        GameWinRequest data = new GameWinRequest
        {
            game_id = gameId,
            player_id = playerId,
            monedas = monedas
        };

        string json = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest(url, "POST");

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("GameWin enviado correctamente");
        }
        else
        {
            Debug.LogError("Error enviando GameWin: " + request.error);
        }
    }
}
