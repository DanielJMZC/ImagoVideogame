using UnityEngine;
using TMPro;

public class DisplayStats : MonoBehaviour
{
    public TextMeshProUGUI amountPoints;
    public TextMeshProUGUI resultText;
    string amountText = "Puntos: ";

    void Start()
    {
        int score = PlayerPrefs.GetInt("ScoreFinal", 0);
        amountPoints.text = amountText + score.ToString();

        if (score >= 45)
        {
            resultText.text = "Ganaste!";
        }
        else
        {
            resultText.text = "Perdiste...";
        }
    }
}