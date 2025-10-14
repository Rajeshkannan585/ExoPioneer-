using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlSettingsUI : MonoBehaviour
{
    public Slider lookSensitivitySlider;
    public Slider moveSensitivitySlider;
    public Toggle invertYToggle;
    public TMP_Dropdown jumpKeyDropdown;
    public TMP_Dropdown fireKeyDropdown;
    public TMP_Dropdown interactKeyDropdown;
    public Button saveButton;
    public Button resetButton;

    private ControlSettingsData settings = new ControlSettingsData();

    void Start()
    {
        settings.Load();
        UpdateUI();

        saveButton.onClick.AddListener(SaveSettings);
        resetButton.onClick.AddListener(ResetDefaults);
    }

    void UpdateUI()
    {
        lookSensitivitySlider.value = settings.lookSensitivity;
        moveSensitivitySlider.value = settings.moveSensitivity;
        invertYToggle.isOn = settings.invertYAxis;

        jumpKeyDropdown.value = jumpKeyDropdown.options.FindIndex(x => x.text == settings.jumpKey);
        fireKeyDropdown.value = fireKeyDropdown.options.FindIndex(x => x.text == settings.fireKey);
        interactKeyDropdown.value = interactKeyDropdown.options.FindIndex(x => x.text == settings.interactKey);
    }

    void SaveSettings()
    {
        settings.lookSensitivity = lookSensitivitySlider.value;
        settings.moveSensitivity = moveSensitivitySlider.value;
        settings.invertYAxis = invertYToggle.isOn;
        settings.jumpKey = jumpKeyDropdown.options[jumpKeyDropdown.value].text;
        settings.fireKey = fireKeyDropdown.options[fireKeyDropdown.value].text;
        settings.interactKey = interactKeyDropdown.options[interactKeyDropdown.value].text;
        settings.Save();
    }

    void ResetDefaults()
    {
        settings = new ControlSettingsData();
        UpdateUI();
    }
}
