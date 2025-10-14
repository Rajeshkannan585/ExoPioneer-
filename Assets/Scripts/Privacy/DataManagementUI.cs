using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataManagementUI : MonoBehaviour
{
    public Button deleteDataButton;
    public Button exportDataButton;
    public TMP_Text statusText;

    void Start()
    {
        deleteDataButton.onClick.AddListener(DeleteAllData);
        exportDataButton.onClick.AddListener(ExportData);
    }

    void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
        EncryptedSaveSystem.DeleteData();
        statusText.text = "🧹 All data cleared successfully!";
        Debug.Log("All user data cleared on request.");
    }

    void ExportData()
    {
        string json = EncryptedSaveSystem.LoadData();
        if (!string.IsNullOrEmpty(json))
        {
            string path = Application.persistentDataPath + "/exported_data.json";
            System.IO.File.WriteAllText(path, json);
            statusText.text = $"📤 Data exported to: {path}";
        }
        else
        {
            statusText.text = "⚠️ No saved data found!";
        }
    }
}
