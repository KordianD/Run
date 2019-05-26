using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager _weaponManager;
    private float _nextTimeToFire;
    private float _nextTimeToFireGun;

    public float fireRate = 15f;

    private Animator _zoomCameraAnim;
    private bool _zoomed;
    private Camera _mainCam;
    private GameObject _croshair;
    private bool _isAiming;

    [SerializeField]
    private GameObject _spearPrefab;
    [SerializeField]
    private Transform _spearStartPosition;

    private void Awake()
    {
        _weaponManager = GetComponent<WeaponManager>();
        _zoomCameraAnim = transform.Find(TagsExtensions.LOOK_ROOT).transform
            .Find(TagsExtensions.ZOOM_CAMERA).GetComponent<Animator>();

        _croshair = GameObject.FindWithTag(TagsExtensions.CROSSHAIR);

        _mainCam = Camera.main;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
    }

    void WeaponShoot()
    {
        if (_weaponManager.GetCurrentWeaponAmmo() < 1) return;
        if (_weaponManager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            if (Input.GetMouseButton(0) && Time.time > _nextTimeToFire)
            {
                _nextTimeToFire = Time.time + 1f / fireRate;
                _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                BulletFired();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && Time.time > _nextTimeToFireGun)
            {
                _nextTimeToFireGun = Time.time + 1.5f;
                if (_weaponManager.GetCurrentSelectedWeapon().tag == TagsExtensions.AXE_TAG)
                {
                    _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                }

                if (_weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                    BulletFired();
                }
                else
                {
                    if (_isAiming)
                    {
                        _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                        if (_weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.ARROW)
                        {

                        }
                        else if (_weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.SPEAR)
                        {
                            ThrowSpear(true);
                        }
                    }
                }
            }
        }
    }

    void ZoomInAndOut()
    {
        if (_weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _zoomCameraAnim.Play(AnimationUtils.ZOOM_IN_ANIM);

                _croshair.SetActive(false);
            }

            if (Input.GetMouseButtonUp(1))
            {
                _zoomCameraAnim.Play(AnimationUtils.ZOOM_OUT_ANIM);

                _croshair.SetActive(true);
            }
        }

        if (_weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _weaponManager.GetCurrentSelectedWeapon().Aim(true);
                _isAiming = true;
            }

            if (Input.GetMouseButtonUp(1))
            {
                _weaponManager.GetCurrentSelectedWeapon().Aim(false);
                _isAiming = false;
            }
        }
    }
    void ThrowSpear(bool throwSpear)
    {
        if (throwSpear)
        {
            GameObject spear = Instantiate(_spearPrefab);
            spear.transform.position = _spearStartPosition.position;
            spear.GetComponent<SpearScript>().Launch(_mainCam);

            BulletFired();
        }
    }

    void BulletFired()
    {
        RaycastHit hit;
        if( Physics.Raycast(_mainCam.transform.position, _mainCam.transform.forward, out hit))
        {
            print("WE HIT: " + hit.transform.gameObject.name);
            if(hit.transform.tag == TagsExtensions.ENEMY_TAG)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage
                    (DamageUtils.DamageByWeapon[_weaponManager.currentWeaponIndex]);
            }
        }

        _weaponManager.BulletFired();
    }
}
