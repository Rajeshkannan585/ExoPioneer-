using UnityEngine;

public class AnimalAI : MonoBehaviour
{
    public float moveSpeed = 2f;        // Movement speed of the animal
    public float detectRange = 5f;      // How close the player must be for the animal to react
    public Transform player;            // Reference to the player object

    private Vector3 startPosition;      // The original position of the animal

    void Start()
    {
        // Store the animal's starting position
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the distance between the animal and the player
        float distance = Vector3.Distance(transform.position, player.position);

        // If the player is within detection range
        if (distance < detectRange)
        {
            // Move toward the player
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Return to original position when the player is far
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
        }
    }
}
