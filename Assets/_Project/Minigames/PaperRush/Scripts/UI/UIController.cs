using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Timer")]
    public int timeSeconds;
    protected float time;
    protected bool isPaused = false;
    protected int helpIndex = 0;

    [Header("UI References")]
    public TextMeshProUGUI timerText; 
    public TextMeshProUGUI date;
    public TextMeshProUGUI newPoints;

    public GameObject fade;
    public GameObject objetives;
    public GameObject newEndUI;
    public GameObject mainUI;
    public GameObject GameStartUI;
    public GameObject helpUI;
    public List<GameObject> helpPage = new List<GameObject>();

    [Header("Controllers")]
    public CharacterController notebook;

    [Header("Buttons")]
    public Button book;
    public Button mainMenu;
    public Button help;

    public Button home;
    void Start()
    {
        time = timeSeconds;
        DateTime day = GameController.Instance.Retrieve<Character>().calendarDate;
        CultureInfo spanishCulture = new CultureInfo("es-ES");
        date.text = day.ToString("dd 'de' MMMM 'de' yyyy", spanishCulture);
        notebook.updateText();
    }

    void Update()
    {
        if (helpUI.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                closeHelp();
            }
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
            if (isPaused == false)
            {
                StartCoroutine(MatchTime());
            }
        }

       
    }

    public void pauseTime()
    {
        isPaused = true;
    }

    public void unpauseTime()
    {
        isPaused = false;
        StartCoroutine(MatchTime());
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

    public void homeClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void helpClick()
    {
        pauseTime();
        helpUI.SetActive(true);
        helpPage[helpIndex].SetActive(true);
        home.gameObject.SetActive(false);
        help.gameObject.SetActive(false);
        book.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        GameController.Instance.fxManager.pageFlipSound();
        GameController.Instance.player.animatorController.SetBool("Moving", false);


    }
    

    public void helpNext()
    {
        helpPage[helpIndex].SetActive(false);
        helpIndex++;
        helpPage[helpIndex].SetActive(true);
        GameController.Instance.fxManager.pageFlipSound();
    }

    public void helpBack()
    {
        helpPage[helpIndex].SetActive(false);
        helpIndex--;
        helpPage[helpIndex].SetActive(true);
        GameController.Instance.fxManager.pageFlipSound();
    }

    public void closeHelp()
    {
        helpUI.SetActive(false);
        unpauseTime();
        home.gameObject.SetActive(true);
        help.gameObject.SetActive(true);
        book.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(true);
        GameController.Instance.fxManager.pageFlipSound();

    }

    public void Finale()
    {
        fade.SetActive(true);
        objetives.SetActive(false);
        newEndUI.SetActive(true);
        mainUI.SetActive(false);
    }
}
