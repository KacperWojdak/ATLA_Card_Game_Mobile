using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;

public class APIController : MonoBehaviour
{
    public TMP_InputField emailInputLogin;
    public TMP_InputField passwordInputLogin;
    public TMP_InputField usernameInputRegister;
    public TMP_InputField emailInputRegister;
    public TMP_InputField passwordInputRegister;
    public GameObject warning;

    public LoadingScreenController loadingScreenController;

    private readonly string baseUrl = "https://apiatlaphp.cloud/api";

    [System.Obsolete]
    public void Login()
    {
        StartCoroutine(SendLoginRequest(emailInputLogin.text, passwordInputLogin.text));
    }

    [System.Obsolete]
    public void Register()
    {
        StartCoroutine(SendRegisterRequest(usernameInputRegister.text, emailInputRegister.text, passwordInputRegister.text));
    }

    [System.Obsolete]
    private IEnumerator SendLoginRequest(string username, string password)
    {
        string jsonRequestBody = JsonUtility.ToJson(new LoginData { email = username, password = password });

        using UnityWebRequest www = UnityWebRequest.Post(baseUrl + "/login", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success || www.responseCode != 200)
        {
            Debug.LogError($"Login failed: {www.error}, Status Code: {www.responseCode}");
            warning.SetActive(true);
        }
        else
        {
            Debug.Log("Login successful. Response: " + www.downloadHandler.text);
            warning.SetActive(false);
            loadingScreenController.StartLoadingSequence();
        }
    }

    [System.Obsolete]
    private IEnumerator SendRegisterRequest(string name, string email, string password)
    {
        string jsonRequestBody = JsonUtility.ToJson(new RegistrationData { name = name, email = email, password = password });

        using UnityWebRequest www = UnityWebRequest.Post(baseUrl + "/register", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success || (www.responseCode != 200 && www.responseCode != 201))
        {
            Debug.LogError($"Register failed: {www.error}, Status Code: {www.responseCode}");
        }
        else
        {
            Debug.Log("Register successful. Response: " + www.downloadHandler.text);
            loadingScreenController.StartLoadingSequence();
        }
    }

    private class LoginData
    {
        public string email;
        public string password;
    }

    private class RegistrationData
    {
        public string name;
        public string email;
        public string password;
    }
}