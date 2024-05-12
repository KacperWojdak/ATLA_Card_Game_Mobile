using UnityEngine;
using TMPro;

public class HeroStats : MonoBehaviour
{
    public int healthPoints;
    public int armorPoints;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI armorText;

    public void TakeDamage(int damage)
    {
        if (armorPoints > 0)
        {
            int damageAfterArmor = damage - armorPoints;
            armorPoints = Mathf.Max(0, armorPoints - damage);
            if (damageAfterArmor > 0)
            {
                healthPoints = Mathf.Max(0, healthPoints - damageAfterArmor);
            }
        }
        else
        {
            healthPoints = Mathf.Max(0, healthPoints - damage);
        }
        UpdateUI();
    }

    public void Heal(int amount)
    {
        healthPoints = Mathf.Min(healthPoints + amount, 20);
        UpdateUI();
    }

    public void AddArmor(int amount)
    {
        armorPoints += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (healthText != null) healthText.text = healthPoints.ToString();
        if (armorText != null) armorText.text = armorPoints.ToString();
    }
}