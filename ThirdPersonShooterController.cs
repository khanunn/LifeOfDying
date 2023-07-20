using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private float normalMovespeed;
    [SerializeField] private float aimMovespeed;
    [SerializeField] private float normalSprintspeed;
    [SerializeField] private float aimSprintspeed;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform bullet;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject soundShoot;
    [SerializeField] private GameObject meleeBullet;
    [SerializeField] private GameObject uiAmmo;


    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private CharacterController characterController;
    private PlayerInput playerInput;
    //private TakeZombie takeZombie;
    public string tagObject;

    //public string tagBoss;
    private bool shootReady;
    public int maxHealth = 100;
    public int currentHealth;
    private float healthFallRate;

    private int maxHungry = 100;
    private int currentHungry;
    private float hungryFallRate;
    private int maxWater = 100;
    private int currentWater;
    private float waterFallRate;

    public HealthBar healthBar;

    public AmmoText ammoText;

    static bool visible;

    public int maxAmmo = 12;
    public int currentAmmo;
    private int clipSize = 24;
    private int currentClip;
    private bool reloadReady;

    public float fireRate = 1f;
    public float meleeRate = 1f;
    //private float mapRate = 2f;
    private float nextFire = 0.0f;
    private float nextMelee = 0.0f;
    //private float nextMap = 0.0f;

    private bool isDeath;

    //weaponswitch
    int weaponSelected = 0;
    bool havePistol;
    bool haveAxe;
    bool haveFlashlight;
    [SerializeField] private GameObject primary, secondary, thirdary;

    private TakeZombie takeZombie;
    public MissionText missionText;
    private UIWeapons uiWeapons;
    private int currentScrap;

    //Minimap
    private bool mapOpened;
    [SerializeField] private GameObject minimap;


    private void Awake()
    {
        uiWeapons = GetComponent<UIWeapons>();
        takeZombie = GetComponent<TakeZombie>();
        playerInput = GetComponent<PlayerInput>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        characterController = GetComponent<CharacterController>();
        //enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        uiAmmo.SetActive(false);
        meleeBullet.SetActive(false);
        muzzle.SetActive(false);
        //soundShoot.SetActive(false);
        crosshair.SetActive(false);

        isDeath = false;

        //HEALTH HUNGRY WATER CONTROLLER
        currentHealth = maxHealth;
        currentHungry = maxHungry;
        currentWater = maxWater;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHungry(maxHungry);
        healthBar.SetWater(maxWater);
        //cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //ammo
        currentAmmo = maxAmmo;
        currentClip = 0;
        ammoText.SetMaxAmmo(maxAmmo);
        ammoText.SetClip(currentClip);
        reloadReady = true;

        //weaponswitch
        primary.SetActive(false);
        secondary.SetActive(false);
        thirdary.SetActive(false);
        SwapWeapon(0);

        //Mission
        missionText.SetMissionI(0, Color.red);
        currentScrap = 0;

        //Minimap
        mapOpened = false;
        minimap.SetActive(false);

        /*//Health Hungry Water Controller
        healthFallRate = 5;
        hungryFallRate = 3;
        waterFallRate = 2f;*/
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        //Transform hitTransform = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            //hitTransform = raycastHit.transform;
        }
        if (weaponSelected == 1)
        {
            if (isDeath == false)
            {
                if (starterAssetsInputs.aim)
                {

                    crosshair.SetActive(true);
                    shootReady = true;
                    aimVirtualCamera.gameObject.SetActive(true);
                    thirdPersonController.SetSensitivity(aimSensitivity);
                    thirdPersonController.SetRotateOnMove(false);
                    thirdPersonController.SetMoveSpeed(aimMovespeed);
                    thirdPersonController.SetSprintSpeed(aimSprintspeed);
                    animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

                    Vector3 worldAimTarget = mouseWorldPosition;
                    worldAimTarget.y = transform.position.y;
                    Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

                    transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

                }
                else
                {
                    crosshair.SetActive(false);
                    shootReady = false;
                    aimVirtualCamera.gameObject.SetActive(false);
                    thirdPersonController.SetSensitivity(normalSensitivity);
                    thirdPersonController.SetRotateOnMove(true);
                    thirdPersonController.SetMoveSpeed(normalMovespeed);
                    thirdPersonController.SetSprintSpeed(normalSprintspeed);
                    animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
                    //animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
                }
            }
            else
            {
                crosshair.SetActive(false);
                shootReady = false;
                aimVirtualCamera.gameObject.SetActive(false);
                thirdPersonController.SetSensitivity(normalSensitivity);
                thirdPersonController.SetRotateOnMove(true);
                thirdPersonController.SetMoveSpeed(normalMovespeed);
                thirdPersonController.SetSprintSpeed(normalSprintspeed);
                animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            }

        }
        else
        {
            crosshair.SetActive(false);
            shootReady = false;
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            thirdPersonController.SetMoveSpeed(normalMovespeed);
            thirdPersonController.SetSprintSpeed(normalSprintspeed);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            //animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
        }

        if (starterAssetsInputs.shoot)
        {
            /*if(hitTransform != null)
            {
                if (hitTransform.GetComponent<BulletTarget>() != null)
                {
                    //hit target
                    Instantiate(vfxHit, transform.position, Quaternion.identity);
                }
                else
                {
                    //hit something else
                }
            }*/
            if (weaponSelected == 2)
            {
                if (Time.time > nextMelee)
                {
                    //StartCoroutine(ExampleCoroutine());
                    nextMelee = Time.time + meleeRate;
                    animator.SetBool("Attack", true);
                    meleeBullet.SetActive(true);
                    StartCoroutine(Attacked());
                    StartCoroutine(MeleeOff(0.1f));
                }
                //animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 5f));
            }
            if (shootReady == true)
            {
                //animator.SetTrigger("Shooting");
                if (currentAmmo > 0)
                {
                    if (Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        //enemy.SoundGun();
                        TakeAmmo(1);
                        takeZombie.Taking();
                        //TakeZombie();
                        //animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 10f));
                        Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                        Instantiate(bullet, spawnBulletPosition.position, Quaternion.LookRotation(aimDir));
                        Instantiate(soundShoot, spawnBulletPosition.position, Quaternion.LookRotation(aimDir));
                        starterAssetsInputs.shoot = false;
                        muzzle.SetActive(true);
                        //soundShoot.SetActive(true);
                        StartCoroutine(MuzzleOff(0.5f));
                    }
                }
            }
            else
            {
                //animator.SetBool("Attack", false);
            }
        }

        if (starterAssetsInputs.reload)
        {
            if (shootReady == false)
            {
                if (reloadReady == true)
                {
                    if (currentClip > 0)
                    {
                        if (currentAmmo < 12)
                        {
                            StartCoroutine(ReloadAmmo());
                            animator.SetTrigger("Reload");
                            //animator.SetLayerWeight(3, 1);
                            //Debug.Log("Reloaded");
                            reloadReady = false;
                            //return;
                        }
                    }
                }
            }
        }
        if (currentClip > clipSize)
        {
            currentClip = clipSize;
            ammoText.SetClip(currentClip);
        }
        //return;
        if (starterAssetsInputs.primary)
        {
            if (havePistol == true)
            {
                if (weaponSelected != 1)
                {
                    SwapWeapon(1);
                    animator.SetLayerWeight(2, 0f);
                }
            }
        }
        if (shootReady == false)
        {
            if (starterAssetsInputs.secondary)
            {
                if (haveAxe == true)
                {
                    if (weaponSelected != 2)
                    {
                        SwapWeapon(2);
                        animator.SetLayerWeight(2, 0f);
                    }
                }
            }
            if (starterAssetsInputs.thirdary)
            {
                if (haveFlashlight == true)
                {
                    if (weaponSelected != 3)
                    {
                        SwapWeapon(3);
                    }
                    animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 30f));
                }
            }
        }
        if (starterAssetsInputs.map)
        {
            //nextMap = Time.time + mapRate;
            if (mapOpened == false)
            {
                if (currentScrap == 4)
                {
                    //nextMap = Time.time + mapRate;
                    minimap.SetActive(true);
                    //mapOpened = true;
                    StartCoroutine(Mapdelay());
                }
            }
            else
            {
                //nextMap = Time.time + mapRate;
                minimap.SetActive(false);
                StartCoroutine(Mapdelayfalse());
            }
        }
        //HEALTH CONTROLLER
        if (currentHungry <= 0 && (currentWater <= 0))
        {
            //currentHealth -= Time.deltaTime / healthFallRate *2;
            //healthBar.SetHealth(currentHealth);
            healthFallRate += 1 * Time.deltaTime;
            if (healthFallRate >= 1)
            {
                currentHealth -= 2;
                healthFallRate = 0;
                healthBar.SetHealth(currentHealth);
            }
        }
        else if (currentHungry <= 0 || currentWater <= 0)
        {
            //currentHealth -= Time.deltaTime / healthFallRate;
            //healthBar.SetHealth(currentHealth);
            healthFallRate += 1 * Time.deltaTime;
            if (healthFallRate >= 1)
            {
                currentHealth -= 1;
                healthFallRate = 0;
                healthBar.SetHealth(currentHealth);
            }
        }
        if (currentHealth <= 0)
        {
            StartCoroutine(clearObject());
            animator.SetTrigger("Death");
            Destroy(playerInput);
            isDeath = true;

            currentHealth = maxHealth;
            //controller.height = 0f;
            //characterController.height = Mathf.Lerp(characterController.height, 0f , Time.deltaTime * speed);
        }
        else if (currentHealth > 100)
        {
            currentHealth = maxHealth;
        }

        //HUNGRY CONTROLLER
        if (currentHungry >= 0)
        {
            //currentHungry -= Time.deltaTime / hungryFallRate;
            hungryFallRate += 1 * Time.deltaTime;
            if (hungryFallRate >= 15)
            {
                currentHungry -= 1;
                hungryFallRate = 0;
                healthBar.SetHungry(currentHungry);
            }
        }
        else if (currentHungry <= 0)
        {
            currentHungry = 0;
            healthBar.SetHungry(currentHungry);
        }
        if (currentHungry >= maxHungry)
        {
            currentHungry = maxHungry;
            healthBar.SetHungry(currentHungry);
        }

        //WATER CONTROLLER
        if (currentWater >= 0)
        {
            waterFallRate += 1 * Time.deltaTime;
            if (waterFallRate >= 10)
            {
                currentWater -= 1;
                waterFallRate = 0;
                healthBar.SetWater(currentWater);
            }
        }
        else if (currentWater <= 0)
        {
            currentWater = 0;
            healthBar.SetWater(currentWater);
        }
        if (currentWater >= maxWater)
        {
            currentWater = maxWater;
            healthBar.SetWater(currentWater);
        }
    }

    void SwapWeapon(int weaponType)
    {
        if (weaponType == 1)
        {
            primary.SetActive(true);
            secondary.SetActive(false);
            thirdary.SetActive(false);
            uiAmmo.SetActive(true);

            weaponSelected = 1;
            //Debug.Log("WeaponChange" + weaponSelected);
        }
        if (weaponType == 2)
        {
            primary.SetActive(false);
            secondary.SetActive(true);
            thirdary.SetActive(false);
            uiAmmo.SetActive(false);

            weaponSelected = 2;
        }
        if (weaponType == 3)
        {
            primary.SetActive(false);
            secondary.SetActive(false);
            thirdary.SetActive(true);
            uiAmmo.SetActive(false);

            weaponSelected = 3;
        }
    }

    /*IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }*/
    IEnumerator Mapdelay()
    {
        yield return new WaitForSeconds(1f);
        //minimap.SetActive(false);
        mapOpened = true;
    }
    IEnumerator Mapdelayfalse()
    {
        yield return new WaitForSeconds(1f);
        //minimap.SetActive(false);
        mapOpened = false;
    }
    IEnumerator Attacked()
    {
        yield return new WaitForSeconds(0.5f);
        meleeBullet.SetActive(false);
    }
    IEnumerator ReloadAmmo()
    {
        yield return new WaitForSeconds(2f);
        int resultAmmo = maxAmmo - currentAmmo;
        if (currentClip >= resultAmmo)
        {
            currentClip = currentClip - resultAmmo;
            currentAmmo = resultAmmo + currentAmmo;
        }
        else
        {
            currentAmmo = currentClip + currentAmmo;
            currentClip = currentClip - currentClip;
        }
        //currentAmmo = maxAmmo - currentAmmo;
        //currentAmmo = maxAmmo;
        ammoText.SetMaxAmmo(currentAmmo);
        ammoText.SetClip(currentClip);
        reloadReady = true;
    }
    IEnumerator MuzzleOff(float secondUntildestroy)
    {
        yield return new WaitForSeconds(secondUntildestroy);
        muzzle.SetActive(false);
    }

    IEnumerator MeleeOff(float secondUntildestroy)
    {
        yield return new WaitForSeconds(secondUntildestroy);
        animator.SetBool("Attack", false);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == tagObject)
        {
            TakeDamage(1);
            //print("เลือด -1");
            //animator.SetTrigger("Death");
            //Destroy(collision.gameObject);
            //navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            //navAgent.isStopped = true;
            //StartCoroutine(clearObject());
        }
    }
    IEnumerator clearObject()
    {
        yield return new WaitForSeconds(3f);
        //yield return new WaitForSeconds(2.0f);
        //Destroy(this.gameObject);
        Destroy(this.gameObject);
        SceneManager.LoadScene("OverScene", LoadSceneMode.Single);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void TakeAmmo(int ammo)
    {
        currentAmmo -= ammo;
        ammoText.SetAmmo(currentAmmo);
    }

    public void GetHeal(int heal)
    {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);
    }

    public void GetClip(int clip)
    {
        currentClip += clip;
        ammoText.SetClip(currentClip);
    }

    public void GetFood(int food)
    {
        currentHungry += food;
        healthBar.SetHungry(currentHungry);
    }
    public void GetWater(int water)
    {
        currentWater += water;
        healthBar.SetWater(currentWater);
        //Debug.Log("Water = " + currentWater);
    }
    public void GetPistol()
    {
        uiWeapons.ShowPistol();
        havePistol = true;
        missionText.SetMissionIII(1, Color.green, true);
        SwapWeapon(1);
        animator.SetLayerWeight(2, 0f);
    }
    public void GetAxe()
    {
        uiWeapons.ShowAxe();
        haveAxe = true;
        missionText.SetMissionI(1, Color.green);
        SwapWeapon(2);
    }
    public void GetFlashlight()
    {
        uiWeapons.ShowFlashlight();
        haveFlashlight = true;
        missionText.SetMissionII(1, Color.green, true);
        SwapWeapon(3);
        animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 30f));
    }

    public void GetMapScrap(int scrap)
    {
        currentScrap += scrap;
        missionText.SetMissionIV(currentScrap, Color.red, false);
        if (currentScrap == 4)
        {
            missionText.SetMissionIV(currentScrap, Color.green, true);
        }
        //Debug.Log("Water = " + currentWater);
    }
}
