using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager instance;

    // Player health variables
    public int playerHealth = 100;
    public int maxHealth = 100;

    void Awake()
    {
        // Ensure only one instance exists across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Player takes damage
    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        if (playerHealth < 0) playerHealth = 0;

        Debug.Log("Player took damage: " + amount);

        if (playerHealth <= 0)
        {
            PlayerDied();
        }
    }

    // Player heals
    public void HealPlayer(int amount)
    {
        playerHealth += amount;
        if (playerHealth > maxHealth)
            playerHealth = maxHealth;

        Debug.Log("Player healed: " + amount + " | Current health: " + playerHealth);
    }

    // Called when health reaches 0
    private void PlayerDied()
    {
        Debug.Log("Player died! Game Over.");
        // Add respawn or game over logic here
    }
}
