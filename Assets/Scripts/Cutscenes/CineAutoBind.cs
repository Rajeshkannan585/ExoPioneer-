using UnityEngine;
using Cinemachine;

public class CineAutoBind : MonoBehaviour
{
    public CinemachineVirtualCamera[] virtualCams;
    public Transform player;

    void Awake()
    {
        foreach (var cam in virtualCams)
        {
            var transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
            if (transposer != null && player != null)
                cam.Follow = player;

            if (cam.LookAt == null && player != null)
                cam.LookAt = player;
        }
    }
}
