using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class SkyBossController : MonoBehaviour
{
    [Header("Boss Core")]
    public Transform target;                     // Player or mount
    public float maxHealth = 5000f;
    public float turnSpeed = 45f;
    public float cruiseAccel = 30f;
    public float phase2Threshold = 0.66f;        // 66% HP
    public float phase3Threshold = 0.33f;        // 33% HP

    [Header("Attacks")]
    public GameObject laserPrefab;
    public Transform[] laserPoints;
    public float laserRatePhase1 = 0.8f;
    public float laserRatePhase2 = 1.5f;
    public float laserRatePhase3 = 2.2f;
    public float projectileSpeed = 120f;

    [Header("Dive / Ram")]
    public float diveCooldown = 8f;
    public float diveForce = 1500f;

    [Header("Summons")]
    public GameObject minionPrefab;              // SkyEnemyAI prefab
    public int minionsPerSummon = 3;
    public float summonCooldown = 15f;
    public Transform[] summonSpawns;

    [Header("Visuals & Audio")]
    public GameObject phaseShiftFX;
    public GameObject deathExplosionFX;
    public AudioClip phaseShiftSFX;
    public AudioClip deathSFX;

    [Header("Rewards & Story")]
    public bool rescueInsteadOfKill = true;      // If true, "cleanse corruption" ending
    public GameObject rescuedAllyPrefab;         // Optional: spawns ally creature
    public GameObject lootChestPrefab;
    public int coinReward = 1500;

    private Rigidbody rb;
    private float health;
    private int phase = 1;
    private float nextShootTime;
    private float nextDiveTime;
    private float nextSummonTime;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();

        if (target == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null) target = p.transform;
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // Slow cruise towards/around target
        Vector3 toTarget = (target.position - transform.position);
        Quaternion desired = Quaternion.LookRotation(toTarget.normalized, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desired, turnSpeed * Time.fixedDeltaTime);

        rb.AddForce(transform.forward * cruiseAccel, ForceMode.Acceleration);
    }

    void Update()
    {
        if (target == null) return;

        UpdatePhase();

        // Shooting cadence by phase
        float rate = phase == 1 ? laserRatePhase1 : phase == 2 ? laserRatePhase2 : laserRatePhase3;
        if (Time.time >= nextShootTime)
        {
            Barrage();
            nextShootTime = Time.time + 1f / rate;
        }

        // Periodic dive/rams
        if (Time.time >= nextDiveTime)
        {
            StartCoroutine(DiveAttack());
            nextDiveTime = Time.time + diveCooldown;
        }

        // Summon minions
        if (phase >= 2 && Time.time >= nextSummonTime && minionPrefab != null && summonSpawns.Length > 0)
        {
            SummonWave();
            nextSummonTime = Time.time + summonCooldown;
        }
    }

    void UpdatePhase()
    {
        float h = health / maxHealth;
        int newPhase = phase;
        if (h <= phase3Threshold) newPhase = 3;
        else if (h <= phase2Threshold) newPhase = 2;
        else newPhase = 1;

        if (newPhase != phase)
        {
            phase = newPhase;
            if (phaseShiftFX) Instantiate(phaseShiftFX, transform.position, Quaternion.identity);
            if (audioSource && phaseShiftSFX) audioSource.PlayOneShot(phaseShiftSFX);
            // Slight stat changes per phase
            turnSpeed += 10f * phase;      // tougher turns
            cruiseAccel += 8f * phase;
        }
    }

    void Barrage()
    {
        if (laserPrefab == null || laserPoints == null) return;
        foreach (var fp in laserPoints)
        {
            if (fp == null) continue;
            GameObject shot = Instantiate(laserPrefab, fp.position, fp.rotation);
            Rigidbody srb = shot.GetComponent<Rigidbody>();
            if (srb != null) srb.velocity = fp.forward * projectileSpeed;
        }
    }

    IEnumerator DiveAttack()
    {
        // Quick wind-up, then forceful dive toward target
        yield return new WaitForSeconds(0.5f);
        Vector3 dir = (target.position - transform.position).normalized;
        rb.AddForce(dir * diveForce, ForceMode.VelocityChange);
    }

    void SummonWave()
    {
        for (int i = 0; i < minionsPerSummon; i++)
        {
            Transform sp = summonSpawns[Random.Range(0, summonSpawns.Length)];
            Instantiate(minionPrefab, sp.position, sp.rotation);
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0f) OnBossDefeated();
    }

    void OnCollisionEnter(Collision c)
    {
        // Self-damage if boss rams terrain hard
        if (c.relativeVelocity.magnitude > 15f) TakeDamage(c.relativeVelocity.magnitude * 3f);
    }

    void OnBossDefeated()
    {
        // FX & sound
        if (deathExplosionFX) Instantiate(deathExplosionFX, transform.position, Quaternion.identity);
        if (audioSource && deathSFX) audioSource.PlayOneShot(deathSFX);

        // Reward
        PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins", 0) + coinReward);

        // Story branch
        if (rescueInsteadOfKill && rescuedAllyPrefab != null)
        {
            // “Cleanse corruption” outcome — transform boss into ally creature
            Instantiate(rescuedAllyPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            // Drop loot chest
            if (lootChestPrefab) Instantiate(lootChestPrefab, transform.position + Vector3.up, Quaternion.identity);
        }

        // Optional: trigger cutscene via event GameObject
        var cut = FindObjectOfType<PetCameraFocus>();
        if (cut != null)
        {
            // You can start a special cutscene here if desired
        }

        Destroy(gameObject, 0.2f);
    }
}
