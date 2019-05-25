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

    public static Vector3 StartedFlarePoint = new Vector3(40, 27, 15);
}
