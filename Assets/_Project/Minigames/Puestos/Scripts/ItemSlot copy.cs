/* using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    // El item slot tendrá un ID, el cual verificaremos si coincide con el del objeto.
    public int id;
    // Referencia a 'UIController'.
    public UIController ui;

    public void OnDrop(PointerEventData eventData)
    {
        // Utilizamos un 'Debug.Log' para comprobar si el objeto fue colocado correctamente.
        Debug.Log("Item dropped successfully!");

        // Referencia al objeto exacto que se arrastró. 
        DragDrop dragged = eventData.pointerDrag.GetComponent<DragDrop>();

        if (eventData.pointerDrag != null)
        {
            // Si el ID del item slot coincide con el del objeto, se añadirán 5 puntos al jugador.
            if (eventData.pointerDrag.GetComponent<DragDrop>.id == id)
            {
                Debug.Log("Nice!");
                ui.AddPoints(5);
                dragged.OnPlacedCorrectly();  
            }
            
            // De lo contrario, el puntaje del jugador se reiniciará.
            else
            {
                Debug.Log("Oh...");
                ui.ResetScore(0);
                dragged.NotPlacedCorrectly(eventData);
                
            }
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}
*/