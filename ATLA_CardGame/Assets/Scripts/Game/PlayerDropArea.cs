using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerDropArea : MonoBehaviour, IDropHandler
{
    public ChiManager chiManager;
    public RoundManager roundManager;

    public Transform playerHero;
    public Transform enemyHero;
    public Transform discard;

    public void OnDrop(PointerEventData eventData)
    {
        InteractiveCard card = eventData.pointerDrag.GetComponent<InteractiveCard>();
        if (card == null || !card.isPlayerCard) return;

        bool hasEnoughGoldChi = chiManager.currentPlayerGoldenChi >= 4;
        bool hasEnoughChiForGoldStats = chiManager.HasEnoughChi(true, Mathf.Max(card.goldAttackPoints, card.goldDefensePoints, card.goldHealingPoints));

        Debug.Log($"hasEnoughGoldChi: {hasEnoughGoldChi}, hasEnoughChiForGoldStats: {hasEnoughChiForGoldStats}");

        if (hasEnoughGoldChi && hasEnoughChiForGoldStats && chiManager.UseChi(true, Mathf.Max(card.goldAttackPoints, card.goldDefensePoints, card.goldHealingPoints)))
        {
            ApplyCardEffects(card, true, true);
            card.transform.SetParent(discard);
            card.gameObject.SetActive(false);
            chiManager.currentPlayerGoldenChi = 0;
            chiManager.UpdateChiDisplay();
        }
        else if (chiManager.UsePlayerChi(card.chiCost))
        {
            ApplyCardEffects(card, true, false);
            roundManager.ToggleTurn();
            card.transform.SetParent(discard);
            card.gameObject.SetActive(false);
        }
        else
        {
            ReturnCardToHand(card);
        }
    }

    private void ApplyCardEffects(InteractiveCard card, bool isPlayerArea, bool useGoldenValues)
    {
        HeroStats heroStats = playerHero.GetComponentInChildren<HeroStats>();
        HeroStats enemyStats = enemyHero.GetComponentInChildren<HeroStats>();

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
        }
    }

    private void ReturnCardToHand(InteractiveCard card)
    {
        card.transform.SetParent(card.originalParent);
        card.transform.localPosition = card.originalPosition;
        card.transform.localScale = Vector3.one * 0.8f;
    }
}