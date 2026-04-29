using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameEnd : MonoBehaviour
{
    public Dictionary<documentType, Document> documentSubmitted;

    public List<GameObject> gameObjectList = new List<GameObject>();
    public List<TextMeshProUGUI> endText = new List<TextMeshProUGUI>();
    public List<GameObject> gameObjectPrefabs = new List<GameObject>();
    public List<documentType> types = new List<documentType>()
    {
        documentType.Passport,
        documentType.Visa,
        documentType.ArrivalTicket,
        documentType.ReturnTicket,
        documentType.TravelInsurance,
        documentType.AcceptanceLetter

    };

    public Dictionary<documentType, GameObject> documentPrefabs = new Dictionary<documentType, GameObject>();

    public int listIndex;

    int points;

    public void startEnd()
    {
        documentPrefabs[documentType.Passport] = gameObjectPrefabs[0];
        documentPrefabs[documentType.Visa] = gameObjectPrefabs[1];
        documentPrefabs[documentType.ArrivalTicket] = gameObjectPrefabs[2];
        documentPrefabs[documentType.ReturnTicket] = gameObjectPrefabs[3];
        documentPrefabs[documentType.TravelInsurance] = gameObjectPrefabs[4];
        documentPrefabs[documentType.AcceptanceLetter] = gameObjectPrefabs[5];

        foreach (var doc in FindObjectsByType<DocumentControllerBase>(FindObjectsSortMode.None))
        {
            if (doc.panel != null && doc.panel.activeInHierarchy)
            {
                doc.closeQuiet();
            }
        }

        points = 0;
        listIndex = 0;
        GameController.Instance.player.moveSpeed = 0;
        GameController.Instance.player.inAction = true;
        GameController.Instance.player.animatorController.SetBool("Moving", false);
        GameController.Instance.fxManager.PauseMusic();
        GameController.Instance.uiController.Finale();
        StartCoroutine(animatingSequentially());
    }

    public IEnumerator AnimateDocument(GameObject prefab, Document document)
    {
        if (document.errorType == documentError.NoDocument)
        {
            checkError(documentError.NoDocument);
            listIndex++;
            yield return new WaitForSeconds(2);
            yield break;
        }

        Vector3 visiblePosition = new Vector3(0f, 2000f, 0f);
        GameObject docGO = Instantiate(prefab, visiblePosition, Quaternion.identity);
        DocumentControllerBase controller = docGO.GetComponent<DocumentControllerBase>();
        controller.assign(document);

        if (!(document.errorType == documentError.MismatchDocument))
        {
            controller.showErrors(document);
        }

        yield return StartCoroutine(animate(document.errorType, controller.endGameAnimator, 3));
    }
    public IEnumerator animate(documentError error, Animator animator, int seconds)
    {
        animator.enabled = true;
        yield return null;
        animator.SetTrigger("SlideIn");
        yield return new WaitForSeconds(1);
        checkError(error);
        GameController.Instance.uiController.newPoints.text = points.ToString();
        yield return new WaitForSeconds(seconds);
        animator.SetTrigger("SlideOut");
        yield return new WaitForSeconds(1);
        listIndex++;
        GameObject.Destroy(animator.gameObject);

    }

    public void checkError(documentError error)
    {
        gameObjectList[listIndex].SetActive(true);
        if (error == documentError.None)
        {
            points += 15;
            endText[listIndex].text = "¡Documento correcto!";
            GameController.Instance.fxManager.maxPoints();

        }
        else if (error == documentError.ErrorInFieldOne)
        {
            points += 10;
            endText[listIndex].text = "El documento tiene 1 error";
            GameController.Instance.fxManager.lessPoints();

        }
        else if (error == documentError.ErrorInFieldTwo)
        {
            points += 5;
            endText[listIndex].text = "El documento tiene 2 errores";
            GameController.Instance.fxManager.lessPoints();

        }
        else if (error == documentError.ErrorInFieldThree)
        {
            endText[listIndex].text = "El documento tiene 3 errores";
            GameController.Instance.fxManager.noPoints();
        }
        else if (error == documentError.MismatchDocument)
        {
            endText[listIndex].text = "El documento es de otro tipo";
            GameController.Instance.fxManager.noPoints();

        }
        else
        {
            endText[listIndex].text = "El documento no fue encontrando";
            GameController.Instance.fxManager.noPoints();

        }
    }

    public IEnumerator animatingSequentially()
    {
        for (int i = 0; i < 6; i++)
        {
            Document doc = documentSubmitted[types[listIndex]];
            yield return StartCoroutine(AnimateDocument(documentPrefabs[doc.type], doc));
        }

        if (points <= 60)
        {
            GameController.Instance.fxManager.loseSound();
            yield return new WaitForSeconds(10);
        }
        else
        {
            GameController.Instance.fxManager.winSound();
            yield return new WaitForSeconds(5);
        }

        int userId = PlayerPrefs.GetInt("user_id", 1);


        StartCoroutine(EnviarMonedas(userId, points));

        int actuales = PlayerPrefs.GetInt("User_Monedas", 0);
        actuales += points;

        PlayerPrefs.SetInt("User_Monedas", actuales);

        GameWinService.Instance.EnviarGameWin(1, points);

        SceneManager.LoadScene("MainMenu");

    }

    IEnumerator EnviarMonedas(int userId, int monedas)
    {
        string url = "http://127.0.0.1:5530/users/monedas/add";
        //string url = "http://10.14.255.43:5530/users/monedas/add";

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
   