// ==================================
// BossAI.cs
// Controls boss behaviour: movement, attack, death
// ==================================

using UnityEngine;

namespace ExoPioneer.BossSystem
{
    public class BossAI : MonoBehaviour
    {
        [Header("Boss Stats")]
        public float health = 500f;          // Boss health
        public float attackDamage = 30f;     // Damage per hit
        public float attackRange = 5f;       // Distance to attack player
        public float attackCooldown = 3f;    // Time between attacks

        private float lastAttackTime;
        public Transform player;

        void Update()
        {
            if (player == null) return;

            // Always face the player
            transform.LookAt(player);

            float distance = Vector3.Distance(transform.position, player.position);

            // If player is close enough → attack
            if (distance <= attackRange && Time.time > lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }

        void AttackPlayer()
        {
            Debug.Log("Boss attacks player for " + attackDamage + " damage!");
            // TODO: Connect to PlayerHealth system
        }

        public void TakeDamage(float dmg)
        {
            health -= dmg;
            Debug.Log("Boss took " + dmg + " damage! Remaining: " + health);

            if (health <= 0) Die();
        }

        void Die()
        {
            Debug.Log("Boss Defeated!");
            // TODO: Drop rare loot, trigger cutscene, unlock story
            Destroy(gameObject);
        }
    }
}
