using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardZoom : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Vector3 originalScale;
    public Vector3 originalPosition;
    private Transform originalParent;
    private Vector2 lastTouchPosition;
    private Vector2 dragStartPosition;

    public bool isZoomed = false;
    private bool isDragging = false;

    public RectTransform screenCardView;
    public ScrollRect scrollRect;
    private Coroutine holdCoroutine;
    private Canvas canvas;

    public float zoomScale = 2f;
    public float zoomDelay = 1f;
    public float moveSpeed = 15f;

    private int originalUIIndex;
    private const float dragThreshold = 10.0f;


    void Start()
    {
        originalScale = transform.localScale;
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastTouchPosition = eventData.position;
        if (scrollRect != null) scrollRect.StopMovement();
        holdCoroutine = StartCoroutine(HoldToZoom());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (holdCoroutine != null)
        {
            StopCoroutine(holdCoroutine);
            holdCoroutine = null;
        }

        if (isZoomed) ResetZoom();
        else
        {
            if (scrollRect != null) scrollRect.enabled = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isZoomed)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position,
                canvas.worldCamera,
                out Vector2 localPoint
            );

            transform.localPosition = localPoint;
        }
        else if (scrollRect != null) scrollRect.OnDrag(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        dragStartPosition = eventData.position;

        if (!isZoomed && scrollRect != null)
        {
            scrollRect.OnBeginDrag(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        if (!isZoomed && scrollRect != null) scrollRect.OnEndDrag(eventData);

        if (isZoomed && Vector2.Distance(dragStartPosition, eventData.position) < dragThreshold) ResetZoom();
    }

    private void ZoomCard()
    {
        originalPosition = transform.localPosition;
        originalParent = transform.parent;
        originalUIIndex = transform.GetSiblingIndex();

        if (!isZoomed)
        {
            transform.SetParent(screenCardView);
            transform.SetAsLastSibling();
            transform.localScale = originalScale * zoomScale;
            isZoomed = true;
            scrollRect.enabled = false;
        }

        transform.localPosition = Vector3.zero;
    }

    private void ResetZoom()
    {
        isZoomed = false;
        scrollRect.enabled = true;
        StartCoroutine(MoveToPosition(originalPosition, () =>
        {
            transform.SetParent(originalParent);
            transform.localScale = originalScale;
            transform.SetSiblingIndex(originalUIIndex);
        }));
    }

    private IEnumerator HoldToZoom()
    {
        yield return new WaitForSeconds(zoomDelay);
        ZoomCard();
    }

    private IEnumerator MoveToPosition(Vector3 target, System.Action onComplete = null)
    {
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
        onComplete?.Invoke();
    }
}