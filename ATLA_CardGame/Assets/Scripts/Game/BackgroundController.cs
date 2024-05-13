using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public static BackgroundController Instance;

    public Sprite[] backgroundImages;
    public Image backgroundUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        LoadRandomBackground();
    }

    public void LoadRandomBackground()
    {
        if (backgroundImages == null) return;

        int index = Random.Range(0, backgroundImages.Length);
        backgroundUI.sprite = backgroundImages[index];
    }
}