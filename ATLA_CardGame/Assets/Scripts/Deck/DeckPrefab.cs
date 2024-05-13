using System.Collections.Generic;
using UnityEngine;

public class DeckPrefab : MonoBehaviour
{
    [SerializeField] public List<GameObject> cards = new List<GameObject>();
    [SerializeField] public GameObject heroCard;
    [SerializeField] public Sprite cardBackSprite;
}