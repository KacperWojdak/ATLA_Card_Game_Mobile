using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public DeckSelectionController deckSelectionController;

    public void OnPlayButtonClicked()
    {
        GameManager.PlayerDeck = deckSelectionController.playerSelectedDeck;
        GameManager.EnemyDeck = deckSelectionController.enemySelectedDeck;

        SceneManager.LoadScene("GameplayScene");
    }
}