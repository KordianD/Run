using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance;

    void Awake()
    {
        MakeInstance();
        _flare = Instantiate(_flarePrefab, LocationUtils.StartedFlarePoint, Quaternion.identity);
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
        if (Vector3.Distance(player.transform.position, _flare.transform.position) <= _toNextLevelDistance)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        SetNewCheckpoint();
        player.GetComponent<PlayerStats>().Level++;
        EnemyManager.instance.LevelUp(_flare.transform.position);
        var enemies = GameObject.FindGameObjectsWithTag(TagsExtensions.ENEMY_TAG);
        for (var i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().IncreasePower();
        }
        player.GetComponent<PlayerStats>().Level++;
    }

    private void SetNewCheckpoint()
    {
        var flareLocation = LocationUtils.FlareLocation;
        Destroy(_flare);

        _flare = Instantiate(_flarePrefab, new Vector3(
            RandomUtils.GetRandomNumber(flareLocation["X_MIN"], flareLocation["X_MAX"]),
            RandomUtils.GetRandomNumber(flareLocation["Y_MIN"], flareLocation["Y_MAX"]),
            RandomUtils.GetRandomNumber(flareLocation["Z_MIN"], flareLocation["Z_MAX"])), Quaternion.identity);
    }

    [SerializeField] private GameObject _flarePrefab;
    private GameObject _flare;
    private GameObject player;
    private const float _toNextLevelDistance = 5f;
}
