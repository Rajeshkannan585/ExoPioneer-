// ==================================
// MeleeWeapon.cs
// Handles melee combat (sword, knife, alien blade)
// ==================================

using UnityEngine;
using Mirror;

namespace ExoPioneer.Weapons
{
    public class MeleeWeapon : NetworkBehaviour
    {
        [Header("Melee Settings")]
        public int damage = 40;
        public float attackRange = 2f;
        public float attackCooldown = 1f;

        private float lastAttackTime;

        void Update()
        {
            if (!isLocalPlayer) return;

            if (Input.GetMouseButtonDown(0) && Time.time > lastAttackTime + attackCooldown)
            {
                lastAttackTime = Time.time;
                CmdMeleeAttack();
            }
        }

        [Command]
        void CmdMeleeAttack()
        {
            // Detect enemies in front of the player
            Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, attackRange))
            {
                // Apply damage to Player
                var playerHealth = hit.collider.GetComponent<ExoPioneer.PlayerSystem.PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }

                // Apply damage to Animal
                var animalHealth = hit.collider.GetComponent<ExoPioneer.Animals.AnimalHealth>();
                if (animalHealth != null)
                {
                    animalHealth.TakeDamage(damage);
                }

                // Apply damage to Boss
                var boss = hit.collider.GetComponent<ExoPioneer.BossSystem.BossAI>();
                if (boss != null)
                {
                    boss.TakeDamage(damage);
                }

                RpcPlayMeleeEffect(hit.point);
            }
        }

        [ClientRpc]
        void RpcPlayMeleeEffect(Vector3 hitPos)
        {
            Debug.Log("Melee hit at: " + hitPos);
            // TODO: Add hit VFX / SFX here
        }
    }
}
