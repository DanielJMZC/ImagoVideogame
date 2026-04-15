using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public int id;
    // Los puntos que se le sumarán al jugador.
    public int points = 5;
    // Referencia a 'UIController'. (Ignoremos que escribí mal Controler)
    public UIController ui;
    public void OnDrop(PointerEventData eventData)
        {
            
            Debug.Log("Item dropped");

            if (eventData.pointerDrag != null)
            {
                // Cuando los IDs del objeto y el slot coinciden, se le sumará puntos al jugador.
                if (eventData.pointerDrag.GetComponent<DragDrop>().id == id)
                {
                    Debug.Log("Bien hecho! +5 puntos.");
                    ui.AddPoints(points);
                    // Referencia al objeto exacto arrastrado.
                    DragDrop dragged =  eventData.pointerDrag.GetComponent<DragDrop>();
                    dragged.OnPlacedCorrectly();
                }
                else
                {
                    Debug.Log("Nimode.");
                    ui.reset(0);
                    DragDrop dragged =  eventData.pointerDrag.GetComponent<DragDrop>();
                    dragged.NotPlacedCorrectly(eventData);
                    CameraShake.instance.Shake(0.2f, 5f);
                    
                }
                RectTransform draggedRect = eventData.pointerDrag.GetComponent<RectTransform>();

                // lo metes al slot
                draggedRect.SetParent(transform);

                // cuántas notas ya hay
                int index = transform.childCount - 1;

                // cuánto  que suba cada nota
                float offsetY = 30f;

                float randomX = Random.Range(-5f, 5f);

                draggedRect.anchoredPosition = new Vector2(randomX, offsetY * index);

                // posición final
            }
        }
    }

// Link a los video tutoriales que seguí: