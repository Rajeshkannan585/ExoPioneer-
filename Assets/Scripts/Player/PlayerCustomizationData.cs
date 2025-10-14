using UnityEngine;

[System.Serializable]
public class PlayerCustomizationData
{
    public int helmetIndex;
    public int armorIndex;
    public Color suitColor;

    public static PlayerCustomizationData Load()
    {
        PlayerCustomizationData data = new PlayerCustomizationData();
        data.helmetIndex = PlayerPrefs.GetInt("HelmetIndex", 0);
        data.armorIndex = PlayerPrefs.GetInt("ArmorIndex", 0);
        float r = PlayerPrefs.GetFloat("SuitR", 0.3f);
        float g = PlayerPrefs.GetFloat("SuitG", 0.6f);
        float b = PlayerPrefs.GetFloat("SuitB", 1f);
        data.suitColor = new Color(r, g, b);
        return data;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("HelmetIndex", helmetIndex);
        PlayerPrefs.SetInt("ArmorIndex", armorIndex);
        PlayerPrefs.SetFloat("SuitR", suitColor.r);
        PlayerPrefs.SetFloat("SuitG", suitColor.g);
        PlayerPrefs.SetFloat("SuitB", suitColor.b);
        PlayerPrefs.Save();
        Debug.Log("🧑‍🚀 Player customization saved!");
    }
}
