using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    Transform targetObject;
    public GameObject enemy;
    UnityEngine.AI.NavMeshAgent navAgent;
    //public Transform Righthere;
    float timeElapesd = 0;
    float ItemCycle = 3.0f;

    private void Awake() {
        navAgent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (targetObject == null)
        {
            targetObject = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    void Update()
    {
        navAgent.destination = targetObject.position;
        timeElapesd += Time.deltaTime;
        if (timeElapesd > ItemCycle)
        {
            GameObject temp;
            temp = (GameObject)Instantiate(enemy);
            //transform.position = Righthere.position;
            Vector3 pos = temp.transform.position;
            temp.transform.position = new Vector3(Random.Range(-2, -2), 0, Random.Range(0, 0));
            //Instantiate(enemy, temp.transform.position);
            timeElapesd -= ItemCycle;
        }
    }
}