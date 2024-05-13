using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckDisplay : MonoBehaviour
{
    [SerializeField] public Deck deck;
    [SerializeField] public TextMeshProUGUI deckNameText;
    [SerializeField] public Image deckImageDisplay;

    public Deck Deck => deck;

    public void Start()
    {
        if (deck != null) UpdateDeckUI();
    }

    public void UpdateDeckUI()
    {
        deckNameText.text = deck.DeckName;
        deckImageDisplay.sprite = deck.DeckImage;
    }
}