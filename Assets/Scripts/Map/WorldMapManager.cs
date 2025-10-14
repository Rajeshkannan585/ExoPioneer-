using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldMapManager : MonoBehaviour
{
    [Header("Map References")]
    public RawImage mapImage;                // Top-down map texture
    public RectTransform playerIcon;
    public RectTransform questIcon;
    public RectTransform bossIcon;

    [Header("World Settings")]
    public Transform player;
    public Transform questTarget;
    public Transform bossTarget;

    public float mapScale = 0.1f;            // Scale world positions to map
    public Vector2 mapOffset;                // Offset adjustment
    public bool rotateWithPlayer = true;

    [Header("UI Panels")]
    public CanvasGroup fullMapPanel;
    public CanvasGroup miniMapPanel;
    public TextMeshProUGUI locationText;

    private bool mapOpen = false;

    void Start()
    {
        ShowMiniMap();
    }

    void Update()
    {
        UpdateMiniMap();

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mapOpen) ShowMiniMap();
            else ShowFullMap();
        }
    }

    void UpdateMiniMap()
    {
        if (player == null) return;

        Vector2 playerPos = new Vector2(player.position.x, player.position.z);
        playerIcon.anchoredPosition = (playerPos * mapScale) + mapOffset;

        if (rotateWithPlayer)
            playerIcon.localRotation = Quaternion.Euler(0, 0, -player.eulerAngles.y);

        if (questTarget != null)
        {
            Vector2 questPos = new Vector2(questTarget.position.x, questTarget.position.z);
            questIcon.anchoredPosition = (questPos * mapScale) + mapOffset;
        }

        if (bossTarget != null)
        {
            Vector2 bossPos = new Vector2(bossTarget.position.x, bossTarget.position.z);
            bossIcon.anchoredPosition = (bossPos * mapScale) + mapOffset;
        }

        // Update location text (approx zone)
        locationText.text = GetZoneName(player.position);
    }

    string GetZoneName(Vector3 pos)
    {
        if (pos.y < 0)
            return "⛰️ Underground Caves";
        if (pos.z > 500)
            return "🌋 Volcanic Ridge";
        if (pos.x < -400)
            return "🌲 Emerald Forest";
        return "🌌 Unknown Terrain";
    }

    void ShowMiniMap()
    {
        mapOpen = false;
        fullMapPanel.alpha = 0;
        fullMapPanel.blocksRaycasts = false;
        miniMapPanel.alpha = 1;
        miniMapPanel.blocksRaycasts = true;
    }

    void ShowFullMap()
    {
        mapOpen = true;
        fullMapPanel.alpha = 1;
        fullMapPanel.blocksRaycasts = true;
        miniMapPanel.alpha = 0;
        miniMapPanel.blocksRaycasts = false;
    }
}
