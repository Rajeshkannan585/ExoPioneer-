using UnityEngine;

public class EnemyDamageReceiver : MonoBehaviour
{
    public EnemyAI enemyAI;
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ApplyDamage(float amount)
    {
        currentHealth -= amount;
        enemyAI.TakeDamage(amount);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            enemyAI.Die();
        }
    }
}
