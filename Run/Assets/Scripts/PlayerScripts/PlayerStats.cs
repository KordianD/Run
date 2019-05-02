using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Image _healthStats, _staminaStats;
    
    public void DisplayHealthStats(float healthValue)
    {
        healthValue /= 100f;

        _healthStats.fillAmount = healthValue;
    }

    public void DisplaStaminaStats(float staminaValue)
    {
        staminaValue /= 100f;

        _staminaStats.fillAmount = staminaValue;
    }
}
