using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Timer")]
    public int timeSeconds;
    protected float time;

    [Header("UI References")]
    public TextMeshProUGUI timerText; 
    public TextMeshProUGUI date;
    public TextMeshProUGUI newPoints;

    public GameObject fade;
    public GameObject objetives;
    public GameObject newEndUI;
    public GameObject mainUI;
    public GameObject GameStartUI;
    public Button book;

    [Header("Controllers")]
    public CharacterController notebook;


    void Start()
    {
        time = timeSeconds;
        DateTime day = GameController.Instance.Retrieve<Character>().calendarDate;
        CultureInfo spanishCulture = new CultureInfo("es-ES");
        date.text = day.ToString("dd 'de' MMMM 'de' yyyy", spanishCulture);
        notebook.updateText();
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
   
    public void GameStart()
    {
       StartCoroutine(GameStartRoutine(3));
    }

    IEnumerator GameStartRoutine(int second)
    
    {
        GameStartUI.SetActive(true);
        yield return new WaitForSeconds(second);
        GameStartUI.SetActive(false);
        GameController.Instance.fxManager.ResumeMusic();
        StartCoroutine(MatchTime());

    }

    public void Finale()
    {
        fade.SetActive(true);
        objetives.SetActive(false);
        newEndUI.SetActive(true);
        mainUI.SetActive(false);
    }
}
