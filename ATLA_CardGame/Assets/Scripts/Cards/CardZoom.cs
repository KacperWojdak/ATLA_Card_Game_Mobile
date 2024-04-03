using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class CardZoom : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Transform originalParent;

    private bool isZoomed = false;
    private Coroutine holdCoroutine;

    public RectTransform screenCardView;
    public ScrollRect scrollRect;
    public float zoomScale = 2f;
    public float zoomDelay = 1f;
    public float moveSpeed = 15f;

    private int originalUIIndex;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
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
    }

    private void ZoomCard()
    {
        originalPosition = transform.position;
        originalParent = transform.parent;
        originalUIIndex = transform.GetSiblingIndex();

        if (!isZoomed)
        {
            transform.SetParent(screenCardView);
            transform.SetAsLastSibling();
            StartCoroutine(MoveToPosition(screenCardView.position));
            transform.localScale = originalScale * zoomScale;
            isZoomed = true;
            scrollRect.enabled = false;
        }
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