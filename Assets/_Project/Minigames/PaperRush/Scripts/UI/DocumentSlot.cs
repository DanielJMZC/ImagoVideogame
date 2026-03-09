using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DocumentSlot : MonoBehaviour, IDropHandler
{
    public bool isLocked = false;
    [SerializeField] public object storedDocument;


    [Header("Document Information")]
    public string documentType;

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

        GameController.Instance.AssignedDocument(documentType, storedDocument);

        isLocked = true;
    }
}
