using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CreditsSceneController : MonoBehaviour
{
    public CanvasGroup fadePanel;
    public RectTransform creditsPanel;
    public float scrollSpeed = 50f;
    public AudioSource musicSource;
    public AudioClip creditsMusic;
    public TextMeshProUGUI endMessage;

    void Start()
    {
        if (musicSource && creditsMusic)
        {
            musicSource.clip = creditsMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
        StartCoroutine(PlayCredits());
    }

    IEnumerator PlayCredits()
    {
        fadePanel.alpha = 0;
        endMessage.alpha = 0;
        yield return StartCoroutine(FadeIn(1f));

        // Scroll credits upward
        float startY = creditsPanel.anchoredPosition.y;
        float endY = startY + 1200f;

        while (creditsPanel.anchoredPosition.y < endY)
        {
            creditsPanel.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(ShowEndMessage());
    }

    IEnumerator FadeIn(float duration)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            fadePanel.alpha = Mathf.Lerp(0, 1, t / duration);
            yield return null;
        }
        fadePanel.alpha = 1;
    }

    IEnumerator ShowEndMessage()
    {
        endMessage.text = "💫 For Kallarai\n\nDeveloped by R. Rajeshkannan ❤️";
        for (float t = 0; t < 2f; t += Time.deltaTime)
        {
            endMessage.alpha = Mathf.Lerp(0, 1, t / 2f);
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("PostCreditsFlightScene");
    }
}
