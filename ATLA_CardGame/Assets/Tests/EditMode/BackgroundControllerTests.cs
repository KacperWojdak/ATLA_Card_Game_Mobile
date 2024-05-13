using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundControllerTests
{
    private GameObject backgroundControllerObject;
    private BackgroundController backgroundController;

    [SetUp]
    public void SetUp()
    {
        backgroundControllerObject = new GameObject();
        backgroundController = backgroundControllerObject.AddComponent<BackgroundController>();

        Texture2D texture1 = new(100, 100);
        Texture2D texture2 = new(100, 100);

        for (int x = 0; x < texture1.width; x++)
        {
            for (int y = 0; y < texture1.height; y++)
            {
                texture1.SetPixel(x, y, Color.red);
                texture2.SetPixel(x, y, Color.blue);
            }
        }
        texture1.Apply();
        texture2.Apply();

        Sprite sprite1 = Sprite.Create(texture1, new Rect(0, 0, texture1.width, texture1.height), new Vector2(0.5f, 0.5f));
        Sprite sprite2 = Sprite.Create(texture2, new Rect(0, 0, texture2.width, texture2.height), new Vector2(0.5f, 0.5f));

        backgroundController.backgroundImages = new Sprite[] { sprite1, sprite2 };
        backgroundController.backgroundUI = new GameObject().AddComponent<Image>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(backgroundControllerObject);
    }

    [Test]
    public void LoadRandomBackground_ChangesBackgroundImage()
    {
        Sprite initialSprite = backgroundController.backgroundUI.sprite;

        backgroundController.LoadRandomBackground();

        Assert.AreNotEqual(initialSprite, backgroundController.backgroundUI.sprite);
    }
}
