// ==================================
// AmmoSystem.cs
// Handles ammo count, reload, and UI updates
// ==================================

using UnityEngine;
using Mirror;

namespace ExoPioneer.Weapons
{
    public class AmmoSystem : NetworkBehaviour
    {
        [Header("Ammo Settings")]
        public int magazineSize = 30;     // Bullets per clip
        public int totalAmmo = 90;        // Extra ammo in reserve
        public float reloadTime = 2f;     // Seconds to reload

        [SyncVar] public int currentAmmo; // Ammo in current magazine
        private bool isReloading = false;

        void Start()
        {
            currentAmmo = magazineSize; // Start with full mag
        }

        void Update()
        {
            if (!isLocalPlayer) return;

            // Shoot logic handled by Weapon.cs
            if (Input.GetButtonDown("Fire1") && currentAmmo > 0 && !isReloading)
            {
                currentAmmo--; // Reduce ammo when firing
            }

            // Reload
            if (Input.GetKeyDown(KeyCode.R) && !isReloading && totalAmmo > 0 && currentAmmo < magazineSize)
            {
                StartCoroutine(Reload());
            }
        }

        System.Collections.IEnumerator Reload()
        {
            isReloading = true;
            Debug.Log("Reloading...");
            yield return new WaitForSeconds(reloadTime);

            int needed = magazineSize - currentAmmo;
            int loadAmount = Mathf.Min(needed, totalAmmo);

            currentAmmo += loadAmount;
            totalAmmo -= loadAmount;

            isReloading = false;
            Debug.Log("Reload complete. Current ammo: " + currentAmmo + "/" + totalAmmo);
        }

        public bool CanFire()
        {
            return currentAmmo > 0 && !isReloading;
        }
    }
}
