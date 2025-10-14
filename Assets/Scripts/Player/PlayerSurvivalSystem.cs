using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSurvivalSystem : MonoBehaviour
{
    [Header("Core Stats")]
    public float health = 100f;
    public float oxygen = 100f;
    public float hunger = 100f;
    public float stamina = 100f;
    public float radiation = 0f;

    [Header("Drain Rates")]
    public float oxygenDrainRate = 2f;
    public float hungerDrainRate = 1f;
    public float staminaDrainRate = 5f;
    public float radiationIncreaseRate = 0.5f;

    [Header("UI")]
    public Slider healthBar;
    public Slider oxygenBar;
    public Slider hungerBar;
    public Slider staminaBar;
    public Slider radiationBar;
    public TextMeshProUGUI warningText;

    [Header("Audio & Effects")]
    public AudioSource warningSound;
    public AudioClip lowOxygenClip;
    public AudioClip damageClip;

    private bool inRadiationZone = false;
    private bool inSafeBase = false;

    void Update()
    {
        UpdateStats();
        UpdateUI();
        CheckWarnings();
    }

    void UpdateStats()
    {
        if (!inSafeBase)
        {
            oxygen -= oxygenDrainRate * Time.deltaTime;
            hunger -= hungerDrainRate * Time.deltaTime;
            stamina = Mathf.Max(0, stamina - (staminaDrainRate * Time.deltaTime * 0.1f));
        }

        if (inRadiationZone)
            radiation += radiationIncreaseRate * Time.deltaTime;
        else
            radiation = Mathf.Max(0, radiation - radiationIncreaseRate * Time.deltaTime * 0.5f);

        if (radiation >= 100f || hunger <= 0f || oxygen <= 0f)
        {
            TakeDamage(15f * Time.deltaTime);
        }
    }

    void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            PlayerDied();
        }
    }

    void PlayerDied()
    {
        warningText.text = "💀 YOU DIED – Respawning...";
        Invoke(nameof(RespawnPlayer), 3f);
    }

    void RespawnPlayer()
    {
        transform.position = Vector3.zero; // spawn point
        health = 100f;
        oxygen = 100f;
        hunger = 100f;
        stamina = 100f;
        radiation = 0f;
        warningText.text = "";
    }

    void CheckWarnings()
    {
        if (oxygen < 25f)
        {
            warningText.text = "⚠️ Low Oxygen!";
            if (!warningSound.isPlaying) warningSound.PlayOneShot(lowOxygenClip);
        }
        else if (hunger < 20f)
        {
            warningText.text = "🍖 You are starving!";
        }
        else if (radiation > 70f)
        {
            warningText.text = "☢️ High Radiation!";
        }
        else
        {
            warningText.text = "";
        }
    }

    void UpdateUI()
    {
        healthBar.value = health / 100f;
        oxygenBar.value = oxygen / 100f;
        hungerBar.value = hunger / 100f;
        staminaBar.value = stamina / 100f;
        radiationBar.value = radiation / 100f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RadiationZone")) inRadiationZone = true;
        if (other.CompareTag("BaseZone")) inSafeBase = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RadiationZone")) inRadiationZone = false;
        if (other.CompareTag("BaseZone")) inSafeBase = false;
    }
}
