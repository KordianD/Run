using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        _enemyState = EnemyState.PATROL;
        patrolTimer = patrolForThisTime;
        _attackTimer = waitBeforeAttack;
        _currentChaseDistance = chaseDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(_enemyState == EnemyState.PATROL)
        {
            Patrol();
        }
        if(_enemyState == EnemyState.CHASE)
        {
            Chase();
        }
        if (_enemyState == EnemyState.ATTACK)
        {
            Attack();
        }

    }

    public void Awake()
    {
        _enemyAnim = GetComponent<EnemyAnimator>();
        _navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(TagsExtensions.PLAYER_TAG).transform;
    }

    void Patrol()
    {
        _navAgent.isStopped = false;
        _navAgent.speed = walkSpeed;
        patrolTimer += Time.deltaTime;

        if(patrolTimer > patrolForThisTime)
        {
            SetNewRandomDestination();
            patrolTimer = 0f;
        }

        if(_navAgent.velocity.sqrMagnitude > 0)
        {
            _enemyAnim.Walk(true);
        }
        else
        {
            _enemyAnim.Walk(false);
        }
    }

    void Chase()
    { }

    void Attack()
    { }

    void SetNewRandomDestination()
    {
        float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);

        Vector3 randDir = Random.insideUnitSphere * randRadius;
        randDir += transform.position;
        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, randRadius, -1);

        _navAgent.SetDestination(navHit.position);
    }

    private EnemyAnimator _enemyAnim;
    private NavMeshAgent _navAgent;
    private EnemyState _enemyState;

    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;
    public float chaseDistance = 7f;
    private float _currentChaseDistance;
    public float attackDistance = 1.8f;
    public float chaseAfterAttackDistance = 2f;
    public float patrolRadiusMin = 20f, patrolRadiusMax = 60f;
    public float patrolForThisTime = 15f;
    public float patrolTimer;
    public float waitBeforeAttack = 2f;
    private float _attackTimer;
    private Transform target;

}
