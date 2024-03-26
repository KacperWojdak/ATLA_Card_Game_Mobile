using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public enum CardType { Attack, Defense, Hero};

public abstract class Card : ScriptableObject
{
    public CardType cardType;

    public string cardName;
    public string description;
    public string goldDescription;

    public int chiCost;

    public Sprite cardImage;
    public Sprite elementImage;
}

[CreateAssetMenu (fileName = "New Attack Card", menuName = "Cards/Attack")]
public class AttackCard : Card
{
    public int attackPoints;
}

[CreateAssetMenu(fileName = "New Defense Card", menuName = "Cards/Defense")]
public class DefenseCard : Card
{
    public int defensePoints;
    public int healPoints;
}

[CreateAssetMenu(fileName = "New Hero Card", menuName = "Cards/Hero")]
public class HeroCard : Card
{
    public int talentCooldown;
    public int talentCost;
}