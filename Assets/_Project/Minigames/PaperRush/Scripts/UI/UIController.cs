using System;
using System.Collections;
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
    public TextMeshProUGUI points;
    public GameObject fade;
    public GameObject objetives;
    public GameObject endUI;
    public GameObject mainUI;
    public GameObject GameStartUI;

    [Header("Controllers")]
    public CharacterController notebook;


    void Start()
    {
        time = timeSeconds;
        DateTime day = GameController.Instance.character.calendarDate;
        date.text = day.ToString("MMMM dd, yyyy");
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
    public void GameEndUI(int _points)
    {
        points.text = _points.ToString();
        StartCoroutine(GameEndUIRoutine(5));
    }

    IEnumerator GameEndUIRoutine(int second)
    {
        fade.SetActive(true);
        objetives.SetActive(false);
        endUI.SetActive(true);
        mainUI.SetActive(false);
        yield return new WaitForSeconds(second);

        UnityEditor.EditorApplication.isPlaying = false;
        
    }

    public void GameStart()
    {
       StartCoroutine(GameStartRoutine(3));
    }

    IEnumerator GameStartRoutine(int second)
    
    {
        GameController.Instance.fxManager.PauseMusic();
        GameStartUI.SetActive(true);
        yield return new WaitForSeconds(second);
        GameStartUI.SetActive(false);
        GameController.Instance.fxManager.ResumeMusic();
        StartCoroutine(MatchTime());
    }
}
