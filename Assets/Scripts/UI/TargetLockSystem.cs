using UnityEngine;
using UnityEngine.UI;

public class TargetLockSystem : MonoBehaviour
{
    public Camera mainCamera;
    public Image lockIcon;              // The lock-on reticle UI
    public float lockRange = 300f;
    public string targetTag = "Enemy";  // You can also set to "Boss"

    private Transform currentTarget;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleLockTarget();
        }

        if (currentTarget != null)
        {
            if (Vector3.Distance(mainCamera.transform.position, currentTarget.position) > lockRange)
            {
                UnlockTarget();
                return;
            }

            Vector3 screenPos = mainCamera.WorldToScreenPoint(currentTarget.position);
            if (screenPos.z > 0)
            {
                lockIcon.enabled = true;
                lockIcon.rectTransform.position = screenPos;
            }
            else
            {
                lockIcon.enabled = false;
            }
        }
    }

    void ToggleLockTarget()
    {
        if (currentTarget != null)
        {
            UnlockTarget();
            return;
        }

        SkyBossController boss = FindObjectOfType<SkyBossController>();
        if (boss != null && Vector3.Distance(mainCamera.transform.position, boss.transform.position) < lockRange)
        {
            currentTarget = boss.transform;
            Debug.Log("🎯 Locked onto boss!");
            return;
        }

        SkyEnemyAI[] enemies = FindObjectsOfType<SkyEnemyAI>();
        if (enemies.Length > 0)
        {
            currentTarget = enemies[0].transform;
            Debug.Log("🎯 Locked onto enemy: " + enemies[0].name);
        }
    }

    void UnlockTarget()
    {
        currentTarget = null;
        lockIcon.enabled = false;
        Debug.Log("🔓 Target unlocked");
    }
}
