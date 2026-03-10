using UnityEngine;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using System.Linq;
using System.Collections;
using System;

public class GameEnd : MonoBehaviour
{

    public GameObject passportPrefab;
    public GameObject acceptanceLetterPrefab;
    public GameObject travelInsurancePrefab;
    public GameObject planeTicketPrefab;
    public GameObject visaPrefab;
    public Dictionary<documentType, KeyValuePair<Document, int>?> documentSubmitted;

    int point = 0;
    
    public void startEnd()
    {
        GameController.Instance.player.moveSpeed = 0;
        GameController.Instance.player.inAction = true;
        GameController.Instance.fxManager.PauseMusic();
        GameController.Instance.uiController.Finale();
        StartCoroutine(animatingSequentially());
    }

    public IEnumerator AnimateDocument<TController, TDocument>(GameObject prefab, TDocument document)
    where TController: DocumentController<TDocument> 
    where TDocument : Document
    {
        if(document == null)
        {
            Debug.Log("Points: 0 (null)");
            soundEffects(0);
            yield return new WaitForSeconds(2);
            yield break;
        }
    
        Vector3 visiblePosition = new Vector3(0f, 2000f, 0f); 
        GameObject docGO = Instantiate(prefab, visiblePosition, Quaternion.identity);
        TController controller = docGO.GetComponent<TController>();

        Debug.Log(document.type);
        Debug.Log("Error Number: " + document.errorNumber);
        Debug.Log("Points: " + document.maxPoints);

        controller.assign(document);

        yield return StartCoroutine(animate(document.maxPoints, controller.endGameAnimator, 3));   
    }

    IEnumerator animate(int points, Animator animator, int seconds)
    {
        animator.enabled = true;
        yield return null;
        animator.SetTrigger("SlideIn");
        yield return new WaitForSeconds(1);
        soundEffects(points);
        point+=points;
        GameController.Instance.uiController.newPoints.text = point.ToString();
        yield return new WaitForSeconds(seconds);
        animator.SetTrigger("SlideOut");
        yield return new WaitForSeconds(1);
        GameObject.Destroy(animator.gameObject);  

    }

    public void soundEffects(int point)
    {
        if (point == 15)
        {
            GameController.Instance.fxManager.maxPoints();
        } else if (point == 10)
        {
            GameController.Instance.fxManager.lessPoints();
        } else if (point == 5)
        {
            GameController.Instance.fxManager.lessPoints();
        } else
        {
            GameController.Instance.fxManager.noPoints();
        }
    }

    IEnumerator animatingSequentially()
    {
        KeyValuePair<Document, int>? passportPair = documentSubmitted[documentType.Passport];
        KeyValuePair<Document, int>? visaPair = documentSubmitted[documentType.Visa];
        KeyValuePair<Document, int>? arrivalPair = documentSubmitted[documentType.ArrivalTicket];
        KeyValuePair<Document, int>? returnPair = documentSubmitted[documentType.ReturnTicket];
        KeyValuePair<Document, int>? insurancePair = documentSubmitted[documentType.TravelInsurance];
        KeyValuePair<Document, int>? letterPair = documentSubmitted[documentType.AcceptanceLetter];
        
        Passport passport = passportPair?.Key as Passport;
        Visa visa = visaPair?.Key as Visa;
        ArrivalTicket arrivalTicket = arrivalPair?.Key as ArrivalTicket;
        ReturnTicket returnTicket = returnPair?.Key as ReturnTicket;
        TravelInsurance insurance = insurancePair?.Key as TravelInsurance;
        AcceptanceLetter letter = letterPair?.Key as AcceptanceLetter;

        yield return AnimateDocument<PassportController, Passport>(passportPrefab, passport);
        yield return AnimateDocument<VisaController, Visa>(visaPrefab, visa);
        yield return AnimateDocument<PlaneTicketController, PlaneTicket>(planeTicketPrefab, arrivalTicket);
        yield return AnimateDocument<PlaneTicketController, PlaneTicket>(planeTicketPrefab, returnTicket);
        yield return AnimateDocument<TravelInsuranceController, TravelInsurance>(travelInsurancePrefab, insurance);
        yield return AnimateDocument<AcceptanceLetterController, AcceptanceLetter>(acceptanceLetterPrefab, letter);

        if (point <= 60)
        {
            GameController.Instance.fxManager.loseSound();
        } else
        {
            GameController.Instance.fxManager.winSound();
        }

        //UnityEditor.EditorApplication.isPlaying = false;

    }

}
   