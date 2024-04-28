using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DeckSelectionManager : MonoBehaviour
{
    public TextMeshProUGUI playerSelectedDeckText;
    public TextMeshProUGUI enemySelectedDeckText;
    public List<GameObject> playerDeckGameObjects;
    public List<GameObject> enemyDeckGameObjects;

    private GameObject playerSelectedDeck;
    private GameObject enemySelectedDeck;

    void Start()
    {
        playerSelectedDeckText.text = "Player Deck: ";
        enemySelectedDeckText.text = "Enemy Deck: ";

        playerDeckGameObjects.ForEach(deckGO => AddDeckListener(deckGO, true));
        enemyDeckGameObjects.ForEach(deckGO => AddDeckListener(deckGO, false));
    }

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

    public void PlayerDeckSelected(GameObject selectedDeckGO)
    {
        SelectDeck(ref playerSelectedDeck, selectedDeckGO, playerSelectedDeckText, playerDeckGameObjects, "Player Deck: ");
    }

    public void EnemyDeckSelected(GameObject selectedDeckGO)
    {
        SelectDeck(ref enemySelectedDeck, selectedDeckGO, enemySelectedDeckText, enemyDeckGameObjects, "Enemy Deck: ");
    }

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
