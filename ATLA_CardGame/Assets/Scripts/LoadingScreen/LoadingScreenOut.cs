using System.Collections;
using UnityEngine;

public class LoadingScreenOut : MonoBehaviour
{
    public CanvasGroup loadingScreenCanvasGroup;
    public float fadeDuration = 1f;

    private void Start()
    {
        StartCoroutine(FadeLoadingScreen(0f, false));
    }

    private IEnumerator FadeLoadingScreen(float targetAlpha, bool blockRaycasts)
    {
        float startAlpha = loadingScreenCanvasGroup.alpha;
        for (float t = 0; t < 1; t += Time.deltaTime / fadeDuration)
        {
            loadingScreenCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }
        loadingScreenCanvasGroup.alpha = targetAlpha;
        loadingScreenCanvasGroup.blocksRaycasts = blockRaycasts;

        this.gameObject.SetActive(false);
    }
}
