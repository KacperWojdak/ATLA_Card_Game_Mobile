using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public ScriptableObject card;

    [Header("Common Card UI Elements")]
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI goldenDescriptionText;
    public TextMeshProUGUI chiCostText;
    public Image cardImage;
    public Image elementImage;

    [Header("Hero Card UI Elements")]
    public TextMeshProUGUI healthPointsText;
    public TextMeshProUGUI defensePointsText;

    void Start()
    {
        if (card != null) SetupCard();
    }

    public void SetupCard()
    {
        if (card is AttackCardData attackCardData) SetupAttackCard(attackCardData.card);
        else if (card is DefenseCardData defenseCardData) SetupDefenseCard(defenseCardData.card);
        else if (card is HeroCardData heroCardData) SetupHeroCard(heroCardData.card);
    }

    private void SetupAttackCard(AttackCard card)
    {
        cardNameText.text = card.cardName;
        descriptionText.text = card.description;
        goldenDescriptionText.text = card.goldenDescription;
        chiCostText.text = card.chiCost.ToString();
        cardImage.sprite = card.cardImage;
        elementImage.sprite = card.elementImage;
    }

    private void SetupDefenseCard(DefenseCard card)
    {
        cardNameText.text = card.cardName;
        descriptionText.text = card.description;
        goldenDescriptionText.text = card.goldenDescription;
        chiCostText.text = card.chiCost.ToString();
        cardImage.sprite = card.cardImage;
        elementImage.sprite = card.elementImage;
    }

    private void SetupHeroCard(HeroCard card)
    {
        cardNameText.text = card.cardName;
        cardImage.sprite = card.cardImage;
        elementImage.sprite = card.elementImage;
        healthPointsText.text = card.healthPoints.ToString();
        defensePointsText.text = card.defensePoints.ToString();
    }
}