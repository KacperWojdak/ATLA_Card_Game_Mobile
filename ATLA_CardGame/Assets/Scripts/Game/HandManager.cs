using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public Transform playerHandArea;
    public Transform enemyHandArea;
    public Transform playerHero;
    public Transform enemyHero;

    public float scale = 0.8f;

    public void DealCards(List<GameObject> playerCards, List<GameObject> enemyCards, GameObject playerHeroCard, GameObject enemyHeroCard, int cardsCount)
    {
        for (int i = 0; i < cardsCount; i++)
        {
            if (playerCards.Count > i) InstantiateCard(playerCards[i], playerHandArea, scale, false);
            if (enemyCards.Count > i) InstantiateCard(enemyCards[i], enemyHandArea, scale, true);
        }

        if (playerHeroCard != null) InstantiateCard(playerHeroCard, playerHero, scale, false);
        if (enemyHeroCard != null) InstantiateCard(enemyHeroCard, enemyHero, scale, true);
    }

    void InstantiateCard(GameObject cardPrefab, Transform handArea, float scale, bool isEnemy)
    {
        GameObject card = Instantiate(cardPrefab, handArea);
        card.transform.localPosition = Vector3.zero;
        card.transform.localScale = new Vector3(scale, scale, scale);

        CardZoom zoom = card.GetComponent<CardZoom>();
        if (zoom != null)
        {
            zoom.enabled = false;
        }

        if (isEnemy)
        {
            Transform chiCost = card.transform.Find("ChiCost");
            if (chiCost != null)
                chiCost.gameObject.SetActive(false);

            Transform cardBack = card.transform.Find("CardBack");
            if (cardBack != null)
                cardBack.gameObject.SetActive(true);
        }
    }
}