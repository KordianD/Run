using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}

public class WeaponHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void ShootAnimation()
    {
        _anim.SetTrigger(AnimationUtils.SHOOT_TRIGGER);
    }

    public void Aim(bool canAim)
    {
        _anim.SetBool(AnimationUtils.AIM_PARAMETER, canAim);
    }

    public void TurnOnMuzzleFlash()
    {
        _muzzleFlash.SetActive(true);
    }

    public void TurnOffMuzzleFlash()
    {
        _muzzleFlash.SetActive(false);
    }

    void PlayShootSound()
    {
        _shootSound.Play();
    }

    void PlayReloadSound()
    {
        _reloadSound.Play();
    }

    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }

    private Animator _anim;
    [SerializeField]
    private GameObject _muzzleFlash;
    [SerializeField]
    private AudioSource _shootSound, _reloadSound;

    public WeaponAim weaponAim;
    public WeaponFireType fireType;
    public WeaponBulletType bulletType;
    public GameObject attackPoint;
}
