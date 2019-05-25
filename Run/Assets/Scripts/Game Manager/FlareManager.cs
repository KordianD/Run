using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareManager : MonoBehaviour
{
    public static FlareManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetNewPlace()
    {
        var flareLocation = LocationUtils.FlareLocation;
        print(GameObject.FindGameObjectWithTag(TagsExtensions.FLARE_TAG).transform.position);
        Destroy(gameObject);
        for (int i = 0; i < 100; i++)
        {
            Instantiate(gameObject, new Vector3(
                RandomUtils.GetRandomNumber(flareLocation["X_MIN"], flareLocation["X_MAX"]),
                RandomUtils.GetRandomNumber(flareLocation["Y_MIN"], flareLocation["Y_MAX"]),
                RandomUtils.GetRandomNumber(flareLocation["Z_MIN"], flareLocation["Z_MAX"])), Quaternion.identity);
        }

        print(GameObject.FindGameObjectWithTag(TagsExtensions.FLARE_TAG).transform.position);
    }
}
