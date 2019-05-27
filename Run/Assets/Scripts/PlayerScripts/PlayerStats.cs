using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

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

    public void SaveStatisticsFromGameToFile()
    {
        string destination =  "save.txt";
        if (!File.Exists(destination))
        {
            using (StreamWriter sw = File.CreateText(destination))
            {
                sw.WriteLine(Score);
            }
        }
        else
        {
            using (StreamWriter sw = File.AppendText(destination))
            {
                sw.WriteLine(Score.ToString());
            }
        }
    }

    public IEnumerable<int> ReadTopStatisticsFromFile()
    {
        List<int> results = new List<int>();
        string destination = Application.persistentDataPath + "/save.txt";
        using (StreamReader sr = File.OpenText(destination))
        {
            string s = "";
            while ((s = sr.ReadLine()) != null)
            {
                results.Add(Int32.Parse(s));
            }
        }

        results.OrderByDescending(i => i);
        return results.Take(10);
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
