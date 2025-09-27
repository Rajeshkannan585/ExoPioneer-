using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float damage = 10f;
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    private Transform player;
    private NavMeshAgent agent;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange)
        {
            agent.SetDestination(player.position);

            if (distance <= attackRange && Time.time > lastAttackTime + attackCooldown)
            {
                AttackPlayer();
            }
        }
    }

    void AttackPlayer()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.TakeDamage(damage);
        }

        Debug.Log("Enemy attacked player! -" + damage + " HP");
        lastAttackTime = Time.time;
    }
}
