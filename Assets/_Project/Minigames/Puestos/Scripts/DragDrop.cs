using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public int id;

    // dónde estaba originalmente (lado derecho)
    public Transform spawnPoint;

    //  la siguiente nota (nota 3)
    public GameObject nextNote;

    // la nota anterior
    public GameObject previousNote;
    [SerializeField] private Texture2D handCursor;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.05f;

        // no funciona for some reaosn
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
        canvasGroup.alpha = 1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;

        // volver a cursor normal
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        canvasGroup.alpha = 1f;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling();
        transform.localScale = Vector3.one * 1.15f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPoint;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out worldPoint
        );

        rectTransform.position = worldPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        transform.localScale = Vector3.one;
    }

    //  esto se llama cuando se coloca correctamente
    public void OnPlacedCorrectly()
    {
        // aparece la siguiente nota en el mismo lugar
        if (nextNote != null && spawnPoint != null)
        {
            nextNote.SetActive(true);
            nextNote.GetComponent<RectTransform>().position = spawnPoint.position;
        }
        //hi

        
        canvasGroup.blocksRaycasts = false;
        this.enabled = false;
    }

    public void NotPlacedCorrectly(PointerEventData eventData)
    {
        DragDrop[] allNotes = FindObjectsOfType<DragDrop>();

        foreach (DragDrop note in allNotes)
        {
            // if not first bye
            if (note.previousNote != null)
            {
                note.gameObject.SetActive(false);

                note.enabled = false; 
                note.canvasGroup.blocksRaycasts = false; 
            }
            else
            {
                //  Nota inicial (nota 1)
                note.gameObject.SetActive(true);

                // back to spawn
                if (note.spawnPoint != null)
                {
                    note.GetComponent<RectTransform>().position = note.spawnPoint.position;
                }

            }
            //  vuelve a ser usable como al inicio
                note.enabled = true;
                note.canvasGroup.blocksRaycasts = true;

                note.transform.SetAsLastSibling();
        }
        
    }

   
}