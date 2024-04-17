using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewDeck", menuName = "Deck")]
public class Deck : ScriptableObject
{
    public string deckName;
    public GameObject heroCardPrefab;
    public List<GameObject> cardPrefabs;
}