using UnityEngine;

public class PetEmotionVoiceSystem : MonoBehaviour
{
    public AudioClip happyVoice;      // Played when pet is happy
    public AudioClip sadVoice;        // Played when pet is sad
    public AudioClip angryVoice;      // Played when pet is angry
    public AudioClip loveVoice;       // Played when pet is fully bonded
    public PetBondSystem petBond;     // Reference to bond system
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayVoiceByMood();
        }
    }

    void PlayVoiceByMood()
    {
        if (petBond.bondXP < petBond.bondXPToNext * 0.25f)
            PlaySound(sadVoice);
        else if (petBond.bondXP < petBond.bondXPToNext * 0.5f)
            PlaySound(angryVoice);
        else if (petBond.bondXP < petBond.bondXPToNext * 0.9f)
            PlaySound(happyVoice);
        else
            PlaySound(loveVoice);
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
}
