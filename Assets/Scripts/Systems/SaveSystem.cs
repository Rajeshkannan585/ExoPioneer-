using UnityEngine;

public static class SaveSystem
{
    // Save player data
    public static void SavePlayer(Vector3 position, float health)
    {
        PlayerPrefs.SetFloat("PlayerX", position.x);
        PlayerPrefs.SetFloat("PlayerY", position.y);
        PlayerPrefs.SetFloat("PlayerZ", position.z);
        PlayerPrefs.SetFloat("PlayerHealth", health);
        PlayerPrefs.Save();

        Debug.Log("Game saved!");
    }

    // Load player position
    public static Vector3 LoadPlayerPosition()
    {
        float x = PlayerPrefs.GetFloat("PlayerX", 0);
        float y = PlayerPrefs.GetFloat("PlayerY", 1);
        float z = PlayerPrefs.GetFloat("PlayerZ", 0);
        return new Vector3(x, y, z);
    }

    // Load player health
    public static float LoadPlayerHealth(float defaultHealth)
    {
        return PlayerPrefs.GetFloat("PlayerHealth", defaultHealth);
    }

    // Clear save data
    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Save data cleared!");
    }
}
