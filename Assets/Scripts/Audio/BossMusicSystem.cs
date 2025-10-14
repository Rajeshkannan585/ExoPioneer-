using UnityEngine;
using System.Collections;

public class BossMusicSystem : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;         // Main music player
    public AudioSource sfxSource;           // Optional for transitions

    [Header("Music Clips")]
    public AudioClip introClip;
    public AudioClip phase1Clip;
    public AudioClip phase2Clip;
    public AudioClip phase3Clip;
    public AudioClip victoryClip;

    [Header("Boss Reference")]
    public SkyBossController boss;

    private int currentPhase = 0;
    private bool ended = false;

    void Start()
    {
        if (boss == null) boss = FindObjectOfType<SkyBossController>();
        StartCoroutine(MusicLoop());
    }

    IEnumerator MusicLoop()
    {
        // Wait for boss intro to finish
        yield return new WaitForSeconds(5f);

        // Start intro clip
        if (introClip)
        {
            PlayClip(introClip, false);
            yield return new WaitForSeconds(introClip.length);
        }

        // Begin looped phase 1 track
        if (phase1Clip)
            PlayClip(phase1Clip, true);
    }

    void Update()
    {
        if (boss == null || ended) return;

        float hpPercent = boss.GetHealthPercent();

        // Phase transitions
        if (hpPercent <= boss.phase3Threshold && currentPhase != 3)
        {
            currentPhase = 3;
            SwitchTrack(phase3Clip);
        }
        else if (hpPercent <= boss.phase2Threshold && currentPhase != 2)
        {
            currentPhase = 2;
            SwitchTrack(phase2Clip);
        }

        // Victory detection
        if (hpPercent <= 0f && !ended)
        {
            ended = true;
            StartCoroutine(PlayVictory());
        }
    }

    void SwitchTrack(AudioClip clip)
    {
        if (clip == null) return;
        StartCoroutine(FadeToNewTrack(clip, 1f));
    }

    IEnumerator FadeToNewTrack(AudioClip newClip, float fadeTime)
    {
        float startVol = musicSource.volume;

        // Fade out
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVol, 0, t / fadeTime);
            yield return null;
        }

        musicSource.clip = newClip;
        musicSource.Play();

        // Fade in
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(0, startVol, t / fadeTime);
            yield return null;
        }
    }

    IEnumerator PlayVictory()
    {
        if (victoryClip == null) yield break;

        yield return new WaitForSeconds(1.5f); // short pause

        StartCoroutine(FadeToNewTrack(victoryClip, 1.5f));
        musicSource.loop = false;
        Debug.Log("🏆 Boss defeated — victory music playing!");
    }

    void PlayClip(AudioClip clip, bool loop)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.volume = 1f;
        musicSource.Play();
    }
}
