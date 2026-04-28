using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI amountPoints;
    string amountText = "Puntos: ";
    int currentScore = 0;

    public TextMeshProUGUI timerText;
    float tiempo = 120f;

    public UnityEngine.UI.Image pantallaRoja;
    bool parpadeoActivo = false;
    float timerParpadeo = 0f;

    bool alreadySent = false;

    void Start()
    {
        PlayerPrefs.SetInt("player_id", 1);
        ActiveScore();

        if (pantallaRoja != null)
        {
            pantallaRoja.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (tiempo > 0)
        {
            tiempo -= Time.deltaTime;
        }

        if (tiempo < 0)
        {
            tiempo = 0;
        }

        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);

        timerText.text = minutos.ToString("00") + ":" + segundos.ToString("00");

        if (tiempo == 0 && !alreadySent)
        {
            alreadySent = true;

            int playerId = PlayerPrefs.GetInt("user_id", 1);

            StartCoroutine(EndGameFlow(playerId, currentScore));
        }

        if (tiempo <= 60 && tiempo > 0)
        {
            parpadeoActivo = true;

            if (pantallaRoja != null)
            {
                pantallaRoja.gameObject.SetActive(true);
            }
        }

        if (parpadeoActivo && pantallaRoja != null)
        {
            timerParpadeo += Time.deltaTime;

            if (timerParpadeo >= 0.5f)
            {
                pantallaRoja.enabled = !pantallaRoja.enabled;
                timerParpadeo = 0f;
            }
        }
    }

    IEnumerator EndGameFlow(int playerId, int puntos)
    {
        yield return SendFinalScore(playerId, puntos);

        PlayerPrefs.SetInt("ScoreFinal", puntos);

        int actuales = PlayerPrefs.GetInt("User_Monedas", 0);
        actuales += puntos;

        PlayerPrefs.SetInt("User_Monedas", actuales);

        GameWinService.Instance.EnviarGameWin(3, puntos);

        SceneManager.LoadScene("End");
    }

    IEnumerator SendFinalScore(int playerId, int puntos)
    {
        // URL de mi API
        string url = "http://127.0.0.1:5000/users/monedas/add";

        string json = JsonUtility.ToJson(new MonedasRequest(playerId, puntos));

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Monedas enviadas correctamente");


            PlayerPrefs.SetInt("MonedasPendientes", 0);
        }
        else
        {
            Debug.LogWarning("No se pudo enviar. Guardando localmente...");

        }
    }

    public void ActiveScore()
    {
        amountPoints.text = amountText + "--";
    }

    public void AddPoints(int points)
    {
        currentScore += points;
        PrintScore();
    }

    public void reset(int points)
    {
        currentScore = points;
        PrintScore();
    }

    public void PrintScore()
    {
        amountPoints.text = amountText + currentScore.ToString();
    }

    public int GetScore()
    {
        return currentScore;
    }

    public float GetTiempo()
    {
        return tiempo;
    }
}