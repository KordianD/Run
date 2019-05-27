using System.Collections.Generic;
using UnityEngine;

public class LocationUtils
{
    public static Dictionary<string, float> FlareLocation = new Dictionary<string, float>
    {
        { "X_MIN", 4f},
        { "X_MAX", 110f},
        { "Y_MIN", 20f},
        { "Y_MAX", 25f},
        { "Z_MIN", 4f},
        { "Z_MAX", 245f},
    };

    public static Vector3 StartedFlarePoint = new Vector3(40f, 25f, 15f);
    public static Vector3 StartedPotionPoint = new Vector3(55f, 27f, 11f);
    public static Vector3 StartedAmmoPoint = new Vector3(33f, 27.5f, 210f);
}
