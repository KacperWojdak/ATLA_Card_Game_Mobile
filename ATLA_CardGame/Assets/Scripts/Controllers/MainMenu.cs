using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject playMenu;
    [SerializeField] private GameObject cardMenu;
    [SerializeField] private GameObject deckMenu;
    [SerializeField] private GameObject rulesMenu;

    public void DisplayMenu()
    {
        mainMenu.SetActive(true);
        playMenu.SetActive(false);
        cardMenu.SetActive(false);
        deckMenu.SetActive(false);
        rulesMenu.SetActive(false);
    }

    public void DisplayPlay()
    {
        mainMenu.SetActive(false);
        playMenu.SetActive(true);
    }

    public void DisplayCards()
    {
        mainMenu.SetActive(false);
        cardMenu.SetActive(true);
    }

    public void DisplayDecks()
    {
        mainMenu.SetActive(false);
        deckMenu.SetActive(true);
    }

    public void DisplayRules()
    {
        mainMenu.SetActive(false);
        rulesMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}