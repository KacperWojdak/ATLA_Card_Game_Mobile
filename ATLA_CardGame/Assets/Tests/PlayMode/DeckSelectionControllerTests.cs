using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class DeckSelectionControllerTests
{
    private GameObject gameObject;
    private DeckSelectionController deckSelectionController;

    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject();
        deckSelectionController = gameObject.AddComponent<DeckSelectionController>();

        // Przygotowanie œrodowiska testowego
        deckSelectionController.playerSelectedDeckText = new GameObject().AddComponent<TextMeshProUGUI>();
        deckSelectionController.enemySelectedDeckText = new GameObject().AddComponent<TextMeshProUGUI>();
        deckSelectionController.playerDeckGameObjects = new List<GameObject>();
        deckSelectionController.enemyDeckGameObjects = new List<GameObject>();

        // Dodanie przyk³adowych talii
        GameObject playerDeck = new GameObject("PlayerDeck");
        playerDeck.AddComponent<Button>();
        deckSelectionController.playerDeckGameObjects.Add(playerDeck);

        GameObject enemyDeck = new GameObject("EnemyDeck");
        enemyDeck.AddComponent<Button>();
        deckSelectionController.enemyDeckGameObjects.Add(enemyDeck);

        // Przygotowanie prefabów talii
        DeckPrefab playerPrefab = playerDeck.AddComponent<DeckPrefab>();
        DeckPrefab enemyPrefab = enemyDeck.AddComponent<DeckPrefab>();
        playerPrefab.heroCard = new GameObject();
        enemyPrefab.heroCard = new GameObject();
    }

    [UnityTest]
    public IEnumerator DeckSelection_WhenPlayerDeckSelected_UpdatesUIAndGameManager()
    {
        var button = deckSelectionController.playerDeckGameObjects[0].GetComponent<Button>();
        button.onClick.Invoke();

        yield return null;

        Assert.IsNotNull(GameManager.PlayerCards);
        Assert.IsNotNull(GameManager.PlayerHeroCard);
        Assert.AreEqual("Player Deck: PlayerDeck", deckSelectionController.playerSelectedDeckText.text);
    }

    [UnityTest]
    public IEnumerator DeckSelection_WhenEnemyDeckSelected_UpdatesUIAndGameManager()
    {
        var button = deckSelectionController.enemyDeckGameObjects[0].GetComponent<Button>();
        button.onClick.Invoke();

        yield return null;

        Assert.IsNotNull(GameManager.EnemyCards);
        Assert.IsNotNull(GameManager.EnemyHeroCard);
        Assert.AreEqual("Enemy Deck: EnemyDeck", deckSelectionController.enemySelectedDeckText.text);
    }
}