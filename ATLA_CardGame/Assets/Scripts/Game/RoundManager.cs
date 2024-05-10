using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public int currentRound = 1;
    public TMP_Text roundText;
    public Button skipButton;
    public GameManager gameManager;
    public ChiManager chiManager;
    public HandManager handManager;

    void Start()
    {
        UpdateRoundText();
        skipButton.onClick.AddListener(SkipRound);
        if (gameManager == null) gameManager = FindObjectOfType<GameManager>();
        if (chiManager == null) chiManager = FindObjectOfType<ChiManager>();
        chiManager.InitializeChi(1, 0, 1, 0);
    }

    public void UpdateRoundText()
    {
        roundText.text = "ROUND " + currentRound;
        AnimateRoundText();
    }

    void AnimateRoundText()
    {
        roundText.gameObject.SetActive(true);
        roundText.alpha = 0;
        LeanTween.value(gameObject, 0, 1, 0.5f).setOnUpdate((float val) => {
            roundText.alpha = val;
        });
        LeanTween.scale(roundText.rectTransform, Vector3.one * 1.2f, 0.5f).setEase(LeanTweenType.easeOutBack);

        StartCoroutine(HideRoundText());
    }

    IEnumerator HideRoundText()
    {
        yield return new WaitForSeconds(2);
        LeanTween.value(gameObject, 1, 0, 0.5f).setOnUpdate((float val) => {
            roundText.alpha = val;
        }).setOnComplete(() => {
            roundText.gameObject.SetActive(false);
        });
        LeanTween.scale(roundText.rectTransform, Vector3.one, 0.5f).setEase(LeanTweenType.easeInBack);
    }

    void SkipRound()
    {
        currentRound++;
        UpdateRoundText();
        gameManager.AddCardAtRoundEnd();

        handManager.ResetPlayerCanPlay();
        handManager.ResetEnemyCanPlay();

        int newChi = Mathf.Min(currentRound, 10);
        chiManager.RefreshChi(newChi, newChi);
    }
}
