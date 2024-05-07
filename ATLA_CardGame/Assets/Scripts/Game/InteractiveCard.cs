using UnityEngine;
using UnityEngine.EventSystems;

public class InteractiveCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Vector3 originalPosition;
    public Transform originalParent;
    private CanvasGroup canvasGroup;
    private bool isDragging = false;
    public bool isPlayerCard = true;
    public bool isDropped = false;

    public ScriptableObject card;
    public AttackCard attackCard;
    public int chiCost;

    void Awake()
    {
        originalPosition = transform.localPosition;
        originalParent = transform.parent;
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        AttackCardData attackCardData = card as AttackCardData;
        if (attackCardData != null) chiCost = attackCardData.card.chiCost;
        else
        {
            DefenseCardData defenseCardData = card as DefenseCardData;

            if (defenseCardData != null) chiCost = defenseCardData.card.chiCost;
            else chiCost = 0;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isPlayerCard) return;

        isDragging = true;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(originalParent.parent);
        transform.localScale = Vector3.one * 1.0f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            transform.position = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging) return;

        isDragging = false;
        canvasGroup.blocksRaycasts = true;
        if (!isDropped)
        {
            transform.SetParent(originalParent);
            transform.localPosition = originalPosition;
            transform.localScale = Vector3.one * 0.8f;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        InteractiveCard card = eventData.pointerDrag.GetComponent<InteractiveCard>();
        if (card != null && this != card)
        {
            card.originalParent = transform;
            card.originalPosition = transform.position;
        }
    }
}