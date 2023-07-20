using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1 : MonoBehaviour
{
    //public GameObject energyOne;
    //public GameObject energyTwo;
    //public GameObject energyThree;
    public DestroyEnemy destroyEnemy;

    private int hpone;
    public string tagObject;

    // Start is called before the first frame update
    void Start()
    {
        hpone = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (hpone <= 0)
        {
            destroyEnemy.DeathEnemy();
            //clear += 1;
            //clearone = true;
            hpone = 0;
            Destroy(this.gameObject);
            //Debug.Log("Clear" + clear);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == tagObject)
        {
            hpone -= 1;

        }
    }
}
