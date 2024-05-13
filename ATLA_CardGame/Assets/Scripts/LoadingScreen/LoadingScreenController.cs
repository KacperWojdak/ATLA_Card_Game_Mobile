using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{
    public CanvasGroup loadingScreenCanvasGroup;
    public float fadeDuration = 1f;
    public static bool IsNewSceneLoaded { get; private set; } = false;

    private void Start()
    {
        loadingScreenCanvasGroup.blocksRaycasts = false;
    }

    public void StartLoadingSequence()
    {
        StartCoroutine(SceneLoading());
    }

    private IEnumerator SceneLoading()
    {
        yield return StartCoroutine(FadeLoadingScreen(1f, true));

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneIndex);
        asyncLoad.allowSceneActivation = false;

        yield return new WaitForSeconds(fadeDuration);

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                StartCoroutine(FadeLoadingScreen(0f, false));
                IsNewSceneLoaded = true;
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public IEnumerator FadeLoadingScreen(float targetAlpha, bool blockRaycasts)
    {
        if (blockRaycasts) loadingScreenCanvasGroup.blocksRaycasts = true;


        float startAlpha = loadingScreenCanvasGroup.alpha;
        for (float t = 0; t < 1; t += Time.deltaTime / fadeDuration)
        {
            loadingScreenCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }
        loadingScreenCanvasGroup.alpha = targetAlpha;

        if (!blockRaycasts) loadingScreenCanvasGroup.blocksRaycasts = false;
    }
}