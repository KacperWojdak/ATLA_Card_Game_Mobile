using System.Collections;
using System.Collections.Generic;
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

    [Header("Attack Card UI Elements")]
    public GameObject attackCardUI;
    
    public TextMeshProUGUI attackDamageText;
    public TextMeshProUGUI goldenAttackDamageText;

    [Header("Defense Card UI Elements")]
    public GameObject defenseCardUI;

    public TextMeshProUGUI defensePointsText;
    public TextMeshProUGUI goldenDefensePointsText;

    public TextMeshProUGUI healPointsText;
    public TextMeshProUGUI goldenHealPointsText;

    [Header("Hero Card UI Elements")]
    public GameObject heroCardUI;

    public TextMeshProUGUI healthPointsText;
    public TextMeshProUGUI talentCooldownText;
    public TextMeshProUGUI talentDescriptionText;

    void Start()
    {
        if (card != null)
        {
            SetupCard();
        }
    }

    public void SetupCard()
    {
        if (card is AttackCardData attackCardData)
        {
            SetupAttackCard(attackCardData.card);
        }
        else if (card is DefenseCardData defenseCardData)
        {
            SetupDefenseCard(defenseCardData.card);
        }
        else if (card is HeroCardData heroCardData)
        {
            SetupHeroCard(heroCardData.card);
        }
    }

    private void SetupAttackCard(AttackCard card)
    {
        cardNameText.text = card.cardName;
        descriptionText.text = card.description;
        goldenDescriptionText.text = card.goldenDescription;
        chiCostText.text = card.chiCost.ToString();
        cardImage.sprite = card.cardImage;
        elementImage.sprite = card.elementImage;
        attackDamageText.text = card.attackDamage.ToString();
        goldenAttackDamageText.text = card.goldenAttackDamage.ToString();

        attackCardUI.SetActive(true);
        defenseCardUI.SetActive(false);
        heroCardUI.SetActive(false);
    }

    private void SetupDefenseCard(DefenseCard card)
    {
        cardNameText.text = card.cardName;
        descriptionText.text = card.description;
        goldenDescriptionText.text = card.goldenDescription;
        chiCostText.text = card.chiCost.ToString();
        cardImage.sprite = card.cardImage;
        elementImage.sprite = card.elementImage;
        defensePointsText.text = card.defensePoints.ToString();
        goldenDefensePointsText.text = card.goldenDefensePoints.ToString();
        healPointsText.text = card.healPoints.ToString();
        goldenHealPointsText.text = card.goldenHealPoints.ToString();

        attackCardUI.SetActive(false);
        defenseCardUI.SetActive(true);
        heroCardUI.SetActive(false);
    }

    private void SetupHeroCard(HeroCard card)
    {
        cardNameText.text = card.cardName;
        cardImage.sprite = card.cardImage;
        elementImage.sprite = card.elementImage;
        healthPointsText.text = card.healthPoints.ToString();
        talentCooldownText.text = card.talentCooldown.ToString();
        talentDescriptionText.text = card.talentDescription;

        
        attackCardUI.SetActive(false);
        defenseCardUI.SetActive(false);
        heroCardUI.SetActive(true);
    }
}
