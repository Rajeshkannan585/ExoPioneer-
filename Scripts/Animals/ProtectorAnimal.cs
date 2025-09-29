// ==============================
// ProtectorAnimal.cs
// Animal that protects the player by auto-attacking enemies
// ==============================

using UnityEngine;
using UnityEngine.AI;

namespace ExoPioneer.Animals
{
    public class ProtectorAnimal : SupportAnimal
    {
        public float attackRange = 5f;
        public float attackCooldown = 2f;

        private float lastAttackTime;

        void Update()
        {
            base.Update(); // still follow player

            // Find nearest enemy
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject nearestEnemy = null;
            float minDist = Mathf.Infinity;

            foreach (var enemy in enemies)
            {
                float dist = Vector3.Distance(transform.position, enemy.transform.position);
                if (dist < minDist && dist <= attackRange)
                {
                    minDist = dist;
                    nearestEnemy = enemy;
                }
            }

            // Attack enemy if found
            if (nearestEnemy != null && Time.time > lastAttackTime + attackCooldown)
            {
                Attack(nearestEnemy);
                lastAttackTime = Time.time;
            }
        }
    }
}
