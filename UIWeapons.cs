using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWeapons : MonoBehaviour
{
    public GameObject pistol,axe,flashlight;
    // Start is called before the first frame update
    void Start()
    {
        pistol.SetActive(false);
        axe.SetActive(false);
        flashlight.SetActive(false);
    }

    public void ShowPistol()
    {
        pistol.SetActive(true);
    }
    public void ShowAxe()
    {
        axe.SetActive(true);
    }

    public void ShowFlashlight()
    {
        flashlight.SetActive(true);
    }
}
