using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using System.Collections;

public class TriviaRitmo : MonoBehaviour
{

    [Header("UI")]
    public GameObject canvas;
    public Text textoPregunta;
    public Text[] textosRespuestas;

    public PatronUI[] patronesUI;
    public PatronUI inputUI;


    [Header("Preguntas")]
    public int npcId;
    public List<Pregunta> preguntas;

    public GameObject panelCorrecto;
    public GameObject panelIncorrecto;

    private int preguntaActual = 0;
    private int combopuntuacion = 0;
    private int combopatron = 0;

    private List<Direccion> inputJugador = new List<Direccion>();
    private int patronSize = 3;

    public PlayerControl player;


    [Header("Textos")]
    public Text textoPuntuacion;
    private int puntuacion = 0;
    public Text TextCalificacion;


    [Header("Ritmo")]
    public float bpm = 74f;
    public float ventanaPerfect = 0.1f;
    public float ventanaGood = 0.2f;
    public Image imagenIndicador;

    private float tiempoPorBeat;


    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip musica;

    public AudioClip sfxCorrect;
    public AudioClip sfxFalse;

    private bool juegoActivo = false;


    [Header("Resumen")]
    public GameObject canvasResumen;

    public Text textoCorrectas;
    public Text textoIncorrectas;
    public Text textoPerfects;
    public Text textoGoods;
    public Text textoMiss;
    public Text textoPuntuacionFinal;
    public Text textoMonedas;

    private int correctas = 0;
    private int incorrectas = 0;
    private int perfects = 0;
    private int goods = 0;
    private int miss = 0;


    void Start()
    {
        canvas.SetActive(false);
        audioSource.pitch = 0.8f;
    }

    public void StartGame()
    {
        canvas.SetActive(true);
        preguntaActual = 0;
        combopatron = 0;
        combopuntuacion = 0;
        patronSize = 3;
        puntuacion = 0;


        correctas = 0;
        incorrectas = 0;
        perfects = 0;
        goods = 0;
        miss = 0;

        canvasResumen.SetActive(false);

        tiempoPorBeat = 60f / bpm;


        inputJugador = new List<Direccion>();
        textoPuntuacion.text = "Puntuación: 0";

        audioSource.Stop();
        audioSource.clip = musica;
        audioSource.time = 0f;
        audioSource.loop = true;
        audioSource.PlayDelayed(0.2f);

        juegoActivo = false;
        Invoke(nameof(ActivarJuego), 0.2f);

        GenerarPatrones();
        MostrarPregunta();
        inputUI.MostrarPatron(inputJugador);

        Debug.Log("Canvas usado: " + canvas.name);
    }

    public void StartGameFromAPI()
    {
        StartCoroutine(
            DialogService.Instance.GetPreguntas(npcId, (result) =>
            {
                if (result != null)
                {
                    preguntas = result;

                    StartGame(); 
                }
                else
                {
                    Debug.LogError("No se pudieron cargar preguntas");
                }
            })
        );
    }


    void ActivarJuego()
    {
        juegoActivo = true;
        MusicManager.Instance.PauseMusic();
    }

    void Update()
    {
        if (!canvas.activeSelf && !canvasResumen.activeSelf) return;

        if (canvas.activeSelf && juegoActivo)
        {
            LeerInput();
            ActualizarIndicadorRitmo();
        }


        if (canvasResumen.activeSelf && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Cerrar resumen");
            CerrarResumen();
        }
    }

    void LeerInput()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
            AgregarInput(Direccion.Arriba);

        if (Keyboard.current.sKey.wasPressedThisFrame)
            AgregarInput(Direccion.Abajo);

        if (Keyboard.current.aKey.wasPressedThisFrame)
            AgregarInput(Direccion.Izquierda);

        if (Keyboard.current.dKey.wasPressedThisFrame)
            AgregarInput(Direccion.Derecha);
    }

    void AgregarInput(Direccion dir)
    {
        if (!juegoActivo) return;

        float offset = 0.2f;
        float tiempoActual = audioSource.time - offset;
        float beat = Mathf.Round(tiempoActual / tiempoPorBeat) * tiempoPorBeat;
        float diferencia = Mathf.Abs(tiempoActual - beat);


        if (diferencia < ventanaPerfect)
        {
            combopuntuacion += 3;
            perfects++;
            TextCalificacion.text = "Perfecto";
        }
        else if (diferencia < ventanaGood)
        {
            combopuntuacion += 1;
            goods++;
            TextCalificacion.text = "Bien";
        }
        else
        {
            miss++;
            TextCalificacion.text = "Mal";
        }

        inputJugador.Add(dir);
        inputUI.MostrarPatron(inputJugador);

        if (inputJugador.Count >= patronSize)
        {
            EvaluarRespuesta();
            Debug.Log("cerrar resumen");
        }
    }

    void EvaluarRespuesta()
    {
        Pregunta p = preguntas[preguntaActual];

        bool correcto = false;


        foreach (var r in p.respuestas)
        {
            if (CompararListas(inputJugador, r.patron))
            {
                if (r.esCorrecta)
                {
                    correcto = true;
                }
                break;
            }
        }


        int puntuacionRespuesta = 0;

        if (correcto)
        {
            correctas++;
            puntuacionRespuesta = combopuntuacion;

            SFXManager.Instance.PlaySFX(sfxCorrect);

            StartCoroutine(MostrarFeedback(panelCorrecto));
        }
        else
        {
            incorrectas++;

            SFXManager.Instance.PlaySFX(sfxFalse);

            StartCoroutine(MostrarFeedback(panelIncorrecto));
        }

        puntuacion += puntuacionRespuesta;
        textoPuntuacion.text = $"Puntuación: {puntuacion}";


        if (puntuacionRespuesta > 0)
        {
            combopatron++;
            patronSize = Mathf.Min(6, 3 + combopatron);
        }
        else
        {
            combopatron = 0;
            patronSize = 3;
        }


        combopuntuacion = 0;

        SiguientePregunta();
    }

    bool CompararListas(List<Direccion> a, List<Direccion> b)
    {
        if (a.Count != b.Count) return false;

        for (int i = 0; i < a.Count; i++)
        {
            if (a[i] != b[i]) return false;
        }

        return true;
    }

    void SiguientePregunta()
    {
        inputJugador.Clear();
        inputUI.MostrarPatron(inputJugador);

        preguntaActual++;

        if (preguntaActual >= preguntas.Count)
        {
            EndGame();
            return;
        }

        GenerarPatrones();
        MostrarPregunta();
    }

    public void EndGame()
    {
        canvas.SetActive(false);
        audioSource.Stop();


        Debug.Log("Juego terminado. Puntuación: " + puntuacion);

        int monedas = puntuacion;

        int actuales = PlayerPrefs.GetInt("User_Monedas", 0);
        actuales += monedas;

        PlayerPrefs.SetInt("User_Monedas", actuales);

        int userId = PlayerPrefs.GetInt("user_id", 1);

        StartCoroutine(EnviarMonedas(userId, monedas));

        MostrarResumen();

        MonedasManager.Instance.RefreshMonedas();
        GameWinService.Instance.EnviarGameWin(2, monedas);
        
    }

    void GenerarPatrones()
    {
        foreach (var r in preguntas[preguntaActual].respuestas)
        {
            r.patron = new List<Direccion>();

            for (int i = 0; i < patronSize; i++)
            {
                r.patron.Add((Direccion)Random.Range(0, 4));
            }
        }
    }

    void MostrarPregunta()
    {
        Pregunta p = preguntas[preguntaActual];

        textoPregunta.text = p.enunciado;

        for (int i = 0; i < p.respuestas.Count; i++)
        {
            textosRespuestas[i].text = p.respuestas[i].texto;
            patronesUI[i].MostrarPatron(p.respuestas[i].patron);
        }
    }

    void ActualizarIndicadorRitmo()
    {
        float offset = 0.2f;
        float tiempoActual = audioSource.time - offset;

        float beat = Mathf.Round(tiempoActual / tiempoPorBeat) * tiempoPorBeat;
        float diff = Mathf.Abs(tiempoActual - beat);

        if (diff < ventanaPerfect)
        {
            imagenIndicador.color = new Color(0.6f, 0.9f, 0.6f);
        }
        else if (diff < ventanaGood)
        {
            imagenIndicador.color = new Color(1f, 0.95f, 0.6f);
        }
        else
        {
            imagenIndicador.color = new Color(1f, 0.6f, 0.6f);
        }


    }

    void MostrarResumen()
    {
        canvasResumen.SetActive(true);

        textoCorrectas.text = "" + correctas;
        textoIncorrectas.text = "" + incorrectas;
        textoPerfects.text = "Perfectos: " + perfects;
        textoGoods.text = "Buenas: " + goods;
        textoMiss.text = "Fallas: " + miss;
        textoPuntuacionFinal.text = "Puntuación Final: " + puntuacion;

        int monedas = puntuacion;
        textoMonedas.text = "Monedas: " + monedas;

        MusicManager.Instance.ResumeMusic();
    }

    void CerrarResumen()
    {
        canvasResumen.SetActive(false);
        Debug.Log("Cerrado");
        Interactable.interactionLocked = false;
        player.inAction = false;
        NPCTrivia.currentNPC = null;

        if (player == null) Debug.LogError("PLAYER ES NULL");
        if (MonedasManager.Instance == null) Debug.LogError("MONEDAS MANAGER ES NULL");

        Debug.Log("player: " + player);
        Debug.Log("MonedasManager: " + MonedasManager.Instance);

        FindAnyObjectByType<MonedasManager>()?.RefreshMonedas();
    }

    IEnumerator MostrarFeedback(GameObject panel)
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(1f);
        panel.SetActive(false);
    }


    IEnumerator EnviarMonedas(int userId, int monedas)
    {
        string url = "http://127.0.0.1:5530/users/monedas/add";

        string json = JsonUtility.ToJson(new MonedasRequest(userId, monedas));

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
}