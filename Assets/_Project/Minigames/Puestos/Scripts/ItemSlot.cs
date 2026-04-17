using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public int id;
    public int points = 5;
    public UIController ui;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item dropped");

        if (eventData.pointerDrag == null) return;

        DragDrop dragged = eventData.pointerDrag.GetComponent<DragDrop>();
        RectTransform draggedRect = dragged.GetComponent<RectTransform>();

        if (dragged.id == id)
        {
            Debug.Log("Bien hecho! +5 puntos.");
            ui.AddPoints(points);
            dragged.OnPlacedCorrectly();
        }
        else
        {
            Debug.Log("Nimode.");
            ui.reset(0);
            dragged.NotPlacedCorrectly(eventData);
            CameraShake.instance.Shake(0.2f, 5f);
        }

        draggedRect.SetParent(transform);

        draggedRect.anchorMin = new Vector2(0.5f, 0.5f);
        draggedRect.anchorMax = new Vector2(0.5f, 0.5f);
        draggedRect.pivot = new Vector2(0.5f, 0.5f);

        draggedRect.anchoredPosition = Vector2.zero;
    }
}