using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(clearObject());
    }

    IEnumerator clearObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
