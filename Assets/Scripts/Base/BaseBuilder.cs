using UnityEngine;

public class BaseBuilder : MonoBehaviour
{
    [Header("Building Prefabs")]
    public GameObject[] buildableStructures; // e.g. Dome, EnergyHub, RadarTower
    public Transform buildParent;

    [Header("Build Settings")]
    public float placementRange = 15f;
    public LayerMask buildLayer;

    private GameObject previewObject;
    private int currentIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            CycleStructure();

        if (Input.GetMouseButtonDown(0))
            PlaceStructure();

        UpdatePreview();
    }

    void CycleStructure()
    {
        currentIndex = (currentIndex + 1) % buildableStructures.Length;
        ShowPreview();
    }

    void ShowPreview()
    {
        if (previewObject != null)
            Destroy(previewObject);

        previewObject = Instantiate(buildableStructures[currentIndex]);
        previewObject.GetComponent<Collider>().enabled = false;
        previewObject.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0, 0.3f);
    }

    void UpdatePreview()
    {
        if (previewObject == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, placementRange, buildLayer))
        {
            previewObject.transform.position = hit.point;
        }
    }

    void PlaceStructure()
    {
        if (previewObject == null) return;

        Vector3 buildPos = previewObject.transform.position;
        Instantiate(buildableStructures[currentIndex], buildPos, Quaternion.identity, buildParent);
        Debug.Log($"🏗️ Structure built: {buildableStructures[currentIndex].name}");

        Destroy(previewObject);
        previewObject = null;
    }
}
