using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionArea3 : MonoBehaviour
{
    public GameObject dialog, stageOpen;
    bool pass;
    //int lvlmission;
    public MissionText missionText;
    // Start is called before the first frame update
    void Start()
    {
        dialog.SetActive(false);
        stageOpen.SetActive(false);
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
                StartCoroutine(CloseScene(5.0f));
                pass = true;
            }
        }
    }
    IEnumerator CloseScene(float secondUntildestroy)
    {
        yield return new WaitForSeconds(secondUntildestroy);
        dialog.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        missionText.SetMissionIV(0,Color.red,false);
        stageOpen.SetActive(true);
        Destroy(this);
    }
}
