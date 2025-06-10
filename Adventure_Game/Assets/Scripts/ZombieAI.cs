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
    [SerializeField] private float damageAmount = 10f;

    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime;
    private float nextPatrolTime;
    private Vector3 patrolOrigin;

    private enum ZombieState { Patrolling, Chasing, Attacking }
    private ZombieState currentState = ZombieState.Patrolling;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        patrolOrigin = transform.position;

        if (player == null && GameObject.FindWithTag("Player") != null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }
    public void SetPlayerReference(Transform target)
    {
        player = target;
    }
    void Update()
    {
        // Find player if it's not set
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                return; // Skip until player is found
            }
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case ZombieState.Patrolling:
                Patrol();
                animator.SetBool("isWalking", true);

                if (distanceToPlayer <= detectionRange)
                    currentState = ZombieState.Chasing;
                break;

            case ZombieState.Chasing:
                agent.SetDestination(player.position);
                animator.SetBool("isWalking", true);

                if (distanceToPlayer <= attackRange)
                    currentState = ZombieState.Attacking;
                else if (distanceToPlayer > detectionRange)
                    currentState = ZombieState.Patrolling;
                break;

            case ZombieState.Attacking:
                agent.ResetPath();
                animator.SetBool("isWalking", false);

                transform.LookAt(player.position);
                AttackPlayer();

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
                animator.SetBool("isWalking", true);
            }

            nextPatrolTime = Time.time + patrolInterval;
        }
    }

    private void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger("attack");

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange)
            {
                PlayerStats stats = player.GetComponent<PlayerStats>();
                if (stats != null)
                {
                    stats.TakeDamage(damageAmount);
                }
            }
        }
    }
}
