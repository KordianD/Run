using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if(isBoar || isCannibal)
        {
            _enemyAnim = GetComponent<EnemyAnimator>();
            _enemyController = GetComponent<EnemyController>();
            _navAgent = GetComponent<NavMeshAgent>();

        }

        if(isPlayer)
        {

        }
    }

    // Update is called once per frame
    public void ApplyDamage(float damage)
    {
        if (_isDead)
            return;

        health -= damage;

        if(isPlayer)
        {

        }

        if(isBoar || isCannibal)
        {
            if(_enemyController.EnemyState == EnemyState.PATROL)
            {
                _enemyController.chaseDistance = 50f;
            }
        }

        if(health <= 0f)
        {
            PlayerDied();
            _isDead = true;
        }
    }

    public void PlayerDied()
    {
        if(isCannibal)
        {
            print("tutaj");
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 10f);
            _enemyController.enabled = false;
            _navAgent.enabled = false;
            _enemyAnim.enabled = false;

        }
        if(isBoar)
        {
            _navAgent.velocity = Vector3.zero;
            _navAgent.isStopped = true;
            _enemyController.enabled = false;
            _enemyAnim.Dead();
        }
        if(isPlayer)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(TagsExtensions.ENEMY_TAG);
            for(int i =0; i<enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
        }

        if(tag == TagsExtensions.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

    private EnemyAnimator _enemyAnim;
    private NavMeshAgent _navAgent;
    private EnemyController _enemyController;
    public float health = 100f;
    public bool isPlayer, isBoar, isCannibal;

    private bool _isDead;
}
