using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomizer : MonoBehaviour
{
    [Header("Model Parts")]
    public GameObject[] helmetOptions;
    public GameObject[] armorOptions;
    public SkinnedMeshRenderer suitRenderer;

    [Header("UI Elements")]
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    public Button nextHelmetButton;
    public Button nextArmorButton;
    public Button saveButton;

    private int currentHelmet = 0;
    private int currentArmor = 0;
    private Color currentColor;

    void Start()
    {
        LoadCustomization();
        UpdateVisuals();

        nextHelmetButton.onClick.AddListener(ChangeHelmet);
        nextArmorButton.onClick.AddListener(ChangeArmor);
        saveButton.onClick.AddListener(SaveCustomization);
    }

    void ChangeHelmet()
    {
        currentHelmet = (currentHelmet + 1) % helmetOptions.Length;
        UpdateVisuals();
    }

    void ChangeArmor()
    {
        currentArmor = (currentArmor + 1) % armorOptions.Length;
        UpdateVisuals();
    }

    void Update()
    {
        currentColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        suitRenderer.material.color = currentColor;
    }

    void UpdateVisuals()
    {
        for (int i = 0; i < helmetOptions.Length; i++)
            helmetOptions[i].SetActive(i == currentHelmet);

        for (int i = 0; i < armorOptions.Length; i++)
            armorOptions[i].SetActive(i == currentArmor);
    }

    void SaveCustomization()
    {
        PlayerCustomizationData data = new PlayerCustomizationData();
        data.helmetIndex = currentHelmet;
        data.armorIndex = currentArmor;
        data.suitColor = currentColor;
        data.Save();
    }

    void LoadCustomization()
    {
        PlayerCustomizationData data = PlayerCustomizationData.Load();
        currentHelmet = data.helmetIndex;
        currentArmor = data.armorIndex;
        currentColor = data.suitColor;

        redSlider.value = currentColor.r;
        greenSlider.value = currentColor.g;
        blueSlider.value = currentColor.b;
    }
}
