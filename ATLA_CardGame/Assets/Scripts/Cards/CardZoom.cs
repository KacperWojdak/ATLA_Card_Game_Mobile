using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CardZoom : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Transform originalParent;

    public RectTransform viewRectTransform; 
    public float zoomScale = 3f;
    public float moveSpeed = 15f; 

    private bool isZoomed = false;
    private Coroutine zoomCoroutine;
    private int originalUIIndex;

    public void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        zoomCoroutine = StartCoroutine(DelayedAction(0.5f, () =>
        {
            if (!isZoomed)
            {
                ZoomCard();
            }
        }));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
            zoomCoroutine = null;
        }

        if (isZoomed)
        {
            UnzoomCard();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
            zoomCoroutine = null;
        }

        if (isZoomed)
        {
            UnzoomCard();
        }
    }

    private void ZoomCard()
    {
        isZoomed = true;
        originalPosition = transform.position;
        originalParent = transform.parent;
        originalUIIndex = transform.GetSiblingIndex();

        transform.SetParent(viewRectTransform);
        transform.SetAsLastSibling();

        StartCoroutine(MoveToPosition(viewRectTransform.position));
        transform.localScale = originalScale * zoomScale;
    }

    private void UnzoomCard()
    {
        isZoomed = false;
        StartCoroutine(MoveToPosition(originalPosition, () =>
        {
            transform.SetParent(originalParent);
            transform.localScale = originalScale;
            transform.SetSiblingIndex(originalUIIndex);
        }));

    }

    private IEnumerator DelayedAction(float delay, System.Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
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
