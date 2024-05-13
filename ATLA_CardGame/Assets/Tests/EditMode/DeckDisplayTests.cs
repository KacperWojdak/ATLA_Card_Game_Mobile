using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckDisplayTests
{
    private GameObject gameObject;
    private DeckDisplay deckDisplay;
    private Deck deck;

    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject();
        deckDisplay = gameObject.AddComponent<DeckDisplay>();

        var textGameObject = new GameObject();
        deckDisplay.deckNameText = textGameObject.AddComponent<TextMeshProUGUI>();
        var imageGameObject = new GameObject();
        deckDisplay.deckImageDisplay = imageGameObject.AddComponent<Image>();

        deck = ScriptableObject.CreateInstance<Deck>();
        deck.deckName = "Test Deck";

        Texture2D texture = new Texture2D(100, 100);
        var rect = new Rect(0.0f, 0.0f, texture.width, texture.height);
        var pivot = new Vector2(0.5f, 0.5f);

        var fillColor = Color.clear;
        var fillPixels = new Color[texture.width * texture.height];
        for (int i = 0; i < fillPixels.Length; i++)
        {
            fillPixels[i] = fillColor;
        }
        texture.SetPixels(fillPixels);
        texture.Apply();

        deck.deckImage = Sprite.Create(texture, rect, pivot);
        deckDisplay.deck = deck;
    }

    [Test]
    public void DeckDisplay_UpdatesUIOnStart()
    {
        deckDisplay.Start();

        Assert.AreEqual("Test Deck", deckDisplay.deckNameText.text);
        Assert.AreEqual(deck.deckImage, deckDisplay.deckImageDisplay.sprite);
    }

    [Test]
    public void UpdateDeckUI_UpdatesTextAndImage()
    {
        deckDisplay.UpdateDeckUI();

        Assert.AreEqual("Test Deck", deckDisplay.deckNameText.text);
        Assert.AreEqual(deck.deckImage, deckDisplay.deckImageDisplay.sprite);
    }
}