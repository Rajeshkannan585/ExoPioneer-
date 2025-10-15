using UnityEngine;
using UnityEngine.Playables;

public class TempleCutsceneDirector : MonoBehaviour
{
    [Header("Timeline")]
    public PlayableDirector introDirector;     // vertical reveal
    public PlayableDirector finaleDirector;    // altar/prayer/portal

    [Header("FX & Audio")]
    public ParticleSystem auraRings;
    public ParticleSystem soulFlakes;
    public GameObject portalCore;
    public AudioSource musicSource;
    public AudioClip templeTheme;
    public AudioClip finaleTheme;

    [Header("State")]
    public bool introPlayed;
    public bool finalePlayed;

    void Start()
    {
        if (musicSource && templeTheme)
        {
            musicSource.clip = templeTheme;
            musicSource.loop = true;
            musicSource.Play();
        }
        if (portalCore) portalCore.SetActive(false);
    }

    public void PlayIntro()
    {
        if (introPlayed || introDirector == null) return;
        introPlayed = true;
        introDirector.time = 0;
        introDirector.Play();
        if (auraRings && !auraRings.isPlaying) auraRings.Play();
    }

    public void PlayFinale()
    {
        if (finalePlayed || finaleDirector == null) return;
        finalePlayed = true;

        if (musicSource && finaleTheme)
        {
            musicSource.clip = finaleTheme;
            musicSource.loop = false;
            musicSource.Play();
        }

        if (soulFlakes && !soulFlakes.isPlaying) soulFlakes.Play();
        if (portalCore) portalCore.SetActive(true);

        finaleDirector.time = 0;
        finaleDirector.Play();
    }

    // Called from Timeline signal at climax to roll credits or load next scene
    public void OnFinaleComplete()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CreditsScene");
    }
}
