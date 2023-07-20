using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private Transform vfxHit;
    //private Rigidbody bulletRigidbody;
    [SerializeField] public Transform handEnemy;
    //private BulletEnemy bulletEnemy;

    private void Awake() {
        //bulletRigidbody = GetComponent<Rigidbody>();
        //bulletEnemy = GetComponent<BulletEnemy>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //float speed = 50f;
        //bulletRigidbody.velocity = transform.forward * speed;
    }

    private void Update() 
    {
        transform.LookAt(handEnemy.position);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * 20f);
    }

    private void OnTriggerEnter(Collider other) 
    {
        /*if (other.GetComponent<BulletTarget>() != null)
        {
            //hit target
            //Instantiate(vfxHit, transform.position, Quaternion.identity);
            //Destroy(other.gameObject);
            //Destroy(vfxHit.gameObject);
            //print("HP-1");
        }
        else
        {
            //hit something else
        }*/
        Destroy(gameObject);
    }
}

