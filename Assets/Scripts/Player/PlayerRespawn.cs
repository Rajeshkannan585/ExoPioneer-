using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;       // Location where the player will respawn
    public GameObject gameOverUI;        // UI to show when player dies
    public float respawnDelay = 3f;      // Time before respawning

    private PlayerHealth playerHealth;   // Reference to PlayerHealth component

    void Start()
    {
        // Get PlayerHealth component from the same GameObject
        playerHealth = GetComponent<PlayerHealth>();

        // Hide Game Over UI at the start
        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    void Update()
    {
        // Check if player is dead
        if (playerHealth != null && playerHealth.GetCurrentHealth() <= 0f)
        {
            StartCoroutine(HandleDeath());
        }
    }

    private System.Collections.IEnumerator HandleDeath()
    {
        // Prevent multiple triggers
        if (gameOverUI != null && !gameOverUI.activeSelf)
        {
            gameOverUI.SetActive(true); // Show Game Over UI
            Debug.Log("Game Over! Respawning soon...");
            yield return new WaitForSeconds(respawnDelay);

            // Respawn player
            transform.position = respawnPoint.position;
            playerHealth.Heal(playerHealth.maxHealth); // Restore full health
            gameOverUI.SetActive(false);
        }
    }
}
