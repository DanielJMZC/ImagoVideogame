using System;
using JetBrains.Annotations;
using Mono.Cecil.Cil;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum documentType
{
    Passport, Visa, ArrivalTicket, ReturnTicket, TravelInsurance, AcceptanceLetter
}

public class DocumentSlot : MonoBehaviour, IDropHandler
{
    public bool isLocked = false;
    [SerializeField] public Document storedDocument;

    public documentType documentType;
   
    [Header("Document Information")]
    public Image slotImage;


    public void OnDrop(PointerEventData eventData)
    {
        if (isLocked)
        {
            return;
        }

        DocumentUIController draggedUI = eventData.pointerDrag?.GetComponent<DocumentUIController>();
        
        storedDocument = draggedUI.documentController.GetDocument();
        slotImage.sprite = draggedUI.prefabSprite;
        slotImage.enabled = true;
        draggedUI.documentController.close();
        GameObject.Destroy(draggedUI.documentController.gameObject);
      
        Debug.Log("Document: " + storedDocument);
        Debug.Log("Type Document:" + storedDocument.type);
        Debug.Log("Slot:" + documentType);

        if (documentType != storedDocument.type)
        {
            Debug.Log("Not Passed");
            GameController.Instance.HandleDocumentDropped(null);
        } else
        {
            Debug.Log("Passed");

            GameController.Instance.HandleDocumentDropped(storedDocument);
        }

        isLocked = true;
    }
}
