using NUnit.Framework;
using UnityEngine;
using TMPro;

[TestFixture]
public class HeroStatsTests
{
    private GameObject gameObject;
    private HeroStats heroStats;

    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject();
        heroStats = gameObject.AddComponent<HeroStats>();

        var healthText = gameObject.AddComponent<TextMeshProUGUI>();
        var armorText = gameObject.AddComponent<TextMeshProUGUI>();

        heroStats.healthText = healthText;
        heroStats.armorText = armorText;

        heroStats.healthPoints = 10;
        heroStats.armorPoints = 5;
    }

    [Test]
    public void HeroStats_TakeDamageWithArmor_ReduceHealthAndArmor()
    {
        heroStats.TakeDamage(3);

        Assert.AreEqual(2, heroStats.armorPoints);
        Assert.AreEqual(10, heroStats.healthPoints);
    }

    [Test]
    public void HeroStats_TakeDamageWithoutArmor_ReduceHealth()
    {
        heroStats.armorPoints = 0;
        heroStats.TakeDamage(4);

        Assert.AreEqual(6, heroStats.healthPoints);
    }

    [Test]
    public void HeroStats_Heal_IncreasesHealthNotExceedMax()
    {
        heroStats.healthPoints = 15;
        heroStats.Heal(10);

        Assert.AreEqual(20, heroStats.healthPoints);
    }

    [Test]
    public void HeroStats_AddArmor_IncreasesArmor()
    {
        heroStats.armorPoints = 3;
        heroStats.AddArmor(2);

        Assert.AreEqual(5, heroStats.armorPoints);
    }
}