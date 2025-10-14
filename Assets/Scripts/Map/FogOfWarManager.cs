using UnityEngine;
using UnityEngine.UI;

public class FogOfWarManager : MonoBehaviour
{
    public RawImage fogImage;              // Fog texture on map
    public Transform player;
    public float revealRadius = 25f;
    public float mapScale = 0.1f;
    public Vector2 mapOffset;

    private Texture2D fogTexture;
    private Color32[] pixels;
    private int width = 1024;
    private int height = 1024;

    void Start()
    {
        fogTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        pixels = new Color32[width * height];

        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = new Color32(0, 0, 0, 255);

        fogTexture.SetPixels32(pixels);
        fogTexture.Apply();
        fogImage.texture = fogTexture;
    }

    void Update()
    {
        RevealAtPlayer();
    }

    void RevealAtPlayer()
    {
        Vector2 mapPos = (new Vector2(player.position.x, player.position.z) * mapScale) + mapOffset;
        int centerX = (int)(mapPos.x + width / 2);
        int centerY = (int)(mapPos.y + height / 2);

        int radius = (int)revealRadius;

        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                int px = centerX + x;
                int py = centerY + y;

                if (px >= 0 && px < width && py >= 0 && py < height)
                {
                    float dist = Mathf.Sqrt(x * x + y * y);
                    if (dist < radius)
                    {
                        int index = py * width + px;
                        pixels[index] = new Color32(0, 0, 0, (byte)Mathf.Lerp(0, 255, dist / radius));
                    }
                }
            }
        }

        fogTexture.SetPixels32(pixels);
        fogTexture.Apply();
    }
}
