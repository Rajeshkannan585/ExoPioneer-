using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PetArmorManager : MonoBehaviour
{
    public EquipmentItem equippedPetArmor;
    public Image armorIcon;
    public TextMeshProUGUI petArmorText;

    [Header("Pet Reference")]
    public GameObject pet;
    private PetAI petAI;

    void Start()
    {
        petAI = pet.GetComponent<PetAI>();
        UpdateUI();
    }

    public void EquipPetArmor(EquipmentItem item)
    {
        if (item.type != EquipmentType.PetArmor) return;

        equippedPetArmor = item;
        petAI.defense += item.defenseBonus;
        petAI.maxHealth += item.healthBonus;
        petAI.attackPower += item.attackBonus;

        UpdateUI();
        Debug.Log($"🐾 Pet Armor Equipped: {item.itemName}");
    }

    void UpdateUI()
    {
        if (equippedPetArmor != null)
        {
            armorIcon.sprite = equippedPetArmor.icon;
            petArmorText.text = $"🐉 {equippedPetArmor.itemName}\n+{equippedPetArmor.defenseBonus} DEF";
        }
    }
}
