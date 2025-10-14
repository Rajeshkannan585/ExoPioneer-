using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Idle, Patrol, Chase, Attack, Dead }
    public EnemyState currentState = EnemyState.Idle;

    [Header("Components")]
    public NavMeshAgent agent;
    public Animator animator;
    public Transform player;

    [Header("Stats")]
    public float health = 100f;
    public float detectionRange = 20f;
    public float attackRange = 3f;
    public float attackDamage = 10f;
    public float patrolRadius = 10f;
    public float attackCooldown = 2f;

    private float nextAttackTime = 0f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        if (player == null) player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (currentState == EnemyState.Dead) return;

        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Idle:
                IdleBehavior(distance);
                break;
            case EnemyState.Patrol:
                PatrolBehavior(distance);
                break;
            case EnemyState.Chase:
                ChaseBehavior(distance);
                break;
            case EnemyState.Attack:
                AttackBehavior(distance);
                break;
        }
    }

    void IdleBehavior(float distance)
    {
        animator.SetBool("isWalking", false);
        if (distance < detectionRange)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = EnemyState.Patrol;
        }
    }

    void PatrolBehavior(float distance)
    {
        animator.SetBool("isWalking", true);
        if (!agent.hasPath)
        {
            Vector3 randomPos = startPosition + Random.insideUnitSphere * patrolRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, patrolRadius, NavMesh.AllAreas))
                agent.SetDestination(hit.position);
        }

        if (distance < detectionRange)
        {
            currentState = EnemyState.Chase;
        }
    }

    void ChaseBehavior(float distance)
    {
        animator.SetBool("isRunning", true);
        agent.SetDestination(player.position);

        if (distance <= attackRange)
        {
            currentState = EnemyState.Attack;
        }
    }

    void AttackBehavior(float distance)
    {
        animator.SetBool("isRunning", false);
        agent.ResetPath();

        transform.LookAt(player);

        if (Time.time > nextAttackTime)
        {
            animator.SetTrigger("attack");
            player.GetComponent<PlayerSurvivalSystem>()?.TakeDamage(attackDamage);
            nextAttackTime = Time.time + attackCooldown;
        }

        if (distance > attackRange + 2f)
        {
            currentState = EnemyState.Chase;
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        currentState = EnemyState.Dead;
        agent.enabled = false;
        animator.SetTrigger("die");
        Destroy(gameObject, 5f);
    }
}
