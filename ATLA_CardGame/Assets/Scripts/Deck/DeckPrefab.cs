using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckPrefab : MonoBehaviour
{
    [SerializeField] private List<GameObject> cards = new List<GameObject>();
    [SerializeField] private GameObject heroCard;
}
