using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public int currentRound = 1;
    public TMP_Text roundText;
    public TMP_Text turnText;
    public Button playerSkipButton;
    public Button enemySkipButton;
    public GameManager gameManager;
    public ChiManager chiManager;
    public HandManager handManager;

    private bool playerTurn;
    private bool playerHasSkipped;
    private bool enemyHasSkipped;
    private bool startingPlayer;

    void Start()
    {
        DetermineFirstTurn();
        startingPlayer = playerTurn;
        UpdateRoundText();
        playerSkipButton.onClick.AddListener(PlayerSkip);
        enemySkipButton.onClick.AddListener(EnemySkip);
        if (gameManager == null) gameManager = FindObjectOfType<GameManager>();
        if (chiManager == null) chiManager = FindObjectOfType<ChiManager>();
        chiManager.InitializeChi(1, 0, 1, 0);
    }

    void DetermineFirstTurn()
    {
        playerTurn = Random.Range(0, 2) == 0;
        turnText.text = playerTurn ? "Your Turn" : "Enemy Turn";
    }

    public bool IsEnemyTurn()
    {
        return !playerTurn;
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
        Debug.Log($"Chi refreshed: Player and Enemy Chi set to {newChi}");
        ResetRound();
    }

    void PlayerSkip()
    {
        if (playerTurn)
        {
            playerHasSkipped = true;
            ToggleTurn();
        }
    }

    void EnemySkip()
    {
        if (!playerTurn)
        {
            enemyHasSkipped = true;
            Debug.Log("Enemy skipped their turn.");
            ToggleTurn();
        }
    }

    public void ToggleTurn()
    {
        if (playerHasSkipped && enemyHasSkipped)
        {
            SkipRound();
        }
        else
        {
            playerTurn = !playerTurn;
            turnText.text = playerTurn ? "Your Turn" : "Enemy Turn";
        }
    }

    void ResetRound()
    {
        startingPlayer = !startingPlayer;
        playerTurn = startingPlayer;
        playerHasSkipped = false;
        enemyHasSkipped = false;
        turnText.text = playerTurn ? "Your Turn" : "Enemy Turn";
    }
}