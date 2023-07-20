using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogArea1 : MonoBehaviour
{
    public GameObject dialog, wall;
    bool pass;
    // Start is called before the first frame update
    void Start()
    {
        dialog.SetActive(false);
        wall.SetActive(true);
        //mission.SetActive(false);
        pass = false;
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
            if (pass == false)
            {
                dialog.SetActive(true);
                wall.SetActive(false);
                StartCoroutine(CloseScene(4.0f));
                pass = true;
            }
        }
    }
    IEnumerator CloseScene(float secondUntildestroy)
    {
        yield return new WaitForSeconds(secondUntildestroy);
        dialog.SetActive(false);
        //mission.SetActive(true);
        Destroy(this);
        SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
    }
}
