using UnityEngine;

public class WeaponUpgradeSystem : MonoBehaviour
{
    [Header("Upgrade Stats")]
    public int weaponLevel = 1;              // Current upgrade level
    public int maxLevel = 5;                 // Max upgrade level
    public float damageMultiplier = 1.2f;    // Damage increase per level
    public float rangeMultiplier = 1.1f;     // Range increase per level
    public float fireRateBoost = 0.05f;      // Fire rate decrease per level (faster shots)

    [Header("Upgrade Costs")]
    public int[] upgradeCosts = { 100, 250, 500, 1000 }; // Coins required per level
    public int playerCoins = 1000;                       // Player’s available coins

    [Header("Visual Upgrade")]
    public MeshRenderer weaponRenderer;      // Reference to weapon model renderer
    public Material[] skinLevels;            // Materials for different rarity levels

    private WeaponBase weaponBase;

    void Start()
    {
        weaponBase = GetComponent<WeaponBase>();
        ApplyUpgradeVisual();
        LoadUpgradeData();
    }

    public void UpgradeWeapon()
    {
        if (weaponLevel >= maxLevel)
        {
            Debug.Log("⚠️ Weapon already at max level!");
            return;
        }

        int cost = upgradeCosts[Mathf.Min(weaponLevel - 1, upgradeCosts.Length - 1)];
        if (playerCoins < cost)
        {
            Debug.Log("❌ Not enough coins!");
            return;
        }

        playerCoins -= cost;
        weaponLevel++;

        // Apply stat boosts
        weaponBase.damage *= damageMultiplier;
        weaponBase.range *= rangeMultiplier;
        weaponBase.fireRate -= fireRateBoost;
        weaponBase.fireRate = Mathf.Max(0.05f, weaponBase.fireRate);

        ApplyUpgradeVisual();
        SaveUpgradeData();

        Debug.Log("🎯 Upgraded " + weaponBase.weaponName + " to Level " + weaponLevel);
    }

    void ApplyUpgradeVisual()
    {
        if (weaponRenderer != null && skinLevels.Length > 0)
        {
            int index = Mathf.Min(weaponLevel - 1, skinLevels.Length - 1);
            weaponRenderer.material = skinLevels[index];
        }
    }

    void SaveUpgradeData()
    {
        PlayerPrefs.SetInt(weaponBase.weaponName + "_Level", weaponLevel);
        PlayerPrefs.Save();
    }

    void LoadUpgradeData()
    {
        weaponLevel = PlayerPrefs.GetInt(weaponBase.weaponName + "_Level", 1);
        ApplyUpgradeVisual();
    }
}
