using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScriptManager : MonoBehaviour
{
    [SerializeField] private string gameplaySceneName = "GameplayScene";
    [SerializeField] private string menuSceneName = "MenuScene";

    void Start()
    {
        ManageInteractiveCardScripts();
    }

    private void ManageInteractiveCardScripts()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        bool enableInteractiveCard = currentScene == gameplaySceneName;

        InteractiveCard[] interactiveCards = FindObjectsOfType<InteractiveCard>(true);
        foreach (InteractiveCard card in interactiveCards)
        {
            card.enabled = enableInteractiveCard;
        }
    }
}
