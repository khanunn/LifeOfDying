using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObject : MonoBehaviour
{
    private ThirdPersonShooterController thirdPersonShooterController;
    public string tagHeal, tagClip, tagFood, tagWater, tagPrimary, tagSecondary, tagThirdary, tagMinimap;
    // Start is called before the first frame update
    void Start()
    {
        thirdPersonShooterController = GetComponent<ThirdPersonShooterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == tagHeal)
        {
            thirdPersonShooterController.GetHeal(25);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == tagClip)
        {
            thirdPersonShooterController.GetClip(Random.Range(5, 12));
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == tagFood)
        {
            thirdPersonShooterController.GetFood(Random.Range(10, 20));
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == tagWater)
        {
            thirdPersonShooterController.GetWater(Random.Range(10, 20));
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == tagPrimary)
        {
            thirdPersonShooterController.GetPistol();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == tagSecondary)
        {
            thirdPersonShooterController.GetAxe();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == tagThirdary)
        {
            thirdPersonShooterController.GetFlashlight();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == tagMinimap)
        {
            thirdPersonShooterController.GetMapScrap(1);
            Destroy(collision.gameObject);
        }
    }
}
