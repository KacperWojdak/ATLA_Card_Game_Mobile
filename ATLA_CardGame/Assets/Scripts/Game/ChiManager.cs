using UnityEngine;
using UnityEngine.UI;

public class ChiManager : MonoBehaviour
{
    public Image[] playerChiPoints;
    public Image[] playerGoldenChiPoints;
    public Image[] enemyChiPoints;
    public Image[] enemyGoldenChiPoints;

    public int currentPlayerChi;
    public int currentPlayerGoldenChi;
    public int currentEnemyChi;
    public int currentEnemyGoldenChi;
    private ChiManager chiManager;

    public void InitializeChi(int startPlayerChi, int startPlayerGoldenChi, int startEnemyChi, int startEnemyGoldenChi)
    {
        currentPlayerChi = startPlayerChi;
        currentPlayerGoldenChi = startPlayerGoldenChi;
        currentEnemyChi = startEnemyChi;
        currentEnemyGoldenChi = startEnemyGoldenChi;
        UpdateChiDisplay();
    }

    public void UpdateChiDisplay()
    {
        UpdateChiImages(playerChiPoints, currentPlayerChi);
        UpdateChiImages(playerGoldenChiPoints, currentPlayerGoldenChi);
        UpdateChiImages(enemyChiPoints, currentEnemyChi);
        UpdateChiImages(enemyGoldenChiPoints, currentEnemyGoldenChi);
    }

    void UpdateChiImages(Image[] chiImages, int currentChi)
    {
        for (int i = 0; i < chiImages.Length; i++)
        {
            chiImages[i].color = i < currentChi ? Color.white : new Color(0.35f, 0.35f, 0.35f);
        }
    }

    public bool UseChi(bool isPlayer, int chiCost)
    {
        int currentChi = isPlayer ? currentPlayerChi : currentEnemyChi;

        if (currentChi >= chiCost)
        {
            if (isPlayer) currentPlayerChi -= chiCost;
            else currentEnemyChi -= chiCost;

            UpdateChiDisplay();
            return true;
        }
        return false;
    }

    public bool UsePlayerChi(int chiCost)
    {
        if (currentPlayerChi >= chiCost)
        {
            currentPlayerChi -= chiCost;
            UpdateChiDisplay();
            return true;
        }
        return false;
    }

    public bool UseEnemyChi(int chiCost)
    {
        if (currentEnemyChi >= chiCost)
        {
            currentEnemyChi -= chiCost;
            UpdateChiDisplay();
            return true;
        }
        return false;
    }

    public bool HasEnoughChi(bool isPlayer, int chiCost)
    {
        int currentChi = isPlayer ? currentPlayerChi : currentEnemyChi;
        return currentChi >= chiCost;
    }

    public void RefreshChi(int newPlayerChi, int newEnemyChi)
    {
        currentPlayerGoldenChi = Mathf.Min(currentPlayerGoldenChi + currentPlayerChi, 4);
        currentEnemyGoldenChi = Mathf.Min(currentEnemyGoldenChi + currentEnemyChi, 4);
        currentPlayerChi = newPlayerChi;
        currentEnemyChi = newEnemyChi;
        UpdateChiDisplay();
    }
}