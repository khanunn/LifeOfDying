using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoText : MonoBehaviour
{
    //public AmmoText ammoText;
    //private string maxValue;
    public Text ammoText;
    public Text clipText;
    //private string maxAmmo;
    public void SetMaxAmmo(int ammo)
    {
        ammoText.text = " "+ammo;
        /*maxValue = ammo;
        ammoText.text = ammo;*/
    }

    public void SetAmmo(int ammo)
    {
        ammoText.text = " "+ ammo;
        //ammoText.text = ammo;
    }

    public void SetClip(int clip)
    {
        clipText.text = "/"+clip;
    }
}
