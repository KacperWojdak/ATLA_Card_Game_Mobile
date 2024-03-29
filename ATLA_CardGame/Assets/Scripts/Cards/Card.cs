using UnityEngine;

public enum CardType { Attack, Defense, Hero};

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
    public int talentCooldown;
    public string talentDescription;
}

[CreateAssetMenu(fileName = "NewAttackCard", menuName = "Cards/AttackCard")]
public class AttackCardData : ScriptableObject
{
    public AttackCard card;
}

[CreateAssetMenu(fileName = "NewDefenseCard", menuName = "Cards/DefenseCard")]
public class DefenseCardData : ScriptableObject
{
    public DefenseCard card;
}

[CreateAssetMenu(fileName = "NewHeroCard", menuName = "Cards/HeroCard")]
public class HeroCardData : ScriptableObject
{
    public HeroCard card;
}