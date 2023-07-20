using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform vfxHit;
    [SerializeField] private Transform vfxEnergy;
    private Rigidbody bulletRigidbody;

    private void Awake() {
        bulletRigidbody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        float speed = 100f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.GetComponent<BulletTarget>() != null)
        {
            //hit target
            Instantiate(vfxHit, transform.position, Quaternion.identity);
            //Destroy(vfxHit.gameObject);
        }

        else if(other.GetComponent<EnergyTarget>() != null)
        {
            Instantiate(vfxEnergy, transform.position, Quaternion.identity);
        }
        else
        {
            //hit something else
        }
        Destroy(gameObject);
    }
}

