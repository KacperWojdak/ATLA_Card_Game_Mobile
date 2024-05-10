using UnityEngine;

public enum CardType { Attack, Defense, Hero };

[System.Serializable]
public class Card
{
    public string cardName;
    public string description;
    public string goldenDescription;
    public int chiCost;

    public Sprite cardImage;
    public Sprite elementImage;
}

[System.Serializable]
public class AttackCard : Card
{
    public int attackDamage;
    public int goldenAttackDamage;
}

[System.Serializable]
public class DefenseCard : Card
{
    public int defensePoints;
    public int goldenDefensePoints;

    public int healPoints;
    public int goldenHealPoints;
}

[System.Serializable]
public class HeroCard : Card
{
    public int healthPoints;
    public int defensePoints;
    public int talentCooldown;
    public string talentDescription;
}