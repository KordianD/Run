using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] _weapons;

    public int currentWeaponIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentWeaponIndex = 0;
        _weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectedWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnOnSelectedWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurnOnSelectedWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TurnOnSelectedWeapon(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TurnOnSelectedWeapon(4);
        }


    }
    void TurnOnSelectedWeapon(int weaponIndex)
    {
        if (currentWeaponIndex == weaponIndex)
            return;

        _weapons[currentWeaponIndex].gameObject.SetActive(false);
        _weapons[weaponIndex].gameObject.SetActive(true);

        currentWeaponIndex = weaponIndex;
    }

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return _weapons[currentWeaponIndex];
    }

    public double GetCurrentWeaponAmmo()
    {
        return _weaponsAmmos[currentWeaponIndex];
    }

    public void BulletFired()
    {
        _weaponsAmmos[currentWeaponIndex] -= 1d;
    }

    public void IncreaseAmmoByLevel()
    {
        _weaponsAmmos[2] += 20d;
        _weaponsAmmos[3] += 10d;
        _weaponsAmmos[4] += 30d;
    }

    public void IncreaseAmmoByItem()
    {
        _weaponsAmmos[2] += 40d;
        _weaponsAmmos[3] += 20d;
        _weaponsAmmos[4] += 60d;
    }

    private Dictionary<int, double> _weaponsAmmos = new Dictionary<int, double>
    {
        { 0, double.PositiveInfinity},
        { 1, double.PositiveInfinity},
        { 2, 20d },
        { 3, 10d },
        { 4, 30d }
    };
}
