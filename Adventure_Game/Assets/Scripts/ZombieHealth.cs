using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour
{
    public enum ZombieType { Female, Male, Monster }
    public ZombieType type;

    private int maxHealth;
    private int currentHealth;

    public GameObject healthBarCanvas;
    public Slider healthSlider;

    private Animator animator;
    private bool isDead = false;
    public GameObject coinPrefab;
    private int coinValue = 1;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (healthBarCanvas != null)
        {
            Canvas canvas = healthBarCanvas.GetComponent<Canvas>();
            if (canvas != null && canvas.renderMode == RenderMode.WorldSpace)
            {
                canvas.worldCamera = Camera.main;
            }
        }

        switch (type)
        {
            case ZombieType.Female:
                maxHealth = 2;
                coinValue = 2;
                break;
            case ZombieType.Male:
                maxHealth = 3;
                coinValue = 5;
                break;
            case ZombieType.Monster:
                maxHealth = 4;
                coinValue = 10;
                break;
        }

        currentHealth = maxHealth;
        UpdateHealthUI();
        ShowHealthBar(true);
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;
        UpdateHealthUI();
        ShowHealthBar(true);

        if (currentHealth <= 0)
            Die();
    }

    public void ShowHealthBar(bool visible)
    {
        if (healthBarCanvas != null)
            healthBarCanvas.SetActive(visible);
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
            healthSlider.value = (float)currentHealth / maxHealth;
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        // Notify MissionManager
        MissionManager.instance?.OnZombieKilled();

        // Disable AI movement
        ZombieAI ai = GetComponent<ZombieAI>();
        if (ai != null)
            ai.enabled = false;

        // Stop navigation
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.ResetPath();
            agent.isStopped = true;
        }

        // Drop coins
        if (coinPrefab != null)
        {
            GameObject coin = Instantiate(coinPrefab, transform.position + Vector3.up * 1f, Quaternion.identity);
            CoinPickup pickup = coin.GetComponent<CoinPickup>();
            if (pickup != null)
                pickup.coinAmount = coinValue;
        }

        // Play death animation
        animator.SetTrigger("die");

        // Destroy after animation delay
        Destroy(gameObject, 3f);
    }
}
