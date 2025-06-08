using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ZombieHealth : MonoBehaviour
{
    public enum ZombieType { Female, Male, Monster }
    public ZombieType type;

    private int maxHealth;
    private int currentHealth;

    public GameObject healthBarCanvas;
    public Slider healthSlider;

    void Start()
    {
        switch (type)
        {
            case ZombieType.Female:
                maxHealth = 1;
                break;
            case ZombieType.Male:
                maxHealth = 2;
                break;
            case ZombieType.Monster:
                maxHealth = 3;
                break;
        }
        currentHealth = maxHealth;
        UpdateHealthUI();
        ShowHealthBar(false);
    }

    public void TakeDamage(int amount)
    {
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
        Destroy(gameObject); // or play death animation
    }
}
