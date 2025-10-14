using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponHUD : MonoBehaviour
{
    public WeaponManager weaponManager;         // Reference to player's weapon manager
    public TextMeshProUGUI weaponNameText;      // Weapon name text
    public TextMeshProUGUI ammoText;            // Ammo count text
    public Image weaponIcon;                    // Icon for current weapon
    public Sprite defaultIcon;                  // Fallback icon if none assigned
    public Animator reloadAnimator;             // Optional reload animation

    private WeaponBase currentWeapon;

    void Update()
    {
        // Get the current active weapon
        currentWeapon = weaponManager.GetCurrentWeapon();
        if (currentWeapon != null)
        {
            UpdateHUD();
        }
        else
        {
            weaponNameText.text = "No Weapon";
            ammoText.text = "- / -";
            weaponIcon.sprite = defaultIcon;
        }
    }

    void UpdateHUD()
    {
        // Update name
        weaponNameText.text = currentWeapon.weaponName;

        // Update ammo info
        ammoText.text = currentWeapon.currentAmmo + " / " + currentWeapon.maxAmmo;

        // Update icon (if your weapon prefabs have a custom icon)
        Sprite icon = currentWeapon.GetComponent<WeaponIcon>()?.iconSprite;
        weaponIcon.sprite = icon != null ? icon : defaultIcon;

        // Reload animation trigger
        if (Input.GetKeyDown(KeyCode.R) && reloadAnimator != null)
        {
            reloadAnimator.SetTrigger("Reload");
        }
    }
}
