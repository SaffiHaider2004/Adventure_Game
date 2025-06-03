using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Zombie Settings")]
    [SerializeField] private float patrolRadius = 10f;
    [SerializeField] private float patrolInterval = 5f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1.5f;

    private NavMeshAgent agent;
    private float lastAttackTime;
    private float nextPatrolTime;
    private Vector3 patrolOrigin;

    private enum ZombieState { Patrolling, Chasing, Attacking }
    private ZombieState currentState = ZombieState.Patrolling;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolOrigin = transform.position;

        if (player == null && GameObject.FindWithTag("Player") != null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case ZombieState.Patrolling:
                Patrol();

                if (distanceToPlayer <= detectionRange)
                    currentState = ZombieState.Chasing;
                break;

            case ZombieState.Chasing:
                agent.SetDestination(player.position);

                if (distanceToPlayer <= attackRange)
                    currentState = ZombieState.Attacking;
                else if (distanceToPlayer > detectionRange)
                    currentState = ZombieState.Patrolling;
                break;

            case ZombieState.Attacking:
                agent.ResetPath();

                transform.LookAt(player.position);

                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    lastAttackTime = Time.time;
                    AttackPlayer(); // Implement this for your game logic
                }

                if (distanceToPlayer > attackRange)
                    currentState = ZombieState.Chasing;
                break;
        }
    }

    private void Patrol()
    {
        if (Time.time >= nextPatrolTime)
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
            randomDirection += patrolOrigin;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }

            nextPatrolTime = Time.time + patrolInterval;
        }
    }

    private void AttackPlayer()
    {
        Debug.Log($"{gameObject.name} attacks the player!");

        // You can trigger an animation or apply damage here
        // e.g., player.GetComponent<PlayerHealth>().TakeDamage(10);
    }
}
