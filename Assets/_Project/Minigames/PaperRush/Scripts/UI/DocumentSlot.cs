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

        if (documentType == storedDocument.type) {

            GameController.Instance.HandleDocumentDropped(documentType, storedDocument);
        } else
        {
            storedDocument.errorType = documentError.MismatchDocument;
            GameController.Instance.HandleDocumentDropped(documentType, storedDocument);
        }

            isLocked = true;
    }
}
