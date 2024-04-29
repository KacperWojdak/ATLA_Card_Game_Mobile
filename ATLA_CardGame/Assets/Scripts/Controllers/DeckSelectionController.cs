using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DeckSelectionController : MonoBehaviour
{
    public TextMeshProUGUI playerSelectedDeckText;
    public TextMeshProUGUI enemySelectedDeckText;
    public List<GameObject> playerDeckGameObjects;
    public List<GameObject> enemyDeckGameObjects;

    public GameObject playerSelectedDeck;
    public GameObject enemySelectedDeck;

    // Initialize the deck selection UI
    void Start()
    {
        playerSelectedDeckText.text = "Player Deck: ";
        enemySelectedDeckText.text = "Enemy Deck: ";

        playerDeckGameObjects.ForEach(deckGO => AddDeckListener(deckGO, true));
        enemyDeckGameObjects.ForEach(deckGO => AddDeckListener(deckGO, false));
    }

    // Helper method to add click listeners to deck game objects
    private void AddDeckListener(GameObject deckGO, bool isPlayer)
    {
        deckGO.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (isPlayer)
                PlayerDeckSelected(deckGO);
            else
                EnemyDeckSelected(deckGO);
        });
    }

    // Handle player deck selection
    public void PlayerDeckSelected(GameObject selectedDeckGO)
    {
        SelectDeck(ref playerSelectedDeck, selectedDeckGO, playerSelectedDeckText, playerDeckGameObjects, "Player Deck: ");

        DeckPrefab deckPrefab = selectedDeckGO.GetComponent<DeckPrefab>();
        GameManager.PlayerCards = deckPrefab.cards;
        GameManager.PlayerHeroCard = deckPrefab.heroCard;
    }

    // Handle enemy deck selection
    public void EnemyDeckSelected(GameObject selectedDeckGO)
    {
        SelectDeck(ref enemySelectedDeck, selectedDeckGO, enemySelectedDeckText, enemyDeckGameObjects, "Enemy Deck: ");

        DeckPrefab deckPrefab = selectedDeckGO.GetComponent<DeckPrefab>();
        GameManager.EnemyCards = deckPrefab.cards;
        GameManager.EnemyHeroCard = deckPrefab.heroCard;
    }

    // Select or unselect a deck
    private void SelectDeck(ref GameObject currentSelectedDeck, GameObject newSelectedDeck, TextMeshProUGUI deckText, List<GameObject> deckGameObjects, string textPrefix)
    {
        if (currentSelectedDeck == newSelectedDeck)
        {
            UnselectDeck(deckGameObjects);
            deckText.text = textPrefix;
            currentSelectedDeck = null;
        }
        else
        {
            currentSelectedDeck = newSelectedDeck;
            deckText.text = textPrefix + newSelectedDeck.name.Replace(" Deck", "");
            UpdateDeckVisuals(deckGameObjects, currentSelectedDeck);
        }
    }

    // Update the visual appearance of decks
    private void UpdateDeckVisuals(List<GameObject> deckGOs, GameObject selectedDeckGO)
    {
        foreach (var deckGO in deckGOs)
        {
            Transform imageTransform = deckGO.transform.Find("DeckFace/Image");
            if (imageTransform != null)
            {
                var image = imageTransform.GetComponent<Image>();
                image.color = deckGO == selectedDeckGO ? Color.white : Color.gray;
            }
        }
    }

    // Reset the visual appearance of all decks
    private void UnselectDeck(List<GameObject> deckGOs)
    {
        foreach (var deckGO in deckGOs)
        {
            Transform imageTransform = deckGO.transform.Find("DeckFace/Image");
            if (imageTransform != null)
            {
                var image = imageTransform.GetComponent<Image>();
                image.color = Color.white;
            }
        }
    }
}
