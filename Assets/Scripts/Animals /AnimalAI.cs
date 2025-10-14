using UnityEngine;

public class AnimalAI : MonoBehaviour
{
    public float moveSpeed = 2f;          // Movement speed of the animal
    public float detectRange = 8f;        // Distance to detect the player
    public float attackRange = 2f;        // Range for attacking the player
    public float attackDamage = 10f;      // Damage dealt to the player
    public float attackCooldown = 2f;     // Time between attacks

    public Transform player;              // Player reference
    private Vector3 startPosition;        // Original animal position
    private float lastAttackTime;         // Timer for next attack

    void Start()
    {
        // Store the animal's starting position
        startPosition = transform.position;
    }

    void Update()
    {
        // Measure distance from player
        float distance = Vector3.Distance(transform.position, player.position);

        // If player is within detection range, move towards player
        if (distance < detectRange && distance > attackRange)
        {
            MoveTowardsPlayer();
        }
        // If player is very close, attack
        else if (distance <= attackRange)
        {
            AttackPlayer();
        }
        // If player is far away, return to start position
        else
        {
            ReturnToStart();
        }
    }

    void MoveTowardsPlayer()
    {
        // Move smoothly toward the player
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void ReturnToStart()
    {
        // Return to original position
        transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        // Attack only if cooldown is ready
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            // Example: reduce player's health (requires PlayerHealth script)
            Debug.Log("Animal attacks player for " + attackDamage + " damage!");
            // player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);

            // Play attack animation or sound here (optional)
        }
    }
}
