using UnityEngine;
using UnityEngine.UI;

public class ChiManager : MonoBehaviour
{
    public Image[] playerChiPoints;
    public Image[] playerGoldenChiPoints;
    public Image[] enemyChiPoints;
    public Image[] enemyGoldenChiPoints;

    private int currentPlayerChi;
    private int currentPlayerGoldenChi;
    private int currentEnemyChi;
    private int currentEnemyGoldenChi;
    private ChiManager chiManager;

    public void InitializeChi(int startPlayerChi, int startPlayerGoldenChi, int startEnemyChi, int startEnemyGoldenChi)
    {
        currentPlayerChi = startPlayerChi;
        currentPlayerGoldenChi = startPlayerGoldenChi;
        currentEnemyChi = startEnemyChi;
        currentEnemyGoldenChi = startEnemyGoldenChi;
        UpdateChiDisplay();
    }

    void UpdateChiDisplay()
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
        if (isPlayer)
        {
            return UsePlayerChi(chiCost);
        }
        else
        {
            return UseEnemyChi(chiCost);
        }
    }

    private bool UsePlayerChi(int chiCost)
    {
        if (currentPlayerChi >= chiCost)
        {
            currentPlayerChi -= chiCost;
            UpdateChiDisplay();
            return true;
        }
        return false;
    }

    private bool UseEnemyChi(int chiCost)
    {
        if (currentEnemyChi >= chiCost)
        {
            currentEnemyChi -= chiCost;
            UpdateChiDisplay();
            return true;
        }
        return false;
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
