using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        
        _footstepSound = GetComponent<AudioSource>();

        _characterController = GetComponentInParent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound()
    {
        if (!_characterController.isGrounded)
            return;

        if (_characterController.velocity.sqrMagnitude > 0)
        {
            _accumulatedDistance += Time.deltaTime;
            if (_accumulatedDistance > stepDistance)
            {
                _footstepSound.volume = Random.Range(volumeMin, volumeMax);
                _footstepSound.clip = _footstepClip[Random.Range(0, _footstepClip.Length)];
                _footstepSound.Play();

                _accumulatedDistance = 0f;
            }
        }
        else
        {
            _accumulatedDistance = 0f;
        }
        
    }



    private AudioSource _footstepSound;
    [SerializeField]
    private AudioClip[] _footstepClip;
    
    private CharacterController _characterController;
    [HideInInspector]
    public float volumeMin, volumeMax;
    private float _accumulatedDistance;
    [HideInInspector]
    public float stepDistance;
    
}
