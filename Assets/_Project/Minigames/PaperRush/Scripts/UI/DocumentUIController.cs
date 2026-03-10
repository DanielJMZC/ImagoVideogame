using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class DocumentUIController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private RectTransform rectTransform;
    public Canvas canvas;
    public CanvasGroup canvasGroup;
    private Vector2 originalPosition;


    [Header("Document Information")]
    public DocumentControllerBase documentController;
    public Sprite prefabSprite;

    public bool isInteractable;

    public float dragScale = 0.1f; 
    private Vector3 originalScale;

    private void Awake()
    {
        isInteractable = true;
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isInteractable) {
            originalPosition = rectTransform.anchoredPosition;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.5f;
            originalScale = rectTransform.localScale;
            rectTransform.localScale = originalScale * dragScale;
            GameController.Instance.fxManager.paperSlideSound();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isInteractable) {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position,
                canvas.worldCamera,
                out Vector2 localPointerPosition
            );

            rectTransform.anchoredPosition = localPointerPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(isInteractable) {
            rectTransform.localScale = originalScale;
            canvasGroup.blocksRaycasts = true;
            rectTransform.anchoredPosition = originalPosition;
            canvasGroup.alpha = 1f;
        }

    }

   
}


