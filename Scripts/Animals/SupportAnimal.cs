// ===============================
// SupportAnimal.cs
// Basic pet AI that follows player and provides support (heal or attack)
// ===============================

using UnityEngine;

public class SupportAnimal : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform player;          // assign player in Inspector
    public float followDistance = 3f; // how far behind player to follow
    public float moveSpeed = 3f;

    [Header("Support Abilities")]
    public bool canHeal = true;
    public bool canAttackAssist = false;
    public float healAmount = 5f;
    public float healCooldown = 5f;
    private float healTimer;

    private void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    private void Update()
    {
        if (player == null) return;

        // --- Follow Player ---
        Vector3 targetPos = player.position - player.forward * followDistance;
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        // Face player
        transform.LookAt(player);

        // --- Heal Support ---
        if (canHeal)
        {
            healTimer += Time.deltaTime;
            if (healTimer >= healCooldown)
            {
                HealPlayer();
                healTimer = 0f;
            }
        }

        // --- Attack Assist (future use) ---
        if (canAttackAssist)
        {
            // TODO: Implement attack support (shoot, bite, etc.)
        }
    }

    void HealPlayer()
    {
        Debug.Log($"{name} healed player for {healAmount} HP!");
        // TODO: Call PlayerHealth.AddHealth(healAmount);
    }
}
