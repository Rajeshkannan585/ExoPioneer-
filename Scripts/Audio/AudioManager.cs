// ==================================
// AudioManager.cs
// Handles background music, ambient sounds, and SFX
// ==================================

using UnityEngine;

namespace ExoPioneer.AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Audio Sources")]
        public AudioSource musicSource;   // Background music
        public AudioSource sfxSource;     // Sound effects
        public AudioSource ambientSource; // Ambient loops

        void Awake()
        {
            // Singleton pattern (only one AudioManager exists)
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Play background music
        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            if (musicSource == null) return;
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }

        // Play ambient sound
        public void PlayAmbient(AudioClip clip, bool loop = true)
        {
            if (ambientSource == null) return;
            ambientSource.clip = clip;
            ambientSource.loop = loop;
            ambientSource.Play();
        }

        // Play one-shot sound effect
        public void PlaySFX(AudioClip clip)
        {
            if (sfxSource == null) return;
            sfxSource.PlayOneShot(clip);
        }
    }
}
