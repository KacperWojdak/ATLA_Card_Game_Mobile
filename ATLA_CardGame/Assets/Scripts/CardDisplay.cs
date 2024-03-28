using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    //Base card
    public Text cardNameText;
    public Text descriptionText;
    public Text goldenDescriptionText;
    public Text chiCostText;
    public Image cardImage;
    public Image elementImage;

    //Hero card
    public Text healthText;
    public Text talentCooldownText;
    public Text talentDescriptionText;

    //Attack card
    public Text attackPointsText;

    //Defense card
    public Text defensePointsText;
    public Text healPointsText;

    void Start()
    {
        cardNameText.text = card.cardName;
        descriptionText.text = card.description;
        goldenDescriptionText.text = card.goldDescription;
        chiCostText.text = card.chiCost.ToString();
        cardImage.sprite = card.cardImage;
        elementImage.sprite = card.elementImage;

        if (card is HeroCard heroCard)
        {
            healthText.text = heroCard.healthPoints.ToString();
            talentCooldownText.text = heroCard.talentCooldown.ToString();
            talentDescriptionText.text = heroCard.talentCost.ToString();
            
            healthText.gameObject.SetActive(true);
            talentCooldownText.gameObject.SetActive(true);
            talentDescriptionText.gameObject.SetActive(true);
        }
        else if (card is AttackCard attackCard)
        {
            attackPointsText.text = attackCard.attackPoints.ToString();
            
            attackPointsText.gameObject.SetActive(true);
        }
        else if (card is DefenseCard defenseCard)
        {
            defensePointsText.text = defenseCard.defensePoints.ToString();
            healPointsText.text = defenseCard.healPoints.ToString();
            
            defensePointsText.gameObject.SetActive(true);
            healPointsText.gameObject.SetActive(true);
        }
    }
}

