using UnityEngine;
using System.Collections;

public class PetCameraFocus : MonoBehaviour
{
    public Camera mainCamera;          // Main camera reference
    public Transform petTarget;        // Pet position
    public float zoomFOV = 35f;        // Zoom level
    public float normalFOV = 60f;      // Normal camera field of view
    public float zoomSpeed = 1f;       // Speed of zoom transition
    public PetBondSystem petBond;      // Reference to bond system
    private bool isCutscenePlaying = false;

    void Update()
    {
        // Trigger cutscene when max bond reached
        if (!isCutscenePlaying && petBond.bondXP >= petBond.bondXPToNext * 0.95f)
        {
            StartCoroutine(BondCutscene());
        }
    }

    IEnumerator BondCutscene()
    {
        isCutscenePlaying = true;
        Debug.Log("🎥 Pet Bond Cutscene started!");
        float time = 0f;

        // Smoothly zoom in
        while (mainCamera.fieldOfView > zoomFOV)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, zoomFOV, time);
            time += Time.deltaTime * zoomSpeed;
            mainCamera.transform.LookAt(petTarget);
            yield return null;
        }

        // Small wait during emotional moment
        yield return new WaitForSeconds(3f);

        // Zoom back
        time = 0f;
        while (mainCamera.fieldOfView < normalFOV)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, normalFOV, time);
            time += Time.deltaTime * zoomSpeed;
            yield return null;
        }

        Debug.Log("💞 Pet Bond Cutscene ended!");
        isCutscenePlaying = false;
    }
}
