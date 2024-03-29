using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject cardMenu;
    [SerializeField] private GameObject deckMenu;

    public void DisplayMenu()
    {
        mainMenu.SetActive(true);
        cardMenu.SetActive(false);
        deckMenu.SetActive(false);
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
