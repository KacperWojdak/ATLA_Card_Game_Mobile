using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;

public class EnemyDropArea : MonoBehaviour
{
    public ChiManager chiManager;
    public RoundManager roundManager;

    public Transform playerHero;
    public Transform enemyHero;
    public Transform discard;
    public TextMeshProUGUI enemyActionText;

    void Update()
    {
        foreach (Transform child in transform)
        {
            StartCoroutine(HandleEnemyCard(child.gameObject));
        }
    }

    private IEnumerator HandleEnemyCard(GameObject cardObject)
    {
        InteractiveCard card = cardObject.GetComponent<InteractiveCard>();
        if (card != null)
        {
            Transform chiCost = card.transform.Find("ChiCost");
            if (chiCost != null)
                chiCost.gameObject.SetActive(true);

            Transform cardBack = card.transform.Find("CardBack");
            if (cardBack != null)
                cardBack.gameObject.SetActive(false);

            bool hasEnoughGoldChi = chiManager.currentEnemyGoldenChi >= 4;
            bool hasEnoughChiForGoldStats = chiManager.HasEnoughChi(true, Mathf.Max(card.goldAttackPoints, card.goldDefensePoints, card.goldHealingPoints));

            card.ToggleCardDisplay(true);
            if (hasEnoughGoldChi && hasEnoughChiForGoldStats && chiManager.UseChi(true, Mathf.Max(card.goldAttackPoints, card.goldDefensePoints, card.goldHealingPoints)))
            {
                ApplyCardEffects(card, false, false);
                chiManager.currentEnemyGoldenChi = 0;
                chiManager.UpdateChiDisplay();

                yield return new WaitForSeconds(1.5f);

                card.transform.SetParent(discard);
                card.gameObject.SetActive(false);
            }
            else if (chiManager.UseEnemyChi(card.chiCost))
            {
                ApplyCardEffects(card, false, false);
                card.transform.SetParent(discard);
                card.gameObject.SetActive(false);
                roundManager.ToggleTurn();
            }
            else
            {
                ReturnCardToHand(card);
            }

        }
    }

    private void ApplyCardEffects(InteractiveCard card, bool isPlayerArea, bool useGoldenValues)
    {
        HeroStats heroStats = enemyHero.GetComponentInChildren<HeroStats>();
        HeroStats enemyStats = playerHero.GetComponentInChildren<HeroStats>();

        if (useGoldenValues)
        {
            enemyStats.TakeDamage(card.goldAttackPoints);
            heroStats.AddArmor(card.goldDefensePoints);
            heroStats.Heal(card.goldHealingPoints);
        }
        else
        {
            enemyStats.TakeDamage(card.attackPoints);
            heroStats.AddArmor(card.defensePoints);
            heroStats.Heal(card.healingPoints);
            enemyActionText.text = $"Enemy defends for {card.defensePoints}";
        }
    }

    private void ReturnCardToHand(InteractiveCard card)
    {
        card.transform.SetParent(card.originalParent);
        card.transform.localPosition = card.originalPosition;
        card.transform.localScale = Vector3.one * 0.8f;
        Transform chiCost = card.transform.Find("ChiCost");
        if (chiCost != null)
            chiCost.gameObject.SetActive(false);

        Transform cardBack = card.transform.Find("CardBack");
        if (cardBack != null)
            cardBack.gameObject.SetActive(true);
    }
}