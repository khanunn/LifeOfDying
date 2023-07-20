using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterArea : MonoBehaviour
{
    public GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn.SetActive(false);
        //Debug.Log("SpawnCheck");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spawn.SetActive(true);
        }
    }
}
