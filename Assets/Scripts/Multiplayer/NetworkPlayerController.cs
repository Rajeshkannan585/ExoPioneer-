using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(CharacterController))]
public class NetworkPlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 120f;
    private CharacterController controller;

    public SkinnedMeshRenderer playerBody;
    private PlayerCustomizationData myCustomData;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCustomData = PlayerCustomizationData.Load();
        ApplyCustomization(myCustomData);
    }

    void Update()
    {
        if (!IsOwner) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * v + transform.right * h;
        controller.SimpleMove(move * moveSpeed);

        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    public void ApplyCustomization(PlayerCustomizationData data)
    {
        playerBody.material.color = data.suitColor;
    }
}
