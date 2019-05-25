using System.Collections.Generic;
using UnityEngine;

public class DamageUtils : MonoBehaviour
{
    public static Dictionary<int, float> DamageByWeapon = new Dictionary<int, float>
    {
        {0, 20},
        {1, 20},
        {2, 40},
        {3, 100},
        {4, 30},
    };
}
