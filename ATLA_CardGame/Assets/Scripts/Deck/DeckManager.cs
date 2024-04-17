using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;
    private List<Deck> decks = new List<Deck>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);  // Makes this object persistent
        }
    }

    void Start()
    {
        CreateExampleDeck();
    }

    void CreateExampleDeck()
    {
        Deck exampleDeck = new Deck("Example Deck");
        exampleDeck.AddCard("Hero Card");
        for (int i = 1; i <= 20; i++)
        {
            exampleDeck.AddCard("Attack Card " + i);
            exampleDeck.AddCard("Defense Card " + i);
        }

        decks.Add(exampleDeck);
        SaveDeck(exampleDeck);
        LoadAndDisplayDeck();
    }

    void SaveDeck(Deck deck)
    {
        string deckJson = JsonUtility.ToJson(deck);
        PlayerPrefs.SetString(deck.deckName, deckJson);
        PlayerPrefs.Save();
    }

    void LoadAndDisplayDeck()
    {
        string deckName = "Example Deck"; // Name of the deck to load
        string deckJson = PlayerPrefs.GetString(deckName);
        Deck loadedDeck = JsonUtility.FromJson<Deck>(deckJson);

        foreach (string cardName in loadedDeck.cardNames)
        {
            Debug.Log("Loaded card: " + cardName);
        }
    }
}
