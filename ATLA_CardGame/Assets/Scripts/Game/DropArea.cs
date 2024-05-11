using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DropArea : MonoBehaviour, IDropHandler
{
    public bool isPlayerArea;
    public ChiManager chiManager;
    public RoundManager roundManager;

    public Transform playerHero;
    public Transform enemyHero;

    public void OnDrop(PointerEventData eventData)
    {
        InteractiveCard card = eventData.pointerDrag.GetComponent<InteractiveCard>();
        if (card == null)
        {
            return;
        }

        if ((isPlayerArea && !card.isPlayerCard) || (!isPlayerArea && card.isPlayerCard))
        {
            ReturnCardToHand(card);
            return;
        }

        bool hasEnoughGoldChi = (isPlayerArea ? chiManager.currentPlayerGoldenChi : chiManager.currentEnemyGoldenChi) >= 4;
        bool hasEnoughChiForGoldStats = chiManager.HasEnoughChi(isPlayerArea, Mathf.Max(card.goldAttackPoints, card.goldDefensePoints, card.goldHealingPoints));

        Debug.Log($"hasEnoughGoldChi: {hasEnoughGoldChi}, hasEnoughChiForGoldStats: {hasEnoughChiForGoldStats}");

        if (hasEnoughGoldChi && hasEnoughChiForGoldStats && chiManager.UseChi(isPlayerArea, Mathf.Max(card.goldAttackPoints, card.goldDefensePoints, card.goldHealingPoints)))
        {
            ApplyCardEffects(card, isPlayerArea, true);
            if (isPlayerArea)
            {
                chiManager.currentPlayerGoldenChi = 0;
            }
            else
            {
                chiManager.currentEnemyGoldenChi = 0;
            }
            chiManager.UpdateChiDisplay();
        }
        else if (chiManager.UseChi(isPlayerArea, card.chiCost))
        {
            ApplyCardEffects(card, isPlayerArea, false);
            roundManager.ToggleTurn();
        }
        else
        {
            ReturnCardToHand(card);
        }
    }

    private void ApplyCardEffects(InteractiveCard card, bool isPlayerArea, bool useGoldenValues)
    {
        HeroStats heroStats = (isPlayerArea ? playerHero : enemyHero).GetComponentInChildren<HeroStats>();
        HeroStats enemyStats = (isPlayerArea ? enemyHero : playerHero).GetComponentInChildren<HeroStats>();

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

    private IEnumerator DestroyCard(GameObject card, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(card);
    }

    private void ReturnCardToHand(InteractiveCard card)
    {
        card.transform.SetParent(card.originalParent);
        card.transform.localPosition = card.originalPosition;
        card.transform.localScale = Vector3.one * 0.8f;
    }
}