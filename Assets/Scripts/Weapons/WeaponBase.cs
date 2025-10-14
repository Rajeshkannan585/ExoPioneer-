using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public string weaponName = "Default Weapon"; // Weapon name
    public float damage = 20f;                   // Damage per hit
    public float range = 50f;                    // Effective range
    public float fireRate = 0.3f;                // Time between shots
    public int maxAmmo = 30;                     // Ammo per clip
    public int currentAmmo;                      // Current ammo count
    public bool isMelee = false;                 // Melee weapon flag
    public ParticleSystem muzzleFlash;           // Shooting effect
    public AudioClip fireSound;                  // Shooting sound
    public AudioClip reloadSound;                // Reload sound
    public Transform firePoint;                  // Where bullets fire from

    private AudioSource audioSource;
    private float nextFireTime = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            if (isMelee)
                MeleeAttack();
            else
                Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("Out of ammo!");
            return;
        }

        nextFireTime = Time.time + fireRate;
        currentAmmo--;

        if (muzzleFlash != null)
            muzzleFlash.Play();

        if (audioSource && fireSound)
            audioSource.PlayOneShot(fireSound);

        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            Debug.Log(weaponName + " hit: " + hit.transform.name);

            if (hit.transform.CompareTag("Enemy") || hit.transform.CompareTag("Animal"))
            {
                // Example: apply damage
                hit.transform.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    void MeleeAttack()
    {
        nextFireTime = Time.time + fireRate;

        if (audioSource && fireSound)
            audioSource.PlayOneShot(fireSound);

        Collider[] enemies = Physics.OverlapSphere(firePoint.position, range);
        foreach (Collider enemy in enemies)
        {
            if (enemy.CompareTag("Enemy") || enemy.CompareTag("Animal"))
            {
                enemy.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
        }

        Debug.Log(weaponName + " melee swing!");
    }

    void Reload()
    {
        currentAmmo = maxAmmo;
        if (audioSource && reloadSound)
            audioSource.PlayOneShot(reloadSound);
        Debug.Log("Reloaded " + weaponName);
    }
}
