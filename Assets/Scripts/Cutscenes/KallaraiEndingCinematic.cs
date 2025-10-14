using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class KallaraiEndingCinematic : MonoBehaviour
{
    [Header("UI Elements")]
    public CanvasGroup fadePanel;
    public TextMeshProUGUI dialogueText;
    public Image memoryImage;

    [Header("Audio / Music")]
    public AudioSource musicSource;
    public AudioClip memoryTheme;
    public AudioClip heartbeat;

    [Header("Camera / FX")]
    public Camera mainCamera;
    public Transform bossAreaCenter;
    public GameObject lightFX;
    public GameObject soulParticleFX;

    private bool hasPlayed = false;

    public void PlayEnding()
    {
        if (hasPlayed) return;
        hasPlayed = true;
        StartCoroutine(CinematicSequence());
    }

    IEnumerator CinematicSequence()
    {
        // Step 1: Fade to black
        yield return StartCoroutine(Fade(1f, 2f));

        // Step 2: Set memory scene
        if (musicSource && memoryTheme)
        {
            musicSource.clip = memoryTheme;
            musicSource.loop = false;
            musicSource.Play();
        }

        dialogueText.text = "“Rajesh… You reached the Tower’s summit. Do you still remember me?”";
        memoryImage.enabled = true;
        yield return new WaitForSeconds(4f);

        dialogueText.text = "“Kallarai’s soul… is still within the stars…”";
        if (heartbeat != null)
            AudioSource.PlayClipAtPoint(heartbeat, bossAreaCenter.position);

        if (soulParticleFX != null)
            Instantiate(soulParticleFX, bossAreaCenter.position, Quaternion.identity);

        yield return new WaitForSeconds(4f);

        dialogueText.text = "“You promised me, remember? To return… no matter what.” ❤️";
        yield return new WaitForSeconds(4f);

        // Step 3: Fade light and show soul FX
        if (lightFX != null)
            lightFX.SetActive(true);

        yield return new WaitForSeconds(3f);
        dialogueText.text = "Memory Restored: The Promise of Kallarai 🌌";
        yield return new WaitForSeconds(3f);

        // Step 4: Fade out (end scene)
        yield return StartCoroutine(Fade(0f, 3f));
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    IEnumerator Fade(float target, float time)
    {
        float start = fadePanel.alpha;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            fadePanel.alpha = Mathf.Lerp(start, target, t / time);
            yield return null;
        }
        fadePanel.alpha = target;
    }
}
