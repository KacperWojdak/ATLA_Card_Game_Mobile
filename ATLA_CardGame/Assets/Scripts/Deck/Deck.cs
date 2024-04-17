using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public string deckName;
    public List<string> cardNames = new List<string>();

    public Deck(string name)
    {
        deckName = name;
    }

    public void AddCard(string cardName)
    {
        cardNames.Add(cardName);
    }
}