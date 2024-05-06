using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject PlayerDeck { get; set; }
    public static GameObject EnemyDeck { get; set; }
    public static List<GameObject> PlayerCards { get; set; }
    public static List<GameObject> EnemyCards { get; set; }
    public static GameObject PlayerHeroCard { get; set; }
    public static GameObject EnemyHeroCard { get; set; }

    public HandManager handManager;

    void Start()
    {
        ShuffleCards(PlayerCards);
        ShuffleCards(EnemyCards);

        handManager.DealCards(PlayerCards, EnemyCards, PlayerHeroCard, EnemyHeroCard, 5);
    }

    void ShuffleCards(List<GameObject> cards)
    {
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (cards[randomIndex], cards[i]) = (cards[i], cards[randomIndex]);
        }
    }

    public void AddCardAtRoundEnd()
    {
        handManager.DealSingleCard(PlayerCards, handManager.playerHandArea, handManager.playerDeck, false);
        handManager.DealSingleCard(EnemyCards, handManager.enemyHandArea, handManager.enemyDeck, true);
    }
}
