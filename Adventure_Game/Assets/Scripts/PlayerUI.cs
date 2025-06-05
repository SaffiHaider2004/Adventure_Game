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

    void Start()
    {
        if (playerStats == null)
            playerStats = GetComponent<PlayerStats>();

        healthSlider.maxValue = playerStats.GetMaxHealth();
        staminaSlider.maxValue = playerStats.GetMaxStamina();
    }

    void Update()
    {
        healthSlider.value = playerStats.GetHealth();
        staminaSlider.value = playerStats.GetStamina();
    }
}

