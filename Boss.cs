using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    Transform targetObject;
    public Transform boxTransform;
    public float fireRate = 1f;
    private float nextFire = 0.0f;
    public string tagObject;
    public GameObject enemyGun, enemyBullet, muzzle ,soundShoot;
    public int enemyHP = 50;
    Animator animator;
    UnityEngine.AI.NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        muzzle.SetActive(false);
        animator = GetComponent<Animator>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (targetObject == null)
        {
            targetObject = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        navAgent.destination = targetObject.position;
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            animator.SetBool("Attack", true);
            //transform.position = Vector3.Lerp(transform.position, targetObject.rotation, Time.deltaTime * 20f);


            if (Time.time > nextFire)
            {
                if (enemyHP > 0)
                {
                    nextFire = Time.time + fireRate;
                    muzzle.SetActive(true);
                    StartCoroutine(MuzzleOff(0.8f));
                    Instantiate(enemyBullet, enemyGun.transform.position, enemyGun.transform.rotation);
                    Instantiate(soundShoot, enemyGun.transform.position, enemyGun.transform.rotation);
                    //Vector3 worldAimTarget = mouseWorldPosition;
                    //worldAimTarget.y = transform.position.y;
                    //Vector3 aimDirection = (targetObject - transform.position).normalized;
                }
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, boxTransform.rotation, Time.deltaTime * 20f);
                //animator.SetBool("Attack", false);
            }
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }
    IEnumerator MuzzleOff(float secondUntildestroy)
    {
        yield return new WaitForSeconds(secondUntildestroy);
        muzzle.SetActive(false);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == tagObject)
        {
            if (enemyHP <= 0)
            {
                animator.SetTrigger("Death");
                Destroy(collision.gameObject);
                navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
                navAgent.isStopped = true;
                StartCoroutine(clearObject());
            }
            else
            {
                enemyHP -= 1;
                //Debug.Log("enemyHP -1");
            }
        }
    }
    IEnumerator clearObject()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
    }

    public void EnemyDeath()
    {
        Destroy(this.gameObject);
    }
}