using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Deck", menuName = "Deck/Deck")]
public class Deck : ScriptableObject
{
    [SerializeField] public string deckName;
    [SerializeField] public Sprite deckImage;
    [SerializeField] private GameObject heroCard;
    [SerializeField] private List<GameObject> cards = new List<GameObject>();

    public string DeckName => deckName;
    public Sprite DeckImage => deckImage;
    public GameObject HeroCard => heroCard;
    public List<GameObject> Cards => cards;
}