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
