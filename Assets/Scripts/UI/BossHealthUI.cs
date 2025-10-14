using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthUI : MonoBehaviour
{
    public Slider bossHealthSlider;           // Main health bar UI
    public TextMeshProUGUI bossNameText;      // Boss name display
    public CanvasGroup uiGroup;               // For fade in/out

    private SkyBossController boss;
    private bool active = false;

    void Update()
    {
        if (boss == null)
        {
            // Try to find boss in scene
            SkyBossController found = FindObjectOfType<SkyBossController>();
            if (found != null)
            {
                boss = found;
                bossNameText.text = found.name;
                StartCoroutine(FadeUI(true));
            }
            return;
        }

        if (boss != null && bossHealthSlider != null)
        {
            float hpPercent = Mathf.Clamp01(boss.GetHealthPercent());
            bossHealthSlider.value = hpPercent;

            // Fade out when boss dies
            if (hpPercent <= 0 && active)
            {
                StartCoroutine(FadeUI(false));
                active = false;
            }
        }
    }

    IEnumerator FadeUI(bool show)
    {
        float target = show ? 1 : 0;
        float speed = 2f;
        while (Mathf.Abs(uiGroup.alpha - target) > 0.05f)
        {
            uiGroup.alpha = Mathf.Lerp(uiGroup.alpha, target, Time.deltaTime * speed);
            yield return null;
        }
        uiGroup.alpha = target;
        uiGroup.blocksRaycasts = show;
        uiGroup.interactable = show;
        active = show;
    }
}
