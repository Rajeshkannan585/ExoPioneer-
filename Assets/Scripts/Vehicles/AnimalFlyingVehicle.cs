using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AnimalFlyingVehicle : MonoBehaviour
{
    [Header("Flight Settings")]
    public float flightSpeed = 50f;
    public float rotationSpeed = 70f;
    public float ascendSpeed = 25f;
    public float descendSpeed = 20f;
    public float boostMultiplier = 2f;
    public float fuel = 100f;
    public float fuelUseRate = 3f;

    [Header("State")]
    public bool isPlayerMounted = false;
    public Transform seatPoint;
    public Transform exitPoint;
    public ParticleSystem boostEffect;
    public AudioSource wingFlapSound;
    public AudioSource boostSound;

    private Rigidbody rb;
    private bool isBoosting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        if (isPlayerMounted)
        {
            HandleFlightInput();
        }
    }

    void HandleFlightInput()
    {
        float moveForward = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        Vector3 moveDir = transform.forward * moveForward * flightSpeed;
        rb.AddForce(moveDir * Time.deltaTime, ForceMode.Acceleration);

        transform.Rotate(Vector3.up * turn * rotationSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space))
            rb.AddForce(Vector3.up * ascendSpeed, ForceMode.Acceleration);
        if (Input.GetKey(KeyCode.LeftControl))
            rb.AddForce(Vector3.down * descendSpeed, ForceMode.Acceleration);

        // Boost mode
        if (Input.GetKeyDown(KeyCode.LeftShift) && fuel > 0)
            StartBoost();
        if (Input.GetKeyUp(KeyCode.LeftShift))
            StopBoost();

        if (isBoosting)
        {
            fuel -= fuelUseRate * Time.deltaTime;
            if (fuel <= 0)
                StopBoost();
        }

        if (Input.GetKeyDown(KeyCode.F))
            ExitMount();
    }

    void StartBoost()
    {
        if (isBoosting) return;
        isBoosting = true;
        flightSpeed *= boostMultiplier;
        if (boostEffect != null) boostEffect.Play();
        if (boostSound != null) boostSound.Play();
        Debug.Log("🚀 Boost activated!");
    }

    void StopBoost()
    {
        if (!isBoosting) return;
        isBoosting = false;
        flightSpeed /= boostMultiplier;
        if (boostEffect != null) boostEffect.Stop();
        Debug.Log("🛑 Boost stopped");
    }

    public void MountPlayer(GameObject player)
    {
        isPlayerMounted = true;
        player.SetActive(false);
        if (wingFlapSound) wingFlapSound.Play();
        Debug.Log("🐉 Player mounted flying beast!");
    }

    void ExitMount()
    {
        isPlayerMounted = false;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && exitPoint != null)
        {
            player.transform.position = exitPoint.position;
            player.SetActive(true);
        }
        if (wingFlapSound) wingFlapSound.Stop();
        StopBoost();
        Debug.Log("🚶 Player dismounted flying beast");
    }
}
