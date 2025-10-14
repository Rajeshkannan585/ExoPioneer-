using UnityEngine;
using UnityEngine.UI;

public class QuestPathTracker : MonoBehaviour
{
    public Transform player;
    public Transform questTarget;
    public RectTransform arrowIcon;
    public float rotateSpeed = 4f;

    void Update()
    {
        if (player == null || questTarget == null) 
        {
            arrowIcon.gameObject.SetActive(false);
            return;
        }

        arrowIcon.gameObject.SetActive(true);

        Vector3 dir = questTarget.position - player.position;
        dir.y = 0;

        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.Euler(0, 0, -angle);
        arrowIcon.localRotation = Quaternion.Slerp(arrowIcon.localRotation, targetRot, Time.deltaTime * rotateSpeed);
    }
}
