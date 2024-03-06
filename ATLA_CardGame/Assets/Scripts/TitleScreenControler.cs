using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField] private Image avatarLogo;
    [SerializeField] private Image backgroundImage;

    [SerializeField] private TextMeshProUGUI cardGameText;
    [SerializeField] private TextMeshProUGUI clickToBeginText;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject avatarMainScreenBlack;
    [SerializeField] private GameObject avatarMainScreenWhite;
    [SerializeField] private GameObject toggleBackground;

    [SerializeField] private VideoPlayer avatarVideo;

    private bool menuAnimated = false;
    private bool isClickable = true;
    private bool mainAnimationCompleted = false;
    private RectTransform avatarLogoRect;

    private void Start()
    {
        InitiateUI();
        StartCoroutine(StartAnimations());
    }

    private void Update()
    {
        if (!mainAnimationCompleted && isClickable && (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
        {
            StartMainScreenAnimation();
        }
    }

    private void InitiateUI()
    {
        backgroundImage.color = Color.black;
        menuPanel.SetActive(false);
        isClickable = false;
        avatarMainScreenBlack.SetActive(false);
        avatarLogoRect = avatarLogo.GetComponent<RectTransform>();
        toggleBackground.SetActive(false);
    }

    private IEnumerator StartAnimations()
    {
        LeanTween.alpha(avatarLogoRect, 1f, 1f);
        LeanTween.scale(avatarLogoRect, Vector3.one, 1.5f).setEase(LeanTweenType.easeOutBack);

        LeanTween.value(gameObject, 0f, 1f, 1f)
            .setDelay(1.5f)
            .setOnUpdate((float alpha) =>
            {
                cardGameText.color = SetAlpha(cardGameText.color, alpha);
            });

        LeanTween.value(gameObject, 0f, 1f, .7f)
            .setDelay(2.2f)
            .setOnUpdate((float alpha) =>
            {
                clickToBeginText.color = SetAlpha(clickToBeginText.color, alpha);
                isClickable = true;
            })
            .setLoopPingPong();

        yield return new WaitForSeconds(2.5f);
    }

    private void StartMainScreenAnimation()
    {
        SetActiveElements(false);

        toggleBackground.SetActive(true);
        avatarMainScreenWhite.SetActive(true);
        avatarVideo.gameObject.SetActive(true);
        avatarVideo.Play();

        LeanTween.color(backgroundImage.rectTransform, Color.white, 1f).setEase(LeanTweenType.easeInCirc);

        LeanTween.delayedCall(gameObject, 1f, () =>
        {
            StartMenuAnimation();
        });

        mainAnimationCompleted = true;
    }

    private void StartMenuAnimation()
    {
        if (!menuAnimated)
        {
            menuPanel.SetActive(true);
            CanvasGroup canvasGroup = menuPanel.GetComponent<CanvasGroup>();

            if (canvasGroup == null)
                {
                    canvasGroup = menuPanel.AddComponent<CanvasGroup>();
                }

            canvasGroup.alpha = 0;
            LeanTween.alphaCanvas(canvasGroup, 1f, 1f).setEase(LeanTweenType.easeInOutQuad);
        }
        menuAnimated = true;
    }

    private void SetActiveElements(bool isActive)
    {
        avatarLogo.gameObject.SetActive(isActive);
        cardGameText.gameObject.SetActive(isActive);
        clickToBeginText.gameObject.SetActive(isActive);
        avatarMainScreenBlack.SetActive(isActive);

        if (!isActive) CancelTweens(avatarLogo.gameObject, cardGameText.gameObject, clickToBeginText.gameObject);
    }

    public void ToggleBackground()
    {
        if (avatarVideo.gameObject.activeSelf)
        {
            avatarVideo.Stop();
            avatarVideo.gameObject.SetActive(false);

            avatarMainScreenWhite.SetActive(false);
            avatarMainScreenBlack.SetActive(true);
        }
        else
        {
            avatarVideo.gameObject.SetActive(true);
            avatarVideo.Play();

            avatarMainScreenWhite.SetActive(true);
            avatarMainScreenBlack.SetActive(false);
        }
        mainAnimationCompleted = true;
    }

    private Color SetAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

    private void CancelTweens(params GameObject[] objects)
    {
        foreach (var obj in objects)
        {
            LeanTween.cancel(obj);
        }
    }
}