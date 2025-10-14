using UnityEngine;
using TMPro;
using System.Collections;

public class PostCreditsFlightController : MonoBehaviour
{
    public Transform player;
    public Transform pet;
    public float flightSpeed = 20f;
    public float rotateSpeed = 45f;
    public Camera mainCamera;
    public TextMeshProUGUI messageText;
    public AudioSource musicSource;
    public AudioClip skyTheme;

    void Start()
    {
        if (musicSource && skyTheme)
        {
            musicSource.clip = skyTheme;
            musicSource.loop = true;
            musicSource.Play();
        }
        messageText.text = "🌅 The Sky Clears...\nYou and Kallarai’s spirit fly free.";
        StartCoroutine(FadeMessage());
    }

    void Update()
    {
        player.position += player.forward * flightSpeed * Time.deltaTime;
        pet.position = player.position + player.right * 4f + Vector3.up * Mathf.Sin(Time.time) * 2f;

        // Smooth orbit camera
        mainCamera.transform.position = player.position - player.forward * 10 + Vector3.up * 3;
        mainCamera.transform.LookAt(player.position + Vector3.up * 2);

        if (Input.GetKey(KeyCode.A))
            player.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            player.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.W))
            player.position += Vector3.up * Time.deltaTime * 5;
        if (Input.GetKey(KeyCode.S))
            player.position -= Vector3.up * Time.deltaTime * 5;
    }

    IEnumerator FadeMessage()
    {
        messageText.alpha = 0;
        for (float t = 0; t < 2f; t += Time.deltaTime)
        {
            messageText.alpha = Mathf.Lerp(0, 1, t / 2f);
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        messageText.alpha = 0;
    }
}
