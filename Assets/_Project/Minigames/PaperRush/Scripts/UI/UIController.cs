using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public int timeSeconds;
    protected float time;
    public TextMeshProUGUI timerText; 
    public TextMeshProUGUI date;
    public TextMeshProUGUI points;

    public GameObject fade;
    public GameObject objetives;
    public GameObject endUI;
    public GameObject mainUI;

    void Start()
    {
        time = timeSeconds;
        DateTime day = GameController.Instance.character.calendarDate;
        date.text = day.ToString("MMMM dd, yyyy");
        StartCoroutine(MatchTime());

        
    }

     IEnumerator MatchTime()
    {
        yield return new WaitForSeconds(1);
        time -= 1;
        UpdateTimerUI();

        if (time <= 0)
        {
            GameController.Instance.EndGame();
        } else
        {
            StartCoroutine(MatchTime());
        }

       
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(time/60f);
        int seconds = Mathf.FloorToInt(time%60f);

        if (timerText != null)
        {
            timerText.text = $"{minutes:0}:{seconds:00}";
        }

    }

    public void isFading(Boolean _isFading)
    {
        if (!_isFading)
        {
            fade.SetActive(false);
            objetives.SetActive(false);
        } else
        {
            fade.SetActive(true);
            objetives.SetActive(true);
        }
    }
    public void GameEndUI(int _points)
    {
        fade.SetActive(true);
        objetives.SetActive(false);
        endUI.SetActive(true);
        mainUI.SetActive(false);

        points.text = _points.ToString();

    }
}
