using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameObject PlayerDeck { get; set; }
    public static GameObject EnemyDeck { get; set; }
    public static List<GameObject> PlayerCards { get; set; }
    public static List<GameObject> EnemyCards { get; set; }
    public static GameObject PlayerHeroCard { get; set; }
    public static GameObject EnemyHeroCard { get; set; }

    public HandManager handManager;

    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI turnText;
    public Button restartButton;

    public Transform playerHero;
    public Transform enemyHero;


    void Start()
    {
        ShuffleCards(PlayerCards);
        ShuffleCards(EnemyCards);

        handManager.DealCards(PlayerCards, EnemyCards, PlayerHeroCard, EnemyHeroCard, 5);
    }

    void Update()
    {
        CheckGameOver();
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

    void CheckGameOver()
    {
        if (enemyHero.GetComponentInChildren<HeroStats>().healthPoints <= 0 || handManager.enemyDeck.childCount == 0)
        {
            GameOver(true);
        }
        else if (playerHero.GetComponentInChildren<HeroStats>().healthPoints <= 0 || handManager.playerDeck.childCount == 0)
        {
            GameOver(false);
        }
    }

    void GameOver(bool playerWon)
    {
        if (playerWon)
        {
            winText.gameObject.SetActive(true);
            loseText.gameObject.SetActive(false);
        }
        else
        {
            loseText.gameObject.SetActive(true);
            winText.gameObject.SetActive(false);
        }

        turnText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}