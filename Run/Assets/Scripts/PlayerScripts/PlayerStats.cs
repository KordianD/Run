using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public void Update()
    {
        DisplayScore();
    }

    public void DisplayScore()
    {
        _scoreTime += TimeUtils.DeltaTime;
        if (!(_scoreTime >= 1)) return;
        _scoreTime = 0;
        Score += Level;
        _scoreLabel.text = Score.ToString();
        _levelLabel.text = "Level " + Level;
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

    public int Level = 1;
    public int Score { get; set; }
    private float _scoreTime = 0;

    [SerializeField]
    private Image _healthStats, _staminaStats;
    [SerializeField]
    private Text _scoreLabel, _levelLabel;
}
