// ==================================
// Weapon.cs
// Handles shooting logic and sync across multiplayer
// ==================================

using UnityEngine;
using Mirror;

namespace ExoPioneer.Weapons
{
    public class Weapon : NetworkBehaviour
    {
        [Header("Weapon Settings")]
        public GameObject projectilePrefab;
        public Transform firePoint;
        public float fireRate = 0.5f;
        public int damage = 25;

        private float lastFireTime;

        void Update()
        {
            if (!isLocalPlayer) return;

            if (Input.GetButtonDown("Fire1") && Time.time > lastFireTime + fireRate)
            {
                lastFireTime = Time.time;
                CmdShoot(firePoint.position, firePoint.forward);
            }
        }

        [Command]
        void CmdShoot(Vector3 pos, Vector3 dir)
        {
            GameObject proj = Instantiate(projectilePrefab, pos, Quaternion.LookRotation(dir));
            Projectile p = proj.GetComponent<Projectile>();
            if (p != null)
                p.damage = damage;

            NetworkServer.Spawn(proj);
        }
    }
}
