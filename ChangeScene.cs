using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void changeScene(string sceneName)
    {
        StartCoroutine(NextScene(sceneName));
    }

    IEnumerator NextScene(string sceneName) 
    {
        yield return new WaitForSeconds(1.0f);
        
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
