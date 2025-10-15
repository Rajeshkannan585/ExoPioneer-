using UnityEngine;
using TMPro;

public class KallaraiVoicePlayer : MonoBehaviour
{
    public AudioSource voiceSource;
    public AudioClip[] voiceLines; // 0..n
    public TextMeshProUGUI subtitleText;
    [TextArea(2,4)] public string[] subtitles; // same indexing as voiceLines
    public float delayBetween = 1.2f;

    void OnEnable() => StartCoroutine(PlaySequence());

    System.Collections.IEnumerator PlaySequence()
    {
        for (int i = 0; i < voiceLines.Length; i++)
        {
            if (i < subtitles.Length && subtitleText != null)
                subtitleText.text = subtitles[i];

            if (voiceLines[i] != null && voiceSource != null)
            {
                voiceSource.clip = voiceLines[i];
                voiceSource.Play();
                yield return new WaitForSeconds(voiceSource.clip.length + delayBetween);
            }
            else
            {
                yield return new WaitForSeconds(2f);
            }
        }
        if (subtitleText) subtitleText.text = "";
    }
}
