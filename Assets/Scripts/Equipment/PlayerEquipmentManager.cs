using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerEquipmentManager : MonoBehaviour
{
    [Header("Equipped Items")]
    public EquipmentItem currentWeapon;
    public EquipmentItem currentArmor;

    [Header("UI")]
    public Image weaponIcon;
    public Image armorIcon;
    public TextMeshProUGUI weaponStatsText;
    public TextMeshProUGUI armorStatsText;

    private float baseAttack = 10f;
    private float baseDefense = 5f;
    private float baseHealth = 100f;
    private float baseEnergy = 100f;

    public float totalAttack => baseAttack + (currentWeapon != null ? currentWeapon.attackBonus : 0);
    public float totalDefense => baseDefense + (currentArmor != null ? currentArmor.defenseBonus : 0);
    public float totalHealth => baseHealth + (currentArmor != null ? currentArmor.healthBonus : 0);
    public float totalEnergy => baseEnergy + (currentArmor != null ? currentArmor.energyBonus : 0);

    void Start()
    {
        UpdateUI();
    }

    public void EquipItem(EquipmentItem item)
    {
        switch (item.type)
        {
            case EquipmentType.Weapon:
                currentWeapon = item;
                break;
            case EquipmentType.Armor:
                currentArmor = item;
                break;
        }
        UpdateUI();
        Debug.Log($"⚙️ Equipped: {item.itemName}");
    }

    void UpdateUI()
    {
        if (currentWeapon != null)
        {
            weaponIcon.sprite = currentWeapon.icon;
            weaponStatsText.text = $"⚔️ {currentWeapon.itemName}\n+{currentWeapon.attackBonus} ATK";
        }
        if (currentArmor != null)
        {
            armorIcon.sprite = currentArmor.icon;
            armorStatsText.text = $"🛡️ {currentArmor.itemName}\n+{currentArmor.defenseBonus} DEF";
        }
    }
}
