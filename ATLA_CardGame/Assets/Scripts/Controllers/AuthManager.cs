using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject registerPanel;

    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;

    [SerializeField] private TMP_InputField registerEmailInput;
    [SerializeField] private TMP_InputField registerusernameInput;
    [SerializeField] private TMP_InputField registerPasswordInput;

    [SerializeField] private LoadingScreenController loadingScreenControler;

    private void Start()
    {
        if (!loadingScreenControler)
        {
            loadingScreenControler = FindObjectOfType<LoadingScreenController>();

            if (!loadingScreenControler)
            {
                Debug.LogWarning("LoadingScreenControler not found.");
            }
        }

        ShowLoginPanel();
    }

    public void ShowLoginPanel()
    {
        registerPanel.SetActive(false);
        loginPanel.SetActive(true);

        registerEmailInput.text = "Email:";
        registerusernameInput.text = "Username:";
        registerPasswordInput.text = "Password:";
    }

    public void ShowRegisterPanel()
    {
        registerPanel.SetActive(true);
        loginPanel.SetActive(false);

        emailInput.text = "Email:";
        passwordInput.text = "Password:";
    }
}
