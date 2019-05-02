using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _anim;

    // Start is called before the first frame update
    void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    
    public void Walk(bool walk)
    {
        _anim.SetBool(TagsExtensions.WALK_PARAMETER, walk);

    }

    public void Run(bool run)
    {
        _anim.SetBool(TagsExtensions.RUN_PARAMETER, run);
    }

    public void Attack()
    {
        _anim.SetTrigger(TagsExtensions.ATTACK_TRIGGER);
    }

    public void Dead()
    {
        _anim.SetTrigger(TagsExtensions.DEAD_TRIGGER);
    }
}
 