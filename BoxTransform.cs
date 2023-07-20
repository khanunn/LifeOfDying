using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTransform : MonoBehaviour
{
    Transform targetObject;
    //public Transform boxTransform;
    //public float fireRate = 1f;
    //private float nextFire = 0.0f;
    //public string tagObject;
    //public GameObject enemyGun, enemyBullet, muzzle;
    //Animator animator;
    UnityEngine.AI.NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        //muzzle.SetActive(false);
        //animator = GetComponent<Animator>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (targetObject == null)
        {
            targetObject = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        navAgent.destination = targetObject.position;
    }
}
