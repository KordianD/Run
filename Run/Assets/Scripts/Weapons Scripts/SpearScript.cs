using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScript : MonoBehaviour
{
    void Awake()
    {
        _myBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateGameObject", deactivateTimer);
    }

    // Update is called once per frame
    void DeactivateGameObject()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }       
    }

    private void OnTriggerEnter(Collider target)
    {
        
    }

    public void Launch(Camera mainCamera)
    {
        _myBody.velocity = mainCamera.transform.forward * speed;
        transform.LookAt(transform.position + _myBody.velocity);
    }


    private Rigidbody _myBody;
    public float speed = 30f;
    public float deactivateTimer = 3f;
    public float damage = 15f;
}










