using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public void Awake()
    {
        _weaponManager = GetComponent<WeaponManager>();
    }

    public void Update()
    {
        DisplayScore();
        DisplayAmmo();
    }

    public void DisplayScore()
    {
        if (!IsIdleTime())
        {
            _scoreTime += TimeUtils.DeltaTime;
            if (!(_scoreTime >= 1)) return;
            _scoreTime = 0;
            Score += Level;
        }
        _scoreLabel.text = Score.ToString();
        _levelLabel.text = "Level " + Level;
    }

    public void DisplayAmmo()
    {
        var currentAmmo =_weaponManager.GetCurrentWeaponAmmo();
        _ammoLabel.text = "Ammo: " + currentAmmo;
        _ammoLabel.color = currentAmmo == 0 ? Color.red : Color.white;
    }

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

    private bool IsIdleTime()
    {
        IdleTime += TimeUtils.DeltaTime;
        return IdleTime > 5f;
    }

    public int Level = 1;
    public int Score { get; set; }
    public float IdleTime { get; set; }

    private float _scoreTime = 0; 
    [SerializeField]
    private Image _healthStats, _staminaStats;
    [SerializeField]
    private Text _scoreLabel, _levelLabel, _ammoLabel;
    private WeaponManager _weaponManager;
}
