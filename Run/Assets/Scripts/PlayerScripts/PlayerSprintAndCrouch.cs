using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _lookRoot = transform.GetChild(0);

        _playerFootsteps = GetComponentInChildren<PlayerFootsteps>();
    }

    void Start()
    {
        _playerFootsteps.volumeMax = _walkVolumeMax;
        _playerFootsteps.volumeMin = _walkVolumeMin;
        _playerFootsteps.stepDistance = _walkStepDistance;

    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }
    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_isCrouching)
        {
            _playerMovement.SetSpeed(_sprintSpeed);
            
            _playerMovement.SetSpeed(_sprintSpeed);

            _playerFootsteps.stepDistance = _sprintStepDistance;
            _playerFootsteps.volumeMin = _sprintVolume;
            _playerFootsteps.volumeMax = _sprintVolume;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !_isCrouching)
        {
            _playerMovement.SetSpeed(_moveSpeed);

            _playerFootsteps.stepDistance = _walkStepDistance;
            _playerFootsteps.volumeMax = _walkVolumeMax;
            _playerFootsteps.volumeMin = _walkVolumeMin;

        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            
            if (_isCrouching)
            {
                _lookRoot.localPosition = new Vector3(0f, _standHeight, 0f);
                _playerMovement.SetSpeed(_moveSpeed);

                _playerFootsteps.stepDistance = _walkStepDistance;
                _playerFootsteps.volumeMax = _walkVolumeMin;
                _playerFootsteps.volumeMin = _walkVolumeMax;

                _isCrouching = false;
            }
            else
            {
                _lookRoot.localPosition = new Vector3(0f, _crouchHeight, 0f);
                _playerMovement.SetSpeed(_crouchSpeed);

                _playerFootsteps.stepDistance = _crouchStepDistance;
                _playerFootsteps.volumeMax = _crouchVolume;
                _playerFootsteps.volumeMin = _crouchVolume;

                _isCrouching = true;
            }
        }
    }

    public PlayerMovement _playerMovement;
    private float _sprintSpeed = 10f;
    private float _moveSpeed = 5f;
    private float _crouchSpeed = 2f;
    private Transform _lookRoot;
    private float _standHeight = 1.6f;
    private float _crouchHeight = 1f;
    private bool _isCrouching;
    private PlayerFootsteps _playerFootsteps;
    private float _sprintVolume = 1f;
    private float _crouchVolume = 0.1f;
    private float _walkVolumeMin = 0.2f, _walkVolumeMax = 0.6f;
    private float _walkStepDistance = 0.4f;
    private float _sprintStepDistance = 0.25f;
    private float _crouchStepDistance = 0.5f;
}
