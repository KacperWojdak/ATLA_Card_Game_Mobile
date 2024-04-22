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

        foreach (var deckGO in playerDeckGameObjects)
        {
            deckGO.GetComponent<Button>().onClick.AddListener(() => PlayerDeckSelected(deckGO));
        }

        foreach (var deckGO in enemyDeckGameObjects)
        {
            deckGO.GetComponent<Button>().onClick.AddListener(() => EnemyDeckSelected(deckGO));
        }
    }

    public void PlayerDeckSelected(GameObject selectedDeckGO)
    {
        if (playerSelectedDeck == selectedDeckGO)
        {
            DeselectDeck(playerDeckGameObjects, ref playerSelectedDeck);
            playerSelectedDeckText.text = "Player Deck: ";
        }
        else
        {
            playerSelectedDeck = selectedDeckGO;
            playerSelectedDeckText.text = "Player Deck: " + selectedDeckGO.name.Replace(" Deck", "");
            UpdateDeckVisuals(playerDeckGameObjects, playerSelectedDeck);
        }
    }

    public void EnemyDeckSelected(GameObject selectedDeckGO)
    {
        if (enemySelectedDeck == selectedDeckGO)
        {
            DeselectDeck(enemyDeckGameObjects, ref enemySelectedDeck);
            enemySelectedDeckText.text = "Enemy Deck: ";
        }
        else
        {
            enemySelectedDeck = selectedDeckGO;
            enemySelectedDeckText.text = "Enemy Deck: " + selectedDeckGO.name.Replace(" Deck", "");
            UpdateDeckVisuals(enemyDeckGameObjects, enemySelectedDeck);
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
                if (deckGO == selectedDeckGO)
                {
                    image.color = Color.white;
                }
                else
                {
                    image.color = Color.gray;
                }
            }
        }
    }

    private void DeselectDeck(List<GameObject> deckGOs, ref GameObject selectedDeck)
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

        selectedDeck = null;
    }
}
