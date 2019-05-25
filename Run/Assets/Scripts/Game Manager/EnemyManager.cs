using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField]
    private GameObject _boarPrefab, _canibalPrefab;

    [SerializeField]
    private int _cannibalEnemyCount, _boarEnemyCount;
    private int _initialCannibalCount, _initialBoarCount;
    private Vector3 _flarePosition = LocationUtils.StartedFlarePoint;
    public float waitBeforeEnemiesTime = 1f;


    // Start is called before the first frame update
    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if(instance == null)
        { 
            instance = this;
        }
    }

    void Start()
    {
        _initialCannibalCount = _cannibalEnemyCount;
        _initialBoarCount = _boarEnemyCount;

        SpawnEnemies();
        StartCoroutine("CheckToSpawnEnemies");
    }

    void SpawnEnemies()
    {
        SpawnCannibals();
        SpawnBoars();
    }

    void SpawnCannibals()
    {
        for(var i = 0; i < _cannibalEnemyCount; i++)
        {
            var flareLocation = LocationUtils.FlareLocation;
            if (RandomUtils._random.NextDouble() > 0.5)
            {
                Instantiate(_canibalPrefab, _flarePosition, Quaternion.identity);
            }
            else
            {
                Instantiate(_canibalPrefab, new Vector3(
                    RandomUtils.GetRandomNumber(flareLocation["X_MIN"], flareLocation["X_MAX"]),
                    RandomUtils.GetRandomNumber(flareLocation["Y_MIN"], flareLocation["Y_MAX"]),
                    RandomUtils.GetRandomNumber(flareLocation["Z_MIN"], flareLocation["Z_MAX"])), Quaternion.identity);
            }
            print("Cannibal spawn");
        }

        _cannibalEnemyCount = 0;
    }

    void SpawnBoars()
    {
        for (int i = 0; i < _boarEnemyCount; i++)
        {
            var flareLocation = LocationUtils.FlareLocation;
            if (RandomUtils._random.NextDouble() > 0.5)
            {
                Instantiate(_boarPrefab, _flarePosition, Quaternion.identity);
            }
            else
            {
                Instantiate(_boarPrefab, new Vector3(
                    RandomUtils.GetRandomNumber(flareLocation["X_MIN"], flareLocation["X_MAX"]),
                    RandomUtils.GetRandomNumber(flareLocation["Y_MIN"], flareLocation["Y_MAX"]),
                    RandomUtils.GetRandomNumber(flareLocation["Z_MIN"], flareLocation["Z_MAX"])), Quaternion.identity);
            }

            print("Boar spawn");
        }

        _boarEnemyCount = 0;
    }

    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(waitBeforeEnemiesTime);

        SpawnCannibals();
        SpawnBoars();

        StartCoroutine("CheckToSpawnEnemies");
    }

    public void EnemyDied(bool cannibal)
    {
        if(cannibal)
        {
            _cannibalEnemyCount++;
            if(_cannibalEnemyCount > _initialCannibalCount)
            {
                _cannibalEnemyCount = _initialCannibalCount;
            }
            else
            {
                _boarEnemyCount++;
                if(_boarEnemyCount > _initialBoarCount)
                {
                    _boarEnemyCount = _initialBoarCount;
                }
            }
        }
    }

    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }

    public void LevelUp(Vector3 flarePosition)
    {
        _initialCannibalCount += _initialCannibalCount;
        _cannibalEnemyCount += _initialCannibalCount;

        _initialBoarCount += _initialBoarCount;
        _boarEnemyCount += _initialBoarCount;
        _flarePosition = flarePosition;
    }
}
