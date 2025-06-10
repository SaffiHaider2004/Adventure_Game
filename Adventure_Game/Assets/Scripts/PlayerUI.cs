using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public PlayerStats playerStats;

    [Header("UI Sliders")]
    public Slider healthSlider;
    public Slider staminaSlider;

    public void InitializeSliders()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats not assigned to PlayerUI.");
            return;
        }

        healthSlider.maxValue = playerStats.GetMaxHealth();
        staminaSlider.maxValue = playerStats.GetMaxStamina();
    }

    void Update()
    {
        if (playerStats != null)
        {
            healthSlider.value = playerStats.GetHealth();
            staminaSlider.value = playerStats.GetStamina();
        }
    }
}


