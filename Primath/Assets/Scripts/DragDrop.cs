using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Vector2 orignPos;

    private CanvasGroup canvasGroup;

    [SerializeField] Canvas canvas;

    private void Awake()
    {
        rectTransform = this.GetComponent<RectTransform>();
        canvasGroup = this.GetComponent<CanvasGroup>();
        orignPos = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = orignPos;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
