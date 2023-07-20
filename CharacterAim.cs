using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAim : MonoBehaviour
{
    //public float turnSpeed = 15;
    //public float aimDuration = 0.3f;

    //Camera mainCamera;
    RaycastWeapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        //mainCamera = Camera.main;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        weapon = GetComponentInChildren<RaycastWeapon>();
    }
    /*void FixedUpdate() {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,yawCamera,0), turnSpeed * Time.fixedDeltaTime);
    }*/
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            weapon.StartFiring();
        }
        if(Input.GetButtonUp("Fire1"))
        {
            weapon.StopFiring();
        }
    }
}
