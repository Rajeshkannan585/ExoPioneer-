using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PetBondUI : MonoBehaviour
{
    public PetBondSystem petBond;          // Reference to pet bond system
    public Image bondBarFill;              // UI bar that fills based on affection XP
    public TextMeshProUGUI bondLevelText;  // Displays bond level
    public Image emotionIcon;              // Displays pet's emotion icon
    public Sprite happyIcon;
    public Sprite sadIcon;
    public Sprite angryIcon;
    public Sprite loveIcon;

    void Update()
    {
        if (petBond != null)
        {
            float fillAmount = petBond.bondXP / petBond.bondXPToNext;
            bondBarFill.fillAmount = Mathf.Clamp01(fillAmount);
            bondLevelText.text = "Bond Lv. " + petBond.bondLevel;

            // Change emotion icon based on affection
            UpdateEmotion();
        }
    }

    void UpdateEmotion()
    {
        if (petBond.bondXP < petBond.bondXPToNext * 0.25f)
            emotionIcon.sprite = sadIcon;
        else if (petBond.bondXP < petBond.bondXPToNext * 0.5f)
            emotionIcon.sprite = angryIcon;
        else if (petBond.bondXP < petBond.bondXPToNext * 0.9f)
            emotionIcon.sprite = happyIcon;
        else
            emotionIcon.sprite = loveIcon;
    }
}
