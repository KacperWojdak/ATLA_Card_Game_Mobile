using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Deck/Deck")]
public class Deck : ScriptableObject
{
    [SerializeField] public string deckName;
    [SerializeField] public Sprite deckImage;
    [SerializeField] public GameObject heroCard;
    [SerializeField] public List<GameObject> cards = new();

    public string DeckName => deckName;
    public Sprite DeckImage => deckImage;
    public GameObject HeroCard => heroCard;
    public List<GameObject> Cards => cards;
}