using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
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

    public void SetupCard(Card card)
    {
        cardNameText.text = card.cardName;
        descriptionText.text = card.description;
        goldenDescriptionText.text = card.goldenDescription;
        chiCostText.text = card.chiCost.ToString();

        cardImage.sprite = card.cardImage;
        elementImage.sprite = card.elementImage;

        attackCardUI.SetActive(false);
        defenseCardUI.SetActive(false);
        heroCardUI.SetActive(false);

        if (card is AttackCard attackCard)
        {
            attackCardUI.SetActive(true);

            attackDamageText.text = attackCard.attackDamage.ToString();
            goldenAttackDamageText.text = attackCard.goldenAttackDamage.ToString();
        }
        else if (card is DefenseCard defenseCard)
        {
            defenseCardUI.SetActive(true);

            chiCostText.text = defenseCard.chiCost.ToString();
            defensePointsText.text = defenseCard.defensePoints.ToString();
            goldenDefensePointsText.text = defenseCard.goldenDefensePoints.ToString();
            healPointsText.text = defenseCard.healPoints.ToString();
            goldenHealPointsText.text = defenseCard.goldenHealPoints.ToString();
        }
        else if (card is HeroCard heroCard)
        {
            heroCardUI.SetActive(true);

            healthPointsText.text = heroCard.healthPoints.ToString();
            talentCooldownText.text = heroCard.talentCooldown.ToString();
            talentDescriptionText.text = heroCard.talentDescription;
        }
    }
}
