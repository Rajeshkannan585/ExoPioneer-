using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public TMP_Text masterValue;
    public TMP_Text musicValue;
    public TMP_Text sfxValue;

    void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVol", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVol", 1f);

        UpdateLabels();

        masterSlider.onValueChanged.AddListener((v) => ChangeVolume("MasterVol", v));
        musicSlider.onValueChanged.AddListener((v) => ChangeVolume("MusicVol", v));
        sfxSlider.onValueChanged.AddListener((v) => ChangeVolume("SFXVol", v));
    }

    void ChangeVolume(string param, float val)
    {
        AudioManager.Instance.SetVolume(param, val);
        UpdateLabels();
    }

    void UpdateLabels()
    {
        masterValue.text = $"{Mathf.RoundToInt(masterSlider.value * 100)}%";
        musicValue.text = $"{Mathf.RoundToInt(musicSlider.value * 100)}%";
        sfxValue.text = $"{Mathf.RoundToInt(sfxSlider.value * 100)}%";
    }
}
