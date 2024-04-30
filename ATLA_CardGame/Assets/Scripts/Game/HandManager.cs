using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public Transform playerHandArea;
    public Transform enemyHandArea;

    public float scale = 0.8f;

    public void DealCards(List<GameObject> playerCards, List<GameObject> enemyCards, int cardsCount)
    {
        for (int i = 0; i < cardsCount; i++)
        {
            if (playerCards.Count > i) InstantiateCard(playerCards[i], playerHandArea, scale);
            if (enemyCards.Count > i) InstantiateCard(enemyCards[i], enemyHandArea, scale);
        }
    }

    void InstantiateCard(GameObject cardPrefab, Transform handArea, float scale)
    {
        GameObject card = Instantiate(cardPrefab, handArea);
        card.transform.localPosition = Vector3.zero;
        card.transform.localScale = new Vector3(scale, scale, scale);

        CardZoom zoom = card.GetComponent<CardZoom>();
        if (zoom != null)
        {
            zoom.enabled = false;
        }
    }
}
