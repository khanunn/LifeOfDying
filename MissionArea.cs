using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionArea : MonoBehaviour
{
    public GameObject dialog, wall, mission;
    bool pass;
    //int lvlmission;
    //public MissionText missionText;
    // Start is called before the first frame update
    void Start()
    {
        //lvlmission = 1;
        dialog.SetActive(false);
        wall.SetActive(true);
        mission.SetActive(false);
        pass = false;
        //Debug.Log("SpawnCheck");
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (pass == false)
            {
                dialog.SetActive(true);
                wall.SetActive(false);
                StartCoroutine(CloseScene(18.0f));
                pass = true;
            }
        }
    }
    IEnumerator CloseScene(float secondUntildestroy)
    {
        yield return new WaitForSeconds(secondUntildestroy);
        dialog.SetActive(false);
        mission.SetActive(true);
        Destroy(this);
    }
}
