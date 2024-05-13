using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AIController : MonoBehaviour
{
    public Transform enemyHand;
    public Transform enemyDropArea;
    public Button enemySkipButton;
    public TextMeshProUGUI enemyActionText;

    private ChiManager chiManager;
    private RoundManager roundManager;

    void Start()
    {
        if (chiManager == null) chiManager = FindObjectOfType<ChiManager>();
        if (roundManager == null) roundManager = FindObjectOfType<RoundManager>();
    }

    void Update()
    {
        if (roundManager.IsEnemyTurn()) PlayTurn();
    }

    private void PlayTurn()
    {
        bool hasPlayedCard = false;

        foreach (Transform cardTransform in enemyHand)
        {
            InteractiveCard card = cardTransform.GetComponent<InteractiveCard>();
            if (card != null && chiManager.HasEnoughChi(false, card.chiCost))
            {
                if (chiManager.UseChi(false, card.chiCost))
                {
                    cardTransform.SetParent(enemyDropArea);
                    hasPlayedCard = true;
                    enemyActionText.text = $"Enemy attacks for {card.attackPoints}";
                    break;
                }
            }
        }

        if (!hasPlayedCard)
        {
            enemySkipButton.onClick.Invoke();
            enemyActionText.text = "Enemy skipped";
        }
    }
}