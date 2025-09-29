// ===============================
// ShrineTrigger.cs
// Attach this script to an Alien Shrine prefab in Camante Camp.
// When the player enters, a memory/cutscene is triggered.
// ===============================

using UnityEngine;

public class ShrineTrigger : MonoBehaviour
{
    [Header("Shrine Settings")]
    public string memoryName = "Kallarai Memory 1";   // Which memory this shrine unlocks
    public bool oneTimeTrigger = true;                // Trigger only once

    private bool _triggered = false;

    [Header("Visuals & Audio")]
    public GameObject shrineEffect;                   // Optional particle effect
    public AudioClip shrineSound;                     // Optional sound
    private AudioSource _audio;

    private void Start()
    {
        if (shrineSound != null)
        {
            _audio = gameObject.AddComponent<AudioSource>();
            _audio.clip = shrineSound;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered && oneTimeTrigger) return;

        if (other.CompareTag("Player"))
        {
            _triggered = true;

            Debug.Log($"Shrine Activated: {memoryName}");

            // TODO: Call your cutscene / dialogue manager here
            // Example: CutsceneManager.Instance.Play(memoryName);

            if (shrineEffect) Instantiate(shrineEffect, transform.position, Quaternion.identity);
            if (_audio) _audio.Play();
        }
    }
}
