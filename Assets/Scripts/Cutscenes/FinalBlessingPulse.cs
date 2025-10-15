using UnityEngine;

public class FinalBlessingPulse : MonoBehaviour
{
    public ParticleSystem pulseFX;
    public Light haloLight;
    public float pulseDuration = 8f;
    public float maxIntensity = 800f;

    void OnEnable() => StartCoroutine(Pulse());

    System.Collections.IEnumerator Pulse()
    {
        float t = 0;
        if (pulseFX && !pulseFX.isPlaying) pulseFX.Play();
        while (t < pulseDuration)
        {
            t += Time.deltaTime;
            float k = Mathf.Sin((t / pulseDuration) * Mathf.PI); // up then down
            if (haloLight) haloLight.intensity = Mathf.Lerp(0, maxIntensity, k);
            yield return null;
        }
        if (haloLight) haloLight.intensity = 0;
    }
}
