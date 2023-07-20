using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public GameObject allEnemy;
    public BossArea bossArea;
    public GameObject dialog;
    //public GameObject lastboss;
    private int clear;
    //public bool DestroyEnemy;
    // Start is called before the first frame update
    void Start()
    {
        allEnemy.SetActive(true);
        dialog.SetActive(false);
        //lastboss.SetActive(false);
        //DestroyEnemy = false;
        //missionOne = GetComponent<Mission1>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeathEnemy()
    {
        clear += 1;
        if (clear >= 3)
        {
            bossArea.MissionClear();
            allEnemy.SetActive(false);
            dialog.SetActive(true);
            StartCoroutine(CloseScene(6.0f));
            //lastboss.SetActive(true);
        }
    }
    IEnumerator CloseScene(float secondUntildestroy)
    {
        yield return new WaitForSeconds(secondUntildestroy);
        dialog.SetActive(false);
    }
}
