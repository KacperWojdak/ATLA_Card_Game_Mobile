using UnityEngine;
using UnityEngine.UI;

public class AIController : MonoBehaviour
{
    public Transform enemyHand;
    public Transform enemyDropArea;
    public ChiManager chiManager;
    public Button enemySkipButton;


    private void PerformAITurn()
    {
        bool hasPlayed = false;

        foreach (Transform cardTransform in enemyHand)
        {
            InteractiveCard card = cardTransform.GetComponent<InteractiveCard>();
            if (card != null && card.chiCost <= chiManager.currentEnemyChi)
            {
                PlayCard(card);
                hasPlayed = true;
                break;
            }
        }

        if (!hasPlayed)
        {
            Debug.Log("AI decides to skip the turn.");
            enemySkipButton.onClick.Invoke();
        }
    }

    private void PlayCard(InteractiveCard card)
    {
        Debug.Log("AI plays: " + card.name);
        card.transform.SetParent(enemyDropArea);
        card.transform.localPosition = Vector3.zero;  // Reset position to the center of the DropArea
        // Tutaj trzeba by dodaæ logikê odpowiedzialn¹ za aktywacjê efektów karty
        //card.OnCardPlayed();  // Przyk³adowa metoda do implementacji w InteractiveCard
    }
}
