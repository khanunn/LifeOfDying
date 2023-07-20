using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArea : MonoBehaviour
{
    public GameObject spawn;
    //public GameObject energy;
    // Start is called before the first frame update
    void Start()
    {
        spawn.SetActive(false);
        gameObject.SetActive(false);
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

    public void MissionClear()
    {
        gameObject.SetActive(true);
    }
}
