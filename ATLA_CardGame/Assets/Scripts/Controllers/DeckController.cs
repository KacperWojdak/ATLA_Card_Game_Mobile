using UnityEngine;

public class DeckController : MonoBehaviour
{
    [SerializeField] private GameObject deckDisplay;
    [SerializeField] private GameObject aangDeck;
    [SerializeField] private GameObject kataraDeck;
    [SerializeField] private GameObject tophDeck;
    [SerializeField] private GameObject sukiDeck;
    [SerializeField] private GameObject irohDeck;
    [SerializeField] private GameObject zukoDeck;

    [SerializeField] private GameObject menuBackButton;

    public void ShowDeckDisplay()
    {
        deckDisplay.SetActive(true);
        aangDeck.SetActive(false);
        kataraDeck.SetActive(false);
        tophDeck.SetActive(false);
        sukiDeck.SetActive(false);
        irohDeck.SetActive(false);
        zukoDeck.SetActive(false);

        menuBackButton.SetActive(true);
    }

    public void ShowAangDeck()
    {
        deckDisplay.SetActive(false);
        aangDeck.SetActive(true);
        kataraDeck.SetActive(false);
        tophDeck.SetActive(false);
        sukiDeck.SetActive(false);
        irohDeck.SetActive(false);
        zukoDeck.SetActive(false);

        menuBackButton.SetActive(false);
    }

    public void ShowKataraDeck()
    {
        deckDisplay.SetActive(false);
        aangDeck.SetActive(false);
        kataraDeck.SetActive(true);
        tophDeck.SetActive(false);
        sukiDeck.SetActive(false);
        irohDeck.SetActive(false);
        zukoDeck.SetActive(false);

        menuBackButton.SetActive(false);
    }

    public void ShowTophDeck()
    {
        deckDisplay.SetActive(false);
        aangDeck.SetActive(false);
        kataraDeck.SetActive(false);
        tophDeck.SetActive(true);
        sukiDeck.SetActive(false);
        irohDeck.SetActive(false);
        zukoDeck.SetActive(false);

        menuBackButton.SetActive(false);
    }

    public void ShowSukiDeck()
    {
        deckDisplay.SetActive(false);
        aangDeck.SetActive(false);
        kataraDeck.SetActive(false);
        tophDeck.SetActive(false);
        sukiDeck.SetActive(true);
        irohDeck.SetActive(false);
        zukoDeck.SetActive(false);

        menuBackButton.SetActive(false);
    }

    public void ShowIrohDeck()
    {
        deckDisplay.SetActive(false);
        aangDeck.SetActive(false);
        kataraDeck.SetActive(false);
        tophDeck.SetActive(false);
        sukiDeck.SetActive(false);
        irohDeck.SetActive(true);
        zukoDeck.SetActive(false);

        menuBackButton.SetActive(false);
    }

    public void ShowZukoDeck()
    {
        deckDisplay.SetActive(false);
        aangDeck.SetActive(false);
        kataraDeck.SetActive(false);
        tophDeck.SetActive(false);
        sukiDeck.SetActive(false);
        irohDeck.SetActive(false);
        zukoDeck.SetActive(true);

        menuBackButton.SetActive(false);
    }
}