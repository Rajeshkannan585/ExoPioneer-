using UnityEngine;

public class PetBondSystem : MonoBehaviour
{
    public int bondLevel = 1;                   // Pet bond level
    public float bondXP = 0f;                   // Current affection progress
    public float bondXPToNext = 100f;           // XP needed for next bond level
    public float affectionRate = 5f;            // How fast bond increases (by feeding, healing, etc.)
    public float decayRate = 1f;                // Bond decay rate over time if ignored
    public float lastInteractionTime;           // Time of last pet interaction

    public AudioClip happySound;                // Sound when affection increases
    public AudioClip sadSound;                  // Sound when ignored
    private AudioSource audioSource;
    private Animator anim;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        lastInteractionTime = Time.time;
    }

    void Update()
    {
        // Decrease bond slowly if player ignores the pet
        if (Time.time - lastInteractionTime > 30f)
        {
            bondXP -= decayRate * Time.deltaTime;
            bondXP = Mathf.Max(0f, bondXP);

            if (bondXP <= 0f)
            {
                if (audioSource && sadSound) audioSource.PlayOneShot(sadSound);
                if (anim) anim.SetTrigger("Sad");
            }
        }

        // Level up affection
        if (bondXP >= bondXPToNext)
        {
            bondXP -= bondXPToNext;
            bondLevel++;
            bondXPToNext *= 1.3f;  // harder next level
            Debug.Log("💖 Pet Bond Level Up! Now Level: " + bondLevel);
            if (audioSource && happySound) audioSource.PlayOneShot(happySound);
            if (anim) anim.SetTrigger("Happy");
        }
    }

    // Call this whenever player interacts with the pet
    public void IncreaseBond(float amount)
    {
        bondXP += amount * affectionRate;
        lastInteractionTime = Time.time;
        Debug.Log("Pet affection increased! Bond XP: " + bondXP);
        if (audioSource && happySound) audioSource.PlayOneShot(happySound);
        if (anim) anim.SetTrigger("Happy");
    }

    public void PetInteraction(string action)
    {
        switch (action)
        {
            case "Feed":
                IncreaseBond(10);
                Debug.Log("🍗 You fed your pet! Affection grew.");
                break;

            case "Play":
                IncreaseBond(15);
                Debug.Log("🎾 You played with your pet! Affection increased.");
                break;

            case "Heal":
                IncreaseBond(8);
                Debug.Log("💚 You healed your pet! Trust improved.");
                break;

            default:
                IncreaseBond(5);
                break;
        }
    }
}
