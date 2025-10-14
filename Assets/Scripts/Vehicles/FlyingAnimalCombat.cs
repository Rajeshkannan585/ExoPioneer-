using UnityEngine;
using System.Collections;

public class FlyingAnimalCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public GameObject fireballPrefab;        // Projectile prefab
    public Transform firePoint;              // Where projectile spawns
    public float fireRate = 1f;              // Shots per second
    public float fireballSpeed = 80f;        // Speed of fireball
    public float fireballDamage = 50f;       // Damage dealt
    public float energy = 100f;              // Energy pool for attacks
    public float energyUse = 10f;            // Energy per shot

    [Header("Visuals & Audio")]
    public ParticleSystem flameMuzzleFX;
    public AudioClip fireSound;
    public AudioClip noEnergySound;

    private float nextFireTime = 0f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            TryShoot();
        }
    }

    void TryShoot()
    {
        if (energy < energyUse)
        {
            if (audioSource && noEnergySound)
                audioSource.PlayOneShot(noEnergySound);
            Debug.Log("⚠️ Not enough energy to shoot!");
            return;
        }

        nextFireTime = Time.time + 1f / fireRate;
        energy -= energyUse;
        ShootFireball();
    }

    void ShootFireball()
    {
        if (firePoint == null || fireballPrefab == null) return;

        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = firePoint.forward * fireballSpeed;

        if (flameMuzzleFX != null)
            flameMuzzleFX.Play();

        if (audioSource && fireSound)
            audioSource.PlayOneShot(fireSound);

        Debug.Log("🔥 Fireball launched!");
    }

    public void RechargeEnergy(float amount)
    {
        energy = Mathf.Min(energy + amount, 100f);
        Debug.Log("⚡ Energy recharged to: " + energy);
    }
}
