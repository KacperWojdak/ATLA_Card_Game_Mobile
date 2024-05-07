using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DropArea : MonoBehaviour, IDropHandler
{
    public bool isPlayerArea;
    public ChiManager chiManager;

    public void OnDrop(PointerEventData eventData)
    {
        InteractiveCard card = eventData.pointerDrag.GetComponent<InteractiveCard>();
        Debug.Log("Dropped object: " + eventData.pointerDrag.name); // Loguj nazwê obiektu przeci¹gniêtego
        if (card == null)
        {
            Debug.LogError("Dropped object does not have an InteractiveCard component.");
            return;
        }

        if (chiManager == null)
        {
            Debug.LogError("ChiManager is not assigned.");
            return;
        }

        if (chiManager.UseChi(isPlayerArea, card.chiCost))
        {
            card.transform.SetParent(transform);
            card.transform.position = transform.position;
            card.isDropped = true;
            StartCoroutine(DestroyCard(card.gameObject, 0.5f));
        }
        else
        {
            Debug.Log("Not enough Chi to play this card");
            ReturnCardToHand(card);
        }
    }


    private IEnumerator DestroyCard(GameObject card, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(card);
    }

    private void ReturnCardToHand(InteractiveCard card)
    {
        card.transform.SetParent(card.originalParent);
        card.transform.localPosition = card.originalPosition;
        card.transform.localScale = Vector3.one;
    }
}
