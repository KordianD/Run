using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField]
    private GameObject _boarPrefab, _canibalPrefab;

    public Transform[] cannibalSpawnPoints, boarSpawnPoints;

    [SerializeField]
    private int _cannibalEnemyCount, _boarEnemyCount;
    private int _initialCannibalCount, _initialBoarCount;
    public float waitBeforeEnemiesTime = 10f;


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
        int index = 0;
        for(int i =0; i < _cannibalEnemyCount; i++)
        {
            if(index >= cannibalSpawnPoints.Length)
            {
                index = 0;
            }

            Instantiate(_canibalPrefab, cannibalSpawnPoints[index].position, Quaternion.identity);
            print("Cannibal spawn");
            index++;
        }

        _cannibalEnemyCount = 0;
    }

    void SpawnBoars()
    {
        int index = 0;
        for (int i = 0; i < _boarEnemyCount; i++)
        {
            if (index >= boarSpawnPoints.Length)
            {
                index = 0;
            }

            Instantiate(_boarPrefab, boarSpawnPoints[index].position, Quaternion.identity);
            print("boar spawn");
            index++;
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
}
