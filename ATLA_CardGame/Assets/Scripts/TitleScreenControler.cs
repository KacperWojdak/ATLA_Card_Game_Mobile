using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField] public Image avatarLogo;
    [SerializeField] public TextMeshProUGUI cardGameText;
    [SerializeField] public TextMeshProUGUI clickToBeginText;
    [SerializeField] public Image backgroundImage;

    private bool isClickable = true;

    private void Start()
    {
        backgroundImage.color = Color.black;
        StartCoroutine(StartAnimations());
    }

    private void Update()
    {
        if (isClickable && (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
        {
            isClickable = false;
            MainScreenAnimation();
        }
    }

    private IEnumerator StartAnimations()
    {
        LeanTween.alpha(avatarLogo.GetComponent<RectTransform>(), 1f, 1f);
        LeanTween.scale(avatarLogo.GetComponent<RectTransform>(), new Vector3(1f, 1f, 1f), 1.5f)
            .setEase(LeanTweenType.easeOutBack);

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
            })
            .setLoopPingPong();

        yield return new WaitForSeconds(2.5f);
        isClickable = true;
    }

    private void MainScreenAnimation()
    {
        LeanTween.cancel(clickToBeginText.gameObject);
        LeanTween.alphaText(clickToBeginText.rectTransform, 0f, 0.2f).setOnComplete(() =>
        { 
            clickToBeginText.gameObject.SetActive(false);
        });
          
        SetTopCenterAnchors(avatarLogo.rectTransform, new Vector2(0f, 180f));
        SetTopCenterAnchors(cardGameText.rectTransform, new Vector2(-63f, 88f));

        LeanTween.moveLocal(avatarLogo.gameObject, new Vector3(0f, 180f, 0f), 1f);
        LeanTween.scale(avatarLogo.GetComponent<RectTransform>(), new Vector3(0.6f, 0.6f, 0.6f), 1f);

        LeanTween.moveLocal(cardGameText.gameObject, new Vector3(-63f, 88f, 0f), 1f);
        LeanTween.scale(cardGameText.GetComponent<RectTransform>(), new Vector3(0.6f, 0.6f, 0.6f), 1f);

        LeanTween.color(avatarLogo.rectTransform, Color.black, 1f);
        LeanTween.value(gameObject, cardGameText.color, Color.black, 1f)
            .setOnUpdate((Color color) =>
            {
                cardGameText.color = color;
            });

        LeanTween.color(backgroundImage.rectTransform, Color.white, 1f).setEase(LeanTweenType.easeInCirc);

        LeanTween.delayedCall(1f, () =>
        {
        });
    }

    private Color SetAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

    private void SetTopCenterAnchors(RectTransform rectTransform, Vector2 endPosition)
    {
        rectTransform.anchorMin = new Vector2(0.5f, 1f);
        rectTransform.anchorMax = new Vector2(0.5f, 1f);
    }
}