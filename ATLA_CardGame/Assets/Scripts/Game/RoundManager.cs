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
    private bool startingPlayer; // True for player, false for enemy

    void Start()
    {
        DetermineFirstTurn();
        startingPlayer = playerTurn; // Initialize who starts the first round
        UpdateRoundText();
        playerSkipButton.onClick.AddListener(PlayerSkip);
        enemySkipButton.onClick.AddListener(EnemySkip);
        if (gameManager == null) gameManager = FindObjectOfType<GameManager>();
        if (chiManager == null) chiManager = FindObjectOfType<ChiManager>();
        chiManager.InitializeChi(1, 0, 1, 0);
    }

    void DetermineFirstTurn()
    {
        playerTurn = Random.Range(0, 2) == 0; // 50% chance for player or enemy to start
        turnText.text = playerTurn ? "Your Turn" : "Enemy Turn";
        Debug.Log($"First turn determined: {(playerTurn ? "Player starts" : "Enemy starts")}");
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
            Debug.Log("Player skipped their turn.");
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
            Debug.Log($"Turn switched: Now it's {(playerTurn ? "Player's" : "Enemy's")} turn.");
        }
    }

    void ResetRound()
    {
        startingPlayer = !startingPlayer; // Alternate who starts the next round
        playerTurn = startingPlayer; // Set the turn based on who is starting the round
        playerHasSkipped = false;
        enemyHasSkipped = false;
        turnText.text = playerTurn ? "Your Turn" : "Enemy Turn";
        Debug.Log($"Round reset: Starting player - {(playerTurn ? "Player" : "Enemy")}");
    }
}