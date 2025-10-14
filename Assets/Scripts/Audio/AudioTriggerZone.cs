using UnityEngine;

public class AudioTriggerZone : MonoBehaviour
{
    public AudioClip ambientClip;
    public AudioClip musicClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ambientClip) AudioManager.Instance.PlayAmbient(ambientClip);
            if (musicClip) AudioManager.Instance.PlayMusic(musicClip);
        }
    }
}
