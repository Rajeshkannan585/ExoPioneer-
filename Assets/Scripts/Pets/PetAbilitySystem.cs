using UnityEngine;
using System.Collections;

public class PetAbilitySystem : MonoBehaviour
{
    public enum AbilityType { HealAura, FireBurst, Shield, SpeedBoost }

    public AbilityType petAbility = AbilityType.HealAura;  // Select pet’s ability
    public float abilityCooldown = 10f;                     // Cooldown between abilities
    public float abilityRange = 5f;                         // Range to affect player or enemies
    public float abilityPower = 20f;                        // Strength of the ability
    public ParticleSystem abilityEffect;                    // Visual effect for ability
    public AudioClip abilitySound;                          // Sound when ability triggers

    private float nextUseTime = 0f;
    private Transform player;
    private AudioSource audioSource;
    private PetController petController;

    void Start()
    {
        player = GetComponent<PetController>().player;
        petController = GetComponent<PetController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time >= nextUseTime)
        {
            // Auto activate ability periodically
            StartCoroutine(ActivateAbility());
            nextUseTime = Time.time + abilityCooldown;
        }
    }

    IEnumerator ActivateAbility()
    {
        Debug.Log("Pet uses ability: " + petAbility.ToString());

        // Play visual and sound effects
        if (abilityEffect != null)
            abilityEffect.Play();
        if (audioSource != null && abilitySound != null)
            audioSource.PlayOneShot(abilitySound);

        switch (petAbility)
        {
            case AbilityType.HealAura:
                HealPlayer();
                break;
            case AbilityType.FireBurst:
                FireBurstAttack();
                break;
            case AbilityType.Shield:
                ShieldBoost();
                break;
            case AbilityType.SpeedBoost:
                SpeedBoost();
                break;
        }

        yield return new WaitForSeconds(2f); // short ability duration
    }

    void HealPlayer()
    {
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.Heal(abilityPower);
            Debug.Log("💚 Pet healed player for " + abilityPower);
        }
    }

    void FireBurstAttack()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, abilityRange);
        foreach (Collider enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Debug.Log("🔥 Pet fire-bursts enemy: " + enemy.name);
                // Optional: apply damage script here
            }
        }
    }

    void ShieldBoost()
    {
        Debug.Log("🛡️ Shield boost activated!");
        // Example: player gets temporary defense (you can add your own PlayerShield script)
    }

    void SpeedBoost()
    {
        Debug.Log("💨 Speed boost active!");
        if (petController != null)
        {
            petController.followSpeed *= 1.5f;
            Invoke(nameof(ResetSpeed), 5f);
        }
    }

    void ResetSpeed()
    {
        if (petController != null)
            petController.followSpeed /= 1.5f;
    }
}
