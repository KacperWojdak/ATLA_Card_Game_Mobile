using System;
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

    public int maxHandSize = 10;
    public float scale = 0.8f;

    private bool playerCanPlay = true;
    private bool enemyCanPlay = true;

    public ChiManager chiManager;
    public RoundManager roundManager;

    public void ResetPlayerCanPlay() { playerCanPlay = true; }
    public void ResetEnemyCanPlay() { enemyCanPlay = true; }

    public void AttemptToPlayCard(GameObject cardObject, bool isPlayer)
    {
        if (cardObject == null) throw new ArgumentNullException(nameof(cardObject));

        InteractiveCard card = cardObject.GetComponent<InteractiveCard>();
        if (card == null) return;

        if ((isPlayer && !playerCanPlay) || (!isPlayer && !enemyCanPlay)) return;

        if ((isPlayer && !chiManager.UseChi(true, card.chiCost)) || (!isPlayer && !chiManager.UseChi(false, card.chiCost))) return;

        if (isPlayer) playerCanPlay = false;
        else enemyCanPlay = false;

        CheckEndOfRound();
    }

    private void CheckEndOfRound()
    {
        if (!playerCanPlay && !enemyCanPlay)
        {
            EndRound();
        }
    }

    private void EndRound()
    {
        roundManager.currentRound++;
        roundManager.UpdateRoundText();
        ResetRound();
    }

    private void ResetRound()
    {
        playerCanPlay = true;
        enemyCanPlay = true;
    }

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

        InteractiveCard interactiveCard = card.GetComponent<InteractiveCard>() ?? card.AddComponent<InteractiveCard>();
        interactiveCard.isEnemyCard = isEnemy;
        interactiveCard.isInDeck = isDeckCard;
        interactiveCard.isPlayerCard = !isEnemy;

        if (isEnemy || isDeckCard)
        {
            Transform chiCost = card.transform.Find("ChiCost");
            if (chiCost != null)
                chiCost.gameObject.SetActive(false);

            Transform cardBack = card.transform.Find("CardBack");
            if (cardBack != null)
                cardBack.gameObject.SetActive(true);
        }

        if (card.TryGetComponent<CardZoom>(out var zoom)) zoom.enabled = false;
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

        CardZoom cardZoomComponent = card.GetComponent<CardZoom>();
        if (cardZoomComponent != null)
        {
            cardZoomComponent.enabled = false;
        }
    }

    public void DealSingleCard(List<GameObject> cards, Transform handArea, Transform deckArea, bool isEnemy)
    {
        if (cards.Count > 0)
        {
            GameObject cardPrefab = cards[0];
            cards.RemoveAt(0);

            if (CountCardsInHand(handArea) >= maxHandSize)
            {
                Destroy(cardPrefab);
            }
            else
            {
                GameObject cardInstance = Instantiate(cardPrefab, handArea);
                cardInstance.transform.localPosition = Vector3.zero;
                cardInstance.transform.localScale = new Vector3(scale, scale, scale);

                if (cardInstance.TryGetComponent<CardZoom>(out var cardZoom))
                {
                    cardZoom.enabled = false;
                }

                Transform chiCost = cardInstance.transform.Find("ChiCost");
                if (chiCost != null)
                    chiCost.gameObject.SetActive(!isEnemy);

                Transform cardBack = cardInstance.transform.Find("CardBack");
                if (cardBack != null)
                    cardBack.gameObject.SetActive(isEnemy);
            }
        }
        else
        {
            deckArea.gameObject.SetActive(false);
        }
    }

    private int CountCardsInHand(Transform handArea)
    {
        return handArea.childCount;
    }
}