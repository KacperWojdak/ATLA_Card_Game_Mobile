using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class ChiManagerTests
{
    private GameObject chiManagerObject;
    private ChiManager chiManager;

    [SetUp]
    public void SetUp()
    {
        chiManagerObject = new GameObject();
        chiManager = chiManagerObject.AddComponent<ChiManager>();

        chiManager.playerChiPoints = new Image[5];
        chiManager.playerGoldenChiPoints = new Image[5];
        chiManager.enemyChiPoints = new Image[5];
        chiManager.enemyGoldenChiPoints = new Image[5];

        for (int i = 0; i < 5; i++)
        {
            chiManager.playerChiPoints[i] = new GameObject().AddComponent<Image>();
            chiManager.playerGoldenChiPoints[i] = new GameObject().AddComponent<Image>();
            chiManager.enemyChiPoints[i] = new GameObject().AddComponent<Image>();
            chiManager.enemyGoldenChiPoints[i] = new GameObject().AddComponent<Image>();
        }
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(chiManagerObject);
    }

    [Test]
    public void InitializeChi_SetsChiValuesCorrectly()
    {
        chiManager.InitializeChi(3, 1, 2, 1);

        Assert.AreEqual(3, chiManager.currentPlayerChi);
        Assert.AreEqual(1, chiManager.currentPlayerGoldenChi);
        Assert.AreEqual(2, chiManager.currentEnemyChi);
        Assert.AreEqual(1, chiManager.currentEnemyGoldenChi);
    }

    [Test]
    public void UsePlayerChi_EnoughChi_ReturnsTrueAndReducesChi()
    {
        chiManager.InitializeChi(3, 0, 0, 0);

        bool result = chiManager.UsePlayerChi(2);

        Assert.IsTrue(result);
        Assert.AreEqual(1, chiManager.currentPlayerChi);
    }

    [Test]
    public void UsePlayerChi_NotEnoughChi_ReturnsFalseAndDoesNotReduceChi()
    {
        chiManager.InitializeChi(1, 0, 0, 0);

        bool result = chiManager.UsePlayerChi(2);

        Assert.IsFalse(result);
        Assert.AreEqual(1, chiManager.currentPlayerChi);
    }

    [Test]
    public void UseEnemyChi_EnoughChi_ReturnsTrueAndReducesChi()
    {
        chiManager.InitializeChi(0, 0, 3, 0);

        bool result = chiManager.UseEnemyChi(2);

        Assert.IsTrue(result);
        Assert.AreEqual(1, chiManager.currentEnemyChi);
    }

    [Test]
    public void UseEnemyChi_NotEnoughChi_ReturnsFalseAndDoesNotReduceChi()
    {
        chiManager.InitializeChi(0, 0, 1, 0);

        bool result = chiManager.UseEnemyChi(2);

        Assert.IsFalse(result);
        Assert.AreEqual(1, chiManager.currentEnemyChi);
    }

    [Test]
    public void HasEnoughChi_PlayerHasEnoughChi_ReturnsTrue()
    {
        chiManager.InitializeChi(3, 0, 0, 0);

        bool result = chiManager.HasEnoughChi(true, 2);

        Assert.IsTrue(result);
    }

    [Test]
    public void HasEnoughChi_PlayerNotEnoughChi_ReturnsFalse()
    {
        chiManager.InitializeChi(1, 0, 0, 0);

        bool result = chiManager.HasEnoughChi(true, 2);

        Assert.IsFalse(result);
    }

    [Test]
    public void HasEnoughChi_EnemyHasEnoughChi_ReturnsTrue()
    {
        chiManager.InitializeChi(0, 0, 3, 0);

        bool result = chiManager.HasEnoughChi(false, 2);

        Assert.IsTrue(result);
    }

    [Test]
    public void HasEnoughChi_EnemyNotEnoughChi_ReturnsFalse()
    {
        chiManager.InitializeChi(0, 0, 1, 0);

        bool result = chiManager.HasEnoughChi(false, 2);

        Assert.IsFalse(result);
    }
}