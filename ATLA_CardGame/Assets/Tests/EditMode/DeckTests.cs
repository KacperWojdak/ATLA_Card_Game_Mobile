using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DeckTests
{
    [Test]
    public void Deck_CorrectlyAssignsAndRetrievesValues()
    {
        var deck = ScriptableObject.CreateInstance<Deck>();

        // Przygotowanie danych testowych
        var expectedName = "Test Deck";
        var expectedImage = Sprite.Create(Texture2D.blackTexture, new Rect(0.0f, 0.0f, 1.0f, 1.0f), new Vector2(0.5f, 0.5f));
        var expectedHeroCard = new GameObject("HeroCard");
        var expectedCards = new List<GameObject> { new GameObject("Card1"), new GameObject("Card2") };

        // Przypisanie wartoœci
        deck.deckName = expectedName;
        deck.deckImage = expectedImage;
        deck.heroCard = expectedHeroCard;
        deck.cards = expectedCards;

        // Weryfikacja
        Assert.AreEqual(expectedName, deck.DeckName);
        Assert.AreEqual(expectedImage, deck.DeckImage);
        Assert.AreEqual(expectedHeroCard, deck.HeroCard);
        Assert.AreEqual(expectedCards, deck.Cards);
    }
}