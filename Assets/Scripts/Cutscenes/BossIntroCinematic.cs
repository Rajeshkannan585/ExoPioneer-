using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BossIntroCinematic : MonoBehaviour
{
    [Header("Camera Control")]
    public Camera mainCamera;
    public Transform bossTarget;
    public float zoomDistance = 35f;
    public float zoomSpeed = 2f;
    public float rotateSpeed = 20f;
    public float duration = 5f;

    [Header("UI Elements")]
    public CanvasGroup introPanel;
    public TextMeshProUGUI bossNameText;
    public TextMeshProUGUI subtitleText;

    [Header("Audio / Music")]
    public AudioSource musicSource;
    public AudioClip introMusic;
    public AudioClip thunderSFX;

    [Header("Environment FX")]
    public Light globalLight;
    public ParticleSystem lightningFX;
    public Animator bossAnimator;

    private bool introPlayed = false;

    void Start()
    {
        introPanel.alpha = 0;
        if (musicSource != null)
            musicSource.loop = false;
    }

    public void PlayIntro(string bossName, string subtitle)
    {
        if (introPlayed) return;
        introPlayed = true;

        bossNameText.text = bossName;
        subtitleText.text = subtitle;
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        // Step 1: Camera cinematic zoom
        Vector3 originalPos = mainCamera.transform.position;
        Quaternion originalRot = mainCamera.transform.rotation;

        float elapsed = 0f;
        if (introMusic != null)
        {
            musicSource.clip = introMusic;
            musicSource.Play();
        }

        if (thunderSFX != null)
            AudioSource.PlayClipAtPoint(thunderSFX, bossTarget.position);

        if (lightningFX != null)
            lightningFX.Play();

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Vector3 direction = (bossTarget.position - mainCamera.transform.position).normalized;
            Quaternion look = Quaternion.LookRotation(direction);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, look, Time.deltaTime * rotateSpeed);

            Vector3 zoomPos = bossTarget.position - direction * zoomDistance;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, zoomPos, Time.deltaTime * zoomSpeed);

            yield return null;
        }

        // Step 2: Show boss name
        yield return StartCoroutine(FadeUI(true));

        // Wait for a moment before returning control
        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(FadeUI(false));

        // Step 3: Restore camera
        mainCamera.transform.position = originalPos;
        mainCamera.transform.rotation = originalRot;

        // Activate boss AI after intro
        if (bossAnimator != null)
            bossAnimator.SetTrigger("Activate");

        Debug.Log("🎬 Boss intro completed!");
    }

    IEnumerator FadeUI(bool show)
    {
        float target = show ? 1f : 0f;
        float speed = 2f;
        while (Mathf.Abs(introPanel.alpha - target) > 0.05f)
        {
            introPanel.alpha = Mathf.Lerp(introPanel.alpha, target, Time.deltaTime * speed);
            yield return null;
        }
        introPanel.alpha = target;
    }
}
