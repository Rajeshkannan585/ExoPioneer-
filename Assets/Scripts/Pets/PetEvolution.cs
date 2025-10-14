using UnityEngine;

public class PetEvolution : MonoBehaviour
{
    public GameObject baseForm;              // Current form of pet
    public GameObject evolvedForm;           // Prefab of evolved pet
    public int evolveLevel = 5;              // Level required to evolve
    public AudioClip evolveSound;            // Sound effect for evolution
    public ParticleSystem evolveEffect;      // Visual effect during evolution

    private PetLevelSystem levelSystem;
    private AudioSource audioSource;
    private bool hasEvolved = false;

    void Start()
    {
        levelSystem = GetComponent<PetLevelSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check if level meets requirement and not yet evolved
        if (!hasEvolved && levelSystem.currentLevel >= evolveLevel)
        {
            Evolve();
        }
    }

    void Evolve()
    {
        hasEvolved = true;
        Debug.Log("🔥 Pet has evolved into a stronger form!");

        // Play sound and particle effect
        if (audioSource != null && evolveSound != null)
            audioSource.PlayOneShot(evolveSound);

        if (evolveEffect != null)
            evolveEffect.Play();

        // Switch model
        if (baseForm != null) baseForm.SetActive(false);
        if (evolvedForm != null) evolvedForm.SetActive(true);

        // Boost stats more dramatically
        levelSystem.baseDamage *= 1.5f;
        levelSystem.baseSpeed *= 1.3f;

        // Reapply updated stats to controller
        levelSystem.SendMessage("ApplyStats");
    }
}
