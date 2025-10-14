using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BaseData
{
    public string prefabName;
    public Vector3 position;
    public Quaternion rotation;
}

public class BaseSaveSystem : MonoBehaviour
{
    public Transform baseParent;

    public void SaveBase()
    {
        List<BaseData> dataList = new List<BaseData>();

        foreach (Transform t in baseParent)
        {
            BaseData data = new BaseData
            {
                prefabName = t.name.Replace("(Clone)", ""),
                position = t.position,
                rotation = t.rotation
            };
            dataList.Add(data);
        }

        string json = JsonUtility.ToJson(new Wrapper { list = dataList });
        PlayerPrefs.SetString("BaseData", json);
        PlayerPrefs.Save();

        Debug.Log("💾 Base saved.");
    }

    public void LoadBase()
    {
        string json = PlayerPrefs.GetString("BaseData", "");
        if (string.IsNullOrEmpty(json)) return;

        Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
        foreach (BaseData d in wrapper.list)
        {
            GameObject prefab = Resources.Load<GameObject>("Structures/" + d.prefabName);
            if (prefab)
                Instantiate(prefab, d.position, d.rotation, baseParent);
        }

        Debug.Log("✅ Base loaded.");
    }

    [System.Serializable]
    public class Wrapper { public List<BaseData> list; }
}
