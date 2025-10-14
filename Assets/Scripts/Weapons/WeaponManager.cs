using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> weaponSlots = new List<GameObject>();   // All weapon objects
    public int currentWeaponIndex = 0;                              // Active weapon index
    public Transform weaponHolder;                                  // Parent transform for weapons

    private WeaponBase currentWeapon;

    void Start()
    {
        // Equip the first weapon by default
        if (weaponSlots.Count > 0)
            EquipWeapon(0);
    }

    void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        // Number key switching (1–5)
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) EquipWeapon(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) EquipWeapon(4);
#endif
    }

    public void EquipWeapon(int index)
    {
        if (index < 0 || index >= weaponSlots.Count)
        {
            Debug.LogWarning("Invalid weapon index!");
            return;
        }

        // Disable all weapons first
        foreach (GameObject weapon in weaponSlots)
            weapon.SetActive(false);

        // Enable the selected weapon
        weaponSlots[index].SetActive(true);
        currentWeaponIndex = index;
        currentWeapon = weaponSlots[index].GetComponent<WeaponBase>();

        Debug.Log("Equipped Weapon: " + currentWeapon.weaponName);
    }

    public void NextWeapon()
    {
        int nextIndex = (currentWeaponIndex + 1) % weaponSlots.Count;
        EquipWeapon(nextIndex);
    }

    public void PreviousWeapon()
    {
        int prevIndex = (currentWeaponIndex - 1 + weaponSlots.Count) % weaponSlots.Count;
        EquipWeapon(prevIndex);
    }

    public WeaponBase GetCurrentWeapon()
    {
        return currentWeapon;
    }
}
