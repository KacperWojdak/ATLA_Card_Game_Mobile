using NUnit.Framework;
using UnityEngine;

public class CardDataTests
{
    [Test]
    public void AttackCardData_HoldsCorrectValues()
    {
        var attackCardData = ScriptableObject.CreateInstance<AttackCardData>();
        attackCardData.card = new AttackCard() { cardName = "Fireball", attackDamage = 10 };

        Assert.AreEqual("Fireball", attackCardData.card.cardName);
        Assert.AreEqual(10, attackCardData.card.attackDamage);
    }

    [Test]
    public void DefenseCardData_HoldsCorrectValues()
    {
        var defenseCardData = ScriptableObject.CreateInstance<DefenseCardData>();
        defenseCardData.card = new DefenseCard() { cardName = "Shield", defensePoints = 5 };

        Assert.AreEqual("Shield", defenseCardData.card.cardName);
        Assert.AreEqual(5, defenseCardData.card.defensePoints);
    }

    [Test]
    public void HeroCardData_HoldsCorrectValues()
    {
        var heroCardData = ScriptableObject.CreateInstance<HeroCardData>();
        heroCardData.card = new HeroCard() { cardName = "Archer", healthPoints = 20};

        Assert.AreEqual("Archer", heroCardData.card.cardName);
        Assert.AreEqual(20, heroCardData.card.healthPoints);
    }
}