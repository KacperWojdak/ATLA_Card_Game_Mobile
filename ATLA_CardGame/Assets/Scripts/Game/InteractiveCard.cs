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
    public bool isInDeck = false;
    public bool isEnemyCard = false;

    public int chiCost;
    public int attackPoints;
    public int goldAttackPoints;
    public int defensePoints;
    public int goldDefensePoints;
    public int healingPoints;
    public int goldHealingPoints;

    void Awake()
    {
        originalPosition = transform.localPosition;
        originalParent = transform.parent;
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isPlayerCard || isInDeck || isEnemyCard) return;

        isDragging = true;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(originalParent.parent);
        transform.localScale = Vector3.one * 1.0f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging || isInDeck) return;

        if (isDragging) transform.position = eventData.position;
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

    public void ToggleCardDisplay(bool showDetails)
    {
        Transform chiCost = transform.Find("ChiCost");
        Transform cardBack = transform.Find("CardBack");

        if (chiCost != null)
            chiCost.gameObject.SetActive(showDetails);

        if (cardBack != null)
            cardBack.gameObject.SetActive(!showDetails);
    }
}