using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject PlayerDeck { get; set; }
    public static GameObject EnemyDeck { get; set; }
    public static List<GameObject> PlayerCards { get; set; }
    public static List<GameObject> EnemyCards { get; set; }
    public static GameObject PlayerHeroCard { get; set; }
    public static GameObject EnemyHeroCard { get; set; }


    public void Start()
    {
        if (PlayerHeroCard != null)
        {
            Debug.Log("Player Hero Card: " + PlayerHeroCard.name);
            foreach (GameObject card in PlayerCards)
            {
                if (card != null)
                    Debug.Log("Player Card: " + card.name);
            }
        }

        if (EnemyHeroCard != null)
        {
            Debug.Log("Enemy Hero Card: " + EnemyHeroCard.name);
            foreach (GameObject card in EnemyCards)
            {
                if (card != null)
                    Debug.Log("Enemy Card: " + card.name);
            }
        }
    }
}
