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

    private bool animationsCompleted = false;

    private void Start()
    {
        StartCoroutine(Animations());
    }

    private void Update()
    {
        if (animationsCompleted && (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    private IEnumerator Animations()
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
        animationsCompleted = true;
    }

    private Color SetAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }
}
