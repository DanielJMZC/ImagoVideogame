using UnityEngine;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using System.Linq;
using System.Collections;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public Dictionary<documentType, Document> documentSubmitted;

    public List<GameObject> gameObjectList = new List<GameObject>();
    public List<TextMeshProUGUI> endText = new List<TextMeshProUGUI>();
    public List <GameObject> gameObjectPrefabs = new List<GameObject>();
    public List<documentType> types = new List<documentType>()
    {
        documentType.Passport,
        documentType.Visa,
        documentType.ArrivalTicket,
        documentType.ReturnTicket,
        documentType.TravelInsurance,
        documentType.AcceptanceLetter

    };

    public Dictionary<documentType, GameObject> documentPrefabs = new Dictionary<documentType,GameObject>();

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

        points = 0;
        listIndex = 0;
        GameController.Instance.player.moveSpeed = 0;
        GameController.Instance.player.inAction = true;
        GameController.Instance.fxManager.PauseMusic();
        GameController.Instance.uiController.Finale();
        StartCoroutine(animatingSequentially());
    }

    public IEnumerator AnimateDocument(GameObject prefab, Document document)
    {
        if(document.errorType == documentError.NoDocument)
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
        
        if (!(document.errorType == documentError.MismatchDocument)) {
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
            points+=15;
            endText[listIndex].text = "¡Documento correcto!";
            GameController.Instance.fxManager.maxPoints();

        } else if (error == documentError.ErrorInFieldOne)
        {
            points+=10;
            endText[listIndex].text = "El documento tiene 1 error";
            GameController.Instance.fxManager.lessPoints();

        } else if (error == documentError.ErrorInFieldTwo)
        {
            points+=5;
            endText[listIndex].text = "El documento tiene 2 errores";
            GameController.Instance.fxManager.lessPoints();

        } else if (error == documentError.ErrorInFieldThree)
        {
            endText[listIndex].text = "El documento tiene 3 errores";
            GameController.Instance.fxManager.noPoints();
        } else if (error == documentError.MismatchDocument)
        { 
            endText[listIndex].text = "El documento es de otro tipo";
            GameController.Instance.fxManager.noPoints();

        } else
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
            Debug.Log(doc);
            yield return StartCoroutine(AnimateDocument(documentPrefabs[doc.type], doc));
        }

        if (points <= 60)
        {
            GameController.Instance.fxManager.loseSound();
        } else
        {
            GameController.Instance.fxManager.winSound();
        }

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("MainMenu");

    }

  

}
   