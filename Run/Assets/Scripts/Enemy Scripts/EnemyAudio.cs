using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    
    [SerializeField]
    private AudioClip _sreamClip, _dieClip;

    [SerializeField]
    private AudioClip[] _attackClips;

    // Start is called before the first frame update
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

    }

    
    public void PlayScreamSound()
    {
        _audioSource.clip = _sreamClip;
        _audioSource.Play();
    }

    public void PlayAttackSound()
    {
        _audioSource.clip = _attackClips[Random.Range(0, _attackClips.Length)];
        _audioSource.Play();
    }

    public void PlayDeadSound()
    {
        _audioSource.clip = _dieClip;
        _audioSource.Play();
    }
}
