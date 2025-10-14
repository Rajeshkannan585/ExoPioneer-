using UnityEngine;

public class PetLevelSystem : MonoBehaviour
{
    public int currentLevel = 1;               // Pet's current level
    public int currentXP = 0;                  // Current XP
    public int xpToNextLevel = 100;            // XP needed for next level

    public float baseDamage = 10f;             // Base attack power
    public float baseSpeed = 4f;               // Base movement speed
    public float levelMultiplier = 1.2f;       // Growth rate per level

    private PetController petController;

    void Start()
    {
        petController = GetComponent<PetController>();
        ApplyStats();
    }

    // Add experience points
    public void AddXP(int amount)
    {
        currentXP += amount;
        Debug.Log("Pet gained " + amount + " XP! Current XP: " + currentXP);

        // Level up if XP exceeds threshold
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    // Increase level and upgrade stats
    void LevelUp()
    {
        currentXP -= xpToNextLevel;
        currentLevel++;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * levelMultiplier);

        // Boost stats
        baseDamage *= levelMultiplier;
        baseSpeed *= levelMultiplier;

        // Apply changes to PetController
        ApplyStats();

        Debug.Log("🎉 Pet leveled up to Level " + currentLevel + "! Damage: " + baseDamage + ", Speed: " + baseSpeed);
    }

    // Update PetController stats
    void ApplyStats()
    {
        if (petController != null)
        {
            petController.attackDamage = baseDamage;
            petController.followSpeed = baseSpeed;
        }
    }
}
