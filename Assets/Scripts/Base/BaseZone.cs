using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseZone : MonoBehaviour
{
    [Header("Zone Settings")]
    public string zoneName;
    public bool isUnlocked;
    public Transform buildSpot;
    public Color unlockedColor = Color.green;
    public Color lockedColor = Color.red;

    [Header("UI References")]
    public Image zoneIcon;
    public TextMeshProUGUI zoneLabel;

    private void Start()
    {
        UpdateZoneVisual();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isUnlocked)
            {
                UnlockZone();
            }
        }
    }

    public void UnlockZone()
    {
        isUnlocked = true;
        PlayerPrefs.SetInt("BaseZone_" + zoneName, 1);
        UpdateZoneVisual();
        Debug.Log($"🏕️ Base Zone Unlocked: {zoneName}");
    }

    public void LoadZoneState()
    {
        isUnlocked = PlayerPrefs.GetInt("BaseZone_" + zoneName, 0) == 1;
        UpdateZoneVisual();
    }

    void UpdateZoneVisual()
    {
        if (zoneIcon != null)
            zoneIcon.color = isUnlocked ? unlockedColor : lockedColor;

        if (zoneLabel != null)
            zoneLabel.text = isUnlocked ? $"🏗️ {zoneName}" : $"🔒 {zoneName}";
    }
}
