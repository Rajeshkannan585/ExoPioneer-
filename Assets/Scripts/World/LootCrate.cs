using UnityEngine;

public class LootCrate : MonoBehaviour
{
    [Header("Crate Settings")]
    public GameObject[] possibleLoot;    // List of weapon prefabs or pickups
    public GameObject openEffect;        // Particle or animation when opened
    public AudioClip openSound;          // Sound when opened
    public int coinReward = 100;         // Coin bonus for opening

    [Header("Crate Interaction")]
    public string openMessage = "Press E to open crate";
    private bool playerInRange = false;
    private bool isOpened = false;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            playerInRange = true;
            Debug.Log(openMessage);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isOpened)
        {
            OpenCrate();
        }
    }

    void OpenCrate()
    {
        isOpened = true;
        Debug.Log("🎁 Loot Crate opened!");

        if (openEffect != null)
            Instantiate(openEffect, transform.position, Quaternion.identity);

        if (audioSource != null && openSound != null)
            audioSource.PlayOneShot(openSound);

        // Random weapon spawn
        int randomIndex = Random.Range(0, possibleLoot.Length);
        GameObject loot = Instantiate(possibleLoot[randomIndex], transform.position + Vector3.up * 0.5f, Quaternion.identity);

        // Add coins
        PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins", 0) + coinReward);
        PlayerPrefs.Save();

        Debug.Log("💰 Player received " + coinReward + " coins and loot: " + loot.name);

        Destroy(gameObject, 2f); // remove crate after a short delay
    }
}
