using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance;

    void Awake()
    {
        MakeInstance();
        _flare = Instantiate(_flarePrefab, LocationUtils.StartedFlarePoint, Quaternion.identity);
        _potions = new List<GameObject>{ Instantiate(_potionPrefab, LocationUtils.StartedPotionPoint, Quaternion.identity) };
        _ammo = Instantiate(_ammoPrefab, LocationUtils.StartedAmmoPoint, Quaternion.identity);
        _terrain = Terrain.activeTerrain;
    }

    void MakeInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        player = GameObject.FindWithTag(TagsExtensions.PLAYER_TAG);
    }

    public void Update()
    {
        if (Vector3.Distance(player.transform.position, _flare.transform.position) <= _toItemDistance)
        {
            LevelUp();
            CreateNewPotion();
            CreateNewAmmo();
        }

        foreach (var potion in _potions)
        {
            if (Vector3.Distance(player.transform.position, potion.transform.position) <= _toItemDistance)
            {
                IncreaseHealth(potion);
            }
        }
        if (_ammo.activeSelf && Vector3.Distance(player.transform.position, _ammo.transform.position) <= _toItemDistance)
        {
            _ammo.SetActive(false);
            player.GetComponent<WeaponManager>().IncreaseAmmoByItem();
        }
    }

    public void LevelUp()
    {
        SetNewCheckpoint();
        EnemyManager.instance.LevelUp(_flare.transform.position);
        var enemies = GameObject.FindGameObjectsWithTag(TagsExtensions.ENEMY_TAG);
        for (var i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().IncreasePower();
        }
        player.GetComponent<PlayerStats>().Level++;
        player.GetComponent<HealthScript>().ApplyHealth(false);
        player.GetComponent<WeaponManager>().IncreaseAmmoByLevel();
    }

    private void IncreaseHealth(GameObject potion)
    {
        player.GetComponent<HealthScript>().ApplyHealth(true);
        potion.transform.position = RandomPoint();
    }

    private void SetNewCheckpoint()
    {
        Destroy(_flare);

        _flare = Instantiate(_flarePrefab, RandomPoint(), Quaternion.identity);
    }

    private void CreateNewPotion()
    {
        _potions.Add(Instantiate(_potionPrefab, RandomPoint(), Quaternion.identity));
    }

    private void CreateNewAmmo()
    {
        if (_ammo.activeSelf) return;
        _ammo = Instantiate(_ammoPrefab, RandomPoint(), Quaternion.identity);
        _ammo.SetActive(true);
    }

    private Vector3 RandomPoint()
    {
        var flareLocation = LocationUtils.FlareLocation;
        var x = RandomUtils.GetRandomNumber(flareLocation["X_MIN"], flareLocation["X_MAX"]);
        var z = RandomUtils.GetRandomNumber(flareLocation["Z_MIN"], flareLocation["Z_MAX"]);
        var y = _terrain.SampleHeight(new Vector3(x, 0f, z));
        return new Vector3(x, y, z);
    }

    [SerializeField] private GameObject _flarePrefab, _potionPrefab, _ammoPrefab;

    private GameObject _flare;
    private GameObject player;
    private GameObject _ammo;
    private Terrain _terrain;
    private List<GameObject> _potions;
    private const float _toItemDistance = 5f;
}
