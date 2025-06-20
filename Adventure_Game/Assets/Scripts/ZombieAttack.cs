using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float damageAmount = 10f;
    public float attackCooldown = 2f;
    private GameObject player;
    private float lastAttackTime;

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null) return; // skip until found
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            PlayerStats stats = player.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.TakeDamage(damageAmount);
                lastAttackTime = Time.time;
            }
        }
    }
}
