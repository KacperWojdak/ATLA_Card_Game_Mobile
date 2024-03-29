using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCreator : MonoBehaviour
{
    public GameObject CreateCard(Card cardData, GameObject cardPrefab)
    {
        GameObject cardInstance = Instantiate(cardPrefab, transform);

        cardInstance.transform.Find("Card Name").GetComponent<Text>().text = cardData.cardName;
        cardInstance.transform.Find("Description").GetComponent<Text>().text = cardData.description;
        cardInstance.transform.Find("GoldenDescription").GetComponent<Text>().text = cardData.goldenDescription;
        cardInstance.transform.Find("Image").GetComponent<Image>().sprite = cardData.cardImage;
        cardInstance.transform.Find("Elemental Stone").GetComponent<Image>().sprite = cardData.elementImage;

        if (cardData is AttackCard attackCard)
        {
            cardInstance.transform.Find("AttackDamage").GetComponent<Text>().text = attackCard.attackDamage.ToString();
        }
        else if (cardData is DefenseCard defenseCard)
        {
            cardInstance.transform.Find("DamageDefended").GetComponent<Text>().text = defenseCard.defensePoints.ToString();
            cardInstance.transform.Find("DamageHealed").GetComponent<Text>().text = defenseCard.healPoints.ToString();
        }
        else if (cardData is HeroCard heroCard)
        {
            cardInstance.transform.Find("TalentDescription").GetComponent<Text>().text = heroCard.talentDescription;
            cardInstance.transform.Find("TalentCooldown").GetComponent<Text>().text = heroCard.talentCooldown.ToString();
            cardInstance.transform.Find("Health").GetComponent<Text>().text = heroCard.healthPoints.ToString();
        }

        return cardInstance;
    }
}
