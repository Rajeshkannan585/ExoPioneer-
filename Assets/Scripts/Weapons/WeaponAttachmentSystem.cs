using UnityEngine;

public class WeaponAttachmentSystem : MonoBehaviour
{
    [Header("Attachment References")]
    public bool hasScope = false;
    public bool hasSilencer = false;
    public bool hasExtendedMag = false;
    public bool hasGrip = false;

    [Header("Scope Settings")]
    public float zoomFOV = 30f;                // Zoom level when scoped
    public float normalFOV = 60f;              // Normal camera view
    public Camera playerCamera;

    [Header("Stat Modifiers")]
    public float silencerDamageReduction = 0.9f; // 10% damage reduction
    public float silencerSoundReduction = 0.5f;
    public int extendedAmmoBoost = 15;          // Extra bullets
    public float gripRecoilReduction = 0.8f;    // 20% less recoil

    private WeaponBase weaponBase;
    private AudioSource weaponAudio;
    private float originalDamage;
    private int originalAmmo;
    private bool isScoped = false;

    void Start()
    {
        weaponBase = GetComponent<WeaponBase>();
        weaponAudio = GetComponent<AudioSource>();

        if (weaponBase != null)
        {
            originalDamage = weaponBase.damage;
            originalAmmo = weaponBase.maxAmmo;
        }

        ApplyAttachments();
    }

    void Update()
    {
        // Toggle scope (right-click or touch)
        if (hasScope && Input.GetMouseButtonDown(1))
        {
            ToggleScope();
        }
    }

    public void ApplyAttachments()
    {
        if (weaponBase == null) return;

        // Silencer
        if (hasSilencer)
        {
            weaponBase.damage *= silencerDamageReduction;
            if (weaponAudio != null)
                weaponAudio.volume *= silencerSoundReduction;
        }

        // Extended Mag
        if (hasExtendedMag)
            weaponBase.maxAmmo = originalAmmo + extendedAmmoBoost;

        // Grip (less recoil)
        if (hasGrip)
        {
            weaponBase.fireRate *= gripRecoilReduction;
        }
    }

    void ToggleScope()
    {
        if (playerCamera == null) return;

        isScoped = !isScoped;
        playerCamera.fieldOfView = isScoped ? zoomFOV : normalFOV;
        Debug.Log(isScoped ? "🎯 Scope ON" : "👁️ Scope OFF");
    }
}
