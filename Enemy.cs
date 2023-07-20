using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //private ThirdPersonShooterController thirdPersonShooterController;
    Transform targetObject;
    Transform soundObject;
    public GameObject bulletEnemy;
    //public GameObject soundGun;
    private float fireRate = 1f;
    private float nextFire = 0.0f;
    //private int hp = 4;
    public string tagObject;
    public string meleeObject;
    //public Collider headObject;
    //public string areaObject;
    //public GameObject enemyGun, enemyBullet, muzzle ,soundShoot;
    Animator animator;
    UnityEngine.AI.NavMeshAgent navAgent;

    private float lookRadius;
    private float soundRadius;

    private int enemyHP = 5;
    private bool getSounded;

    

    //private bool soundReady = false;
    // Start is called before the first frame update
    void Start()
    {
        //hp = 4;
        //muzzle.SetActive(false);
        //thirdPersonShooterController = GetComponent<ThirdPersonShooterController>();
        lookRadius = 10;
        soundRadius = 30;
        getSounded = false;
        bulletEnemy.SetActive(false);
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
        float distance = Vector3.Distance(targetObject.position, transform.position);
        if (distance <= soundRadius)
        {
            if (getSounded == true)
            {
                if (enemyHP >= 1)
                {
                    navAgent.destination = targetObject.position;
                    animator.SetBool("Run", true);
                    //animator.SetBool("Attack", false);
                }
            }
        }
        else
        {
            getSounded = false;
        }
        if (distance <= lookRadius)
        {
            if (enemyHP >= 1)
            {
                navAgent.destination = targetObject.position;
                animator.SetBool("Run", true);
                if (navAgent.remainingDistance <= navAgent.stoppingDistance)
                {
                    FaceTarget();
                    animator.SetBool("Run", false);
                    animator.SetBool("Attack", true);
                    StartCoroutine(Attacked());
                    if (Time.time > nextFire)
                    {
                        if (enemyHP > 0)
                        {
                            nextFire = Time.time + fireRate;
                            //Instantiate(bulletEnemy, handEnemy.transform.position, handEnemy.transform.rotation);
                            //muzzle.SetActive(true);
                            //StartCoroutine(MuzzleOff(0.8f));
                            //Instantiate(enemyBullet, enemyGun.transform.position, enemyGun.transform.rotation);
                            //Instantiate(soundShoot, enemyGun.transform.position, enemyGun.transform.rotation);
                        }
                    }
                }
                else
                {
                    animator.SetBool("Attack", false);
                    bulletEnemy.SetActive(false);
                }
            }
            else
            {
                animator.SetTrigger("Death");
                gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                //Destroy(collision.gameObject);
                StartCoroutine(clearObject());
            }
        }
        /*else
        {
            //animator.SetBool("Run", false);
            getSounded = false;
        }*/
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == tagObject)
        {
            enemyHP -= 5;
            Debug.Log("enemyHP -5");
            animator.SetTrigger("Death");
            Destroy(collision.gameObject);
            //navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            //navAgent.isStopped = true;
            //navAgent.enable = false;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            StartCoroutine(clearObject());
            /*if (enemyHP <= 0)
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
                Debug.Log("enemyHP -1");
            }*/
        }
        if (collision.gameObject.tag == meleeObject)
        {
            bulletEnemy.SetActive(false);
            enemyHP -= 3;
            animator.SetBool("Damaged", true);
            StartCoroutine(Damaged());
            Debug.Log("enemyHP -3");
        }
    }

    IEnumerator Attacked()
    {
        yield return new WaitForSeconds(1f);
        bulletEnemy.SetActive(true);
    }

    IEnumerator Damaged()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("Damaged", false);
        bulletEnemy.SetActive(true);
    }
    IEnumerator clearObject()
    {
        yield return new WaitForSeconds(2f);
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //navAgent.isStopped = true;
        Destroy(this.gameObject);
    }

    public void EnemyDeath()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, soundRadius);
    }

    void FaceTarget()
    {
        Vector3 direction = (targetObject.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void GetSounded()
    {
        getSounded = true;
        //getSound.GetSounded(true) = getSounded;
    }
}