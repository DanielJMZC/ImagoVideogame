using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // PUNTOS
    public TextMeshProUGUI amountPoints;
    string amountText = "Puntos: ";
    int currentScore = 0;

    // TIMER
    public TextMeshProUGUI timerText;
    float tiempo = 100f; // 2 minutos (puedes cambiar a 180 si quieres 3)

    // PANTALLA ROJA (parpadeo)
    public UnityEngine.UI.Image pantallaRoja;
    bool parpadeoActivo = false;
    float timerParpadeo = 0f;

    void Start()
    {
        ActiveScore();

        // MODIFIQUE ALGO AQUI 👉 asegurar que el rojo inicia apagado
        if (pantallaRoja != null)
        {
            pantallaRoja.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // ================= TIMER =================
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

        // ================= FIN DEL JUEGO =================
        if (tiempo == 0)
        {
            Debug.Log("Tiempo terminado");

            parpadeoActivo = false;

            if (pantallaRoja != null)
            {
                pantallaRoja.enabled = false;
            }
            PlayerPrefs.SetInt("ScoreFinal", currentScore);
            SceneManager.LoadScene("End");
            // opcional:
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // ================= ACTIVAR PARPADEO =================
        if (tiempo <= 60 && tiempo > 0)
        {
            parpadeoActivo = true;

            // MODIFIQUE ALGO AQUI 👉 activar objeto rojo
            if (pantallaRoja != null)
            {
                pantallaRoja.gameObject.SetActive(true);
            }
        }

        // ================= PARPADEO =================
        if (parpadeoActivo)
        {
            timerParpadeo += Time.deltaTime;

            if (timerParpadeo >= 0.5f)
            {
                pantallaRoja.enabled = !pantallaRoja.enabled;
                timerParpadeo = 0f;
            }
        }
    }

    // ================= SCORE =================
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