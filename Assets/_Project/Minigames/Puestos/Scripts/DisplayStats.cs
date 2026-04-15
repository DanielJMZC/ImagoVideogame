using UnityEngine;
using TMPro;

public class DisplayStats : MonoBehaviour
{
    public TextMeshProUGUI amountPoints;
    string amountText = "Puntos: ";

    void Start()
    {
        int score = PlayerPrefs.GetInt("ScoreFinal", 0);
        amountPoints.text = amountText + score.ToString();
    }
}