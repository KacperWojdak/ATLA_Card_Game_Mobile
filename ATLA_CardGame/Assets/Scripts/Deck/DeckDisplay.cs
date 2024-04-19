using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckDisplay : MonoBehaviour
{
    [SerializeField] private Deck deck;
    [SerializeField] private TextMeshProUGUI deckNameText;
    [SerializeField] private Image deckImageDisplay;

    void Start()
    {
        if (deck != null)
        {
            UpdateDeckUI();
        }
    }

    public void UpdateDeckUI()
    {
        deckNameText.text = deck.DeckName;
        deckImageDisplay.sprite = deck.DeckImage;
    }
}
