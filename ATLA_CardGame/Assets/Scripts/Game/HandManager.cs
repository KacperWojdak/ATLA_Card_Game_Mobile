using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public Transform playerHandArea;
    public Transform enemyHandArea;
    public Transform playerHero;
    public Transform enemyHero;
    public Transform playerDeck;
    public Transform enemyDeck;

    public float scale = 0.8f;

    public void DealCards(List<GameObject> playerCards, List<GameObject> enemyCards, GameObject playerHeroCard, GameObject enemyHeroCard, int cardsCount)
    {
        for (int i = 0; i < cardsCount; i++)
        {
            if (playerCards.Count > i) InstantiateCard(playerCards[i], playerHandArea, scale, false, false, true);
            if (enemyCards.Count > i) InstantiateCard(enemyCards[i], enemyHandArea, scale, true, false, true);
        }

        for (int i = cardsCount; i < playerCards.Count; i++) InstantiateCard(playerCards[i], playerDeck, scale, false, true, false);
        for (int i = cardsCount; i < enemyCards.Count; i++) InstantiateCard(enemyCards[i], enemyDeck, scale, true, true, false);

        if (playerHeroCard != null) InstantiateCard(playerHeroCard, playerHero, scale, false, false, false);
        if (enemyHeroCard != null) InstantiateCard(enemyHeroCard, enemyHero, scale, true, false, false);
    }

    void InstantiateCard(GameObject cardPrefab, Transform handArea, float scale, bool isEnemy, bool isDeckCard, bool isInHand)
    {
        GameObject card = Instantiate(cardPrefab, handArea);
        card.transform.localPosition = Vector3.zero;
        card.transform.localScale = new Vector3(scale, scale, scale);

        CardZoom zoom = card.GetComponent<CardZoom>();
        if (zoom != null)
        {
            zoom.enabled = false;
        }

        if (isInHand && !isEnemy)
        {
            InteractiveCard gameplayZoom = card.AddComponent<InteractiveCard>();
            gameplayZoom.enabled = true;
            gameplayZoom.isPlayerCard = !isEnemy;
        }

        if (isEnemy || isDeckCard)
        {
            Transform chiCost = card.transform.Find("ChiCost");
            if (chiCost != null)
                chiCost.gameObject.SetActive(false);

            Transform cardBack = card.transform.Find("CardBack");
            if (cardBack != null)
                cardBack.gameObject.SetActive(true);
        }
    }

    public void DealSingleCard(List<GameObject> cards, Transform handArea, Transform deckArea, bool isEnemy)
    {
        if (cards.Count > 0)
        {
            GameObject card = cards[0];
            cards.RemoveAt(0);

            InstantiateCard(card, handArea, scale, isEnemy, false, true);
            UpdateDeckVisual(deckArea);
        }
    }

    void UpdateDeckVisual(Transform deckArea)
    {
        foreach (Transform card in deckArea)
        {
            Transform cardBack = card.Find("CardBack");
            if (cardBack != null)
                cardBack.gameObject.SetActive(true);
        }
    }
}