using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class SkyEnemyAI : MonoBehaviour
{
    [Header("Targeting")]
    public Transform target;                    // Usually the player or flying mount
    public float detectRange = 120f;
    public float keepDistance = 40f;

    [Header("Movement")]
    public float moveAccel = 40f;
    public float turnSpeed = 90f;
    public float strafeStrength = 15f;
    public float ramRange = 12f;
    public float ramForce = 800f;

    [Header("Combat")]
    public GameObject projectilePrefab;         // Reuse your FireballProjectile or laser prefab
    public Transform firePoint;
    public float fireRate = 1.2f;               // shots/sec
    public float projectileSpeed = 90f;
    public float aimLead = 0.25f;

    [Header("Dodge")]
    public float dodgeCooldown = 4f;
    public float dodgeImpulse = 18f;

    [Header("Vitals & Rewards")]
    public float maxHealth = 120f;
    public GameObject deathExplosionFX;
    public GameObject lootPrefab;               // Optional: drop pickup
    public int coinReward = 50;
    public int petXPReward = 15;

    private Rigidbody rb;
    private float health;
    private float nextFireTime;
    private float nextDodgeTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        health = maxHealth;

        if (target == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null) target = p.transform;
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 toTarget = target.position - transform.position;
        float dist = toTarget.magnitude;

        // Face target smoothly
        Quaternion look = Quaternion.LookRotation(toTarget.normalized, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, look, turnSpeed * Time.fixedDeltaTime);

        // Maintain distance (close in or back off)
        float forwardSign = dist > keepDistance ? 1f : -0.6f;
        rb.AddForce(transform.forward * (forwardSign * moveAccel), ForceMode.Acceleration);

        // Strafe to avoid straight lines
        Vector3 strafeDir = Vector3.Cross(Vector3.up, toTarget).normalized;
        rb.AddForce(strafeDir * strafeStrength, ForceMode.Acceleration);

        // Opportunistic ram
        if (dist <= ramRange)
        {
            rb.AddForce(transform.forward * ramForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }

        // Random dodge bursts
        if (Time.time >= nextDodgeTime)
        {
            Vector3 dodge = (Random.value < 0.5f ? transform.right : -transform.right) + Vector3.up * 0.3f;
            rb.AddForce(dodge.normalized * dodgeImpulse, ForceMode.VelocityChange);
            nextDodgeTime = Time.time + dodgeCooldown;
        }
    }

    void Update()
    {
        if (target == null) return;

        if (Time.time >= nextFireTime && projectilePrefab != null && firePoint != null)
        {
            ShootAtTarget();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void ShootAtTarget()
    {
        Vector3 toTarget = target.position - firePoint.position;

        // Simple aim lead
        Rigidbody trgRB = target.GetComponent<Rigidbody>();
        Vector3 lead = Vector3.zero;
        if (trgRB != null)
            lead = trgRB.velocity * aimLead;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation((toTarget + lead).normalized));
        Rigidbody prb = proj.GetComponent<Rigidbody>();
        if (prb != null) prb.velocity = proj.transform.forward * projectileSpeed;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0f) Die();
    }

    void OnCollisionEnter(Collision c)
    {
        // Take some self-damage on hard rams
        if (c.relativeVelocity.magnitude > 10f) TakeDamage(c.relativeVelocity.magnitude * 2f);
    }

    void Die()
    {
        if (deathExplosionFX) Instantiate(deathExplosionFX, transform.position, Quaternion.identity);
        if (lootPrefab) Instantiate(lootPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);

        // Reward coins
        PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins", 0) + coinReward);

        // Reward pet XP if present
        var pet = FindObjectOfType<PetLevelSystem>();
        if (pet) pet.AddXP(petXPReward);

        Destroy(gameObject);
    }
}
