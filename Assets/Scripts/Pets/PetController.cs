using UnityEngine;
using UnityEngine.AI;

public class PetController : MonoBehaviour
{
    public Transform player;              // Player reference
    public float followDistance = 3f;     // Distance to maintain from player
    public float followSpeed = 4f;        // Movement speed
    public bool canAttack = false;        // Toggle for combat pets
    public float attackRange = 2f;
    public float attackDamage = 10f;

    private NavMeshAgent agent;
    private Animator anim;
    private Transform targetEnemy;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (targetEnemy == null)
        {
            FollowPlayer();
        }
        else
        {
            AttackEnemy();
        }
    }

    void FollowPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Move towards player if too far
        if (distance > followDistance)
        {
            agent.SetDestination(player.position);
            anim?.SetBool("isMoving", true);
        }
        else
        {
            agent.ResetPath();
            anim?.SetBool("isMoving", false);
        }
    }

    void AttackEnemy()
    {
        if (!canAttack || targetEnemy == null) return;

        float distance = Vector3.Distance(transform.position, targetEnemy.position);
        if (distance > attackRange)
        {
            agent.SetDestination(targetEnemy.position);
            anim?.SetBool("isAttacking", false);
        }
        else
        {
            agent.ResetPath();
            anim?.SetBool("isAttacking", true);
            // Example: Deal damage
            Debug.Log("Pet attacks enemy for " + attackDamage + " damage!");
        }
    }

    public void SetTarget(Transform enemy)
    {
        targetEnemy = enemy;
    }
}
