using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealthBar : MonoBehaviour 
{
    // [SerializeField] private Image healthBar;
    [SerializeField] private Slider healthSlider;

    [SerializeField] private TMP_Text healthValue;



    public void Start()
    {
        Debug.Log("UIHealthBar Start");
        PlayerStatsEvents.OnPlayerStatsChanged += PlayerStatsChanged;
    
    }
    
    void PlayerStatsChanged(Player player)
    {
        float currentHealth = player.Health.CurrentHealth.value;
        float maxHealth = player.Health.MaxHealth.value;
        Debug.Log($"Current Health: {currentHealth} Max Health: {maxHealth}");

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        healthValue.text = $"{currentHealth} / {maxHealth}";

        Debug.Log($"Current Slider: {healthSlider.value}");
    }

}