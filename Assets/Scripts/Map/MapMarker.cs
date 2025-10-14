using UnityEngine;
using UnityEngine.UI;

public class MapMarker : MonoBehaviour
{
    public Transform target;
    public RectTransform icon;
    public float mapScale = 0.1f;
    public Vector2 offset;

    void Update()
    {
        if (target == null) return;
        Vector2 pos = new Vector2(target.position.x, target.position.z);
        icon.anchoredPosition = (pos * mapScale) + offset;
    }
}
