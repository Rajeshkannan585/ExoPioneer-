using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    PetArmor
}

[System.Serializable]
public class EquipmentItem
{
    public string itemName;
    public EquipmentType type;
    public Sprite icon;
    public float attackBonus;
    public float defenseBonus;
    public float healthBonus;
    public float energyBonus;
}
