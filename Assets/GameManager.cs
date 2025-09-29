using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton pattern

    public int playerHealth = 100;
    public int maxHealth = 100;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 🛑 Player takes damage
    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        if (playerHealth < 0) playerHealth = 0;

        Debug.Log("Player took damage: " + amount + " | Health now: " + playerHealth);

        if (playerHealth <= 0)
        {
            PlayerDied();
        }
    }

    // ❤️ Player heals
    public void HealPlayer(int amount)
    {
        playerHealth += amount;
        if (playerHealth > maxHealth) playerHealth = maxHealth;

        Debug.Log("Player healed: " + amount + " | Health now: " + playerHealth);
    }

    // ☠️ If health reaches 0
    private void PlayerDied()
    {
        Debug.Log("Player died! Game Over.");
        // இங்கே respawn logic அல்லது game over screen வைக்கலாம்
    }
}
