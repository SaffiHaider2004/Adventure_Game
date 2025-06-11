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
                break;
            case ZombieType.Male:
                maxHealth = 3;
                break;
            case ZombieType.Monster:
                maxHealth = 4;
                break;
        }
        currentHealth = maxHealth;
        UpdateHealthUI();
        ShowHealthBar(true);
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
        { 
            return; 
        }

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

        // Disable AI movement and stop navigation
        ZombieAI ai = GetComponent<ZombieAI>();
        if (ai != null)
            ai.enabled = false;

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.ResetPath();
            agent.isStopped = true; // ✅ freezes movement safely
        }

        animator.SetTrigger("die");

        Destroy(gameObject, 3f); // gives time for death animation
    }
}
