using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;
using System.Collections;

public class APIControllerTests
{
    private APIController apiController;
    private GameObject gameObject;

    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject();
        apiController = gameObject.AddComponent<APIController>();

        apiController.emailInputLogin = new GameObject().AddComponent<TMP_InputField>();
        apiController.passwordInputLogin = new GameObject().AddComponent<TMP_InputField>();
        apiController.warning = new GameObject();
        apiController.warning.SetActive(false);
        apiController.loadingScreenController = new GameObject().AddComponent<LoadingScreenController>();
    }

    [UnityTest]
    [System.Obsolete]
    public IEnumerator Login_WithEmptyEmail_DisplaysWarning()
    {
        apiController.emailInputLogin.text = "";
        apiController.passwordInputLogin.text = "password";

        apiController.Login();

        yield return null;

        Assert.IsTrue(apiController.warning.activeSelf);
    }

    [UnityTest]
    [System.Obsolete]
    public IEnumerator Login_WithInvalidEmail_DisplaysWarning()
    {
        apiController.emailInputLogin.text = "bademail";
        apiController.passwordInputLogin.text = "password";

        apiController.Login();

        yield return null;

        Assert.IsTrue(apiController.warning.activeSelf);
    }
}