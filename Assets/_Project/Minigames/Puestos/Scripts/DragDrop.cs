using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public int id;

    public Transform spawnPoint;
    public GameObject nextNote;
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
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling();

        transform.localScale = Vector3.one * 1.15f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        rectTransform.anchoredPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        transform.localScale = Vector3.one;
    }

    public void OnPlacedCorrectly()
    {
        if (nextNote != null && spawnPoint != null)
        {
            nextNote.SetActive(true);

            RectTransform rt = nextNote.GetComponent<RectTransform>();
            rt.SetParent(spawnPoint.parent, false);
            rt.anchoredPosition = spawnPoint.GetComponent<RectTransform>().anchoredPosition;
        }

        canvasGroup.blocksRaycasts = false;
        this.enabled = false;
    }

    public void NotPlacedCorrectly(PointerEventData eventData)
    {
        DragDrop[] allNotes = FindObjectsOfType<DragDrop>();

        foreach (DragDrop note in allNotes)
        {
            if (note.previousNote != null)
            {
                note.gameObject.SetActive(false);
                note.canvasGroup.blocksRaycasts = false;
                note.enabled = false;
            }
            else
            {
                note.gameObject.SetActive(true);

                if (note.spawnPoint != null)
                {
                    RectTransform rt = note.GetComponent<RectTransform>();

                    rt.SetParent(note.spawnPoint.parent, false); 
                    rt.anchoredPosition = note.spawnPoint.GetComponent<RectTransform>().anchoredPosition;
                }
            }

            
            note.transform.SetAsLastSibling();

            note.enabled = true;
            note.canvasGroup.blocksRaycasts = true;
        }
    }
}