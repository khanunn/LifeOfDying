using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeZombie : MonoBehaviour
{
    public GetSound zombieSet1, zombieSet2, zombieSet3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public void Taking()
    {
        zombieSet1.GetSounded();
        zombieSet2.GetSounded();
        zombieSet3.GetSounded();
    }
}
