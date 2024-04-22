using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset inputActionAsset;
    public int health = 3;
    public float speed = 5.0f;
    public float acceleration = 1.0f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float fireRate = 0.1f;
    public GameObject pauseScreen;
    private int selectedSkill = 0;
    private PlayerData data;
    private float nextFire = 0.0f;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;

    private GameObject waveBombReference;

    private GameObject waveBomb;

    private GameObject superbulletReference;

    private GameObject superbullet;
    private GameObject shieldReference;
    private GameObject shield;

    private GameObject visualNovel;
    private bool isPaused = false;
    private HealthBar hpbar;
    private EnergyBar enbar;

    private int bullletCount;

    private GameObject victoryScreen;



    void Start() {
        try
        {
            this.data = SaveManager.Instance.CurrentSave;
        }
        catch (System.Exception)
        {
            string path = Application.persistentDataPath + "/playerData_1.json";
            string json = File.ReadAllText(path);
            this.data = JsonUtility.FromJson<PlayerData>(json);
        }
        this.hpbar = FindObjectOfType<HealthBar>();
        this.enbar = FindObjectOfType<EnergyBar>();
        this.bullletCount = this.data.stats[1].value;
        this.health = this.data.stats[2].value;
        this.visualNovel = GameObject.Find("Visual Novel");
        this.waveBombReference = GameObject.Find("Wave Bomb");
        this.superbulletReference = GameObject.Find("Superbullet");
        this.shieldReference = GameObject.Find("Shield");

        this.victoryScreen = GameObject.Find("Victory Screen");
    }

    void Update()
    {   
        if (!visualNovel.activeSelf) {
            Vector3 newMove = inputActionAsset["Movement"].ReadValue<Vector3>();

            Vector3 targetVelocity = newMove * speed;
            velocity = Vector3.Lerp(velocity, targetVelocity, acceleration * Time.deltaTime);
            GetComponent<Rigidbody>().velocity = velocity;

            if (inputActionAsset["Fire"].IsPressed() && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                FireBullet();
            }

            if (selectedSkill == 0)
            {
                if (waveBomb == null)
                {
                    if (inputActionAsset["UseSkill"].IsPressed())
                    {

                        if (enbar.CheckEnergy())
                        {
                            enbar.EmptyBar();
                            waveBomb = Instantiate(waveBombReference, this.transform.position, this.transform.rotation);
                            waveBomb.GetComponent<Collider>().isTrigger = true;
                        }
                        else {
                            enbar.flashRed();
                        }
                    }
                }
            }
            else if (selectedSkill == 1)
            {
                if (superbullet == null)
                {
                    if (inputActionAsset["UseSkill"].IsPressed())
                    {

                        if (enbar.CheckEnergy())
                        {
                            enbar.EmptyBar();
                            FireSuperbullet();
                        }
                        else {
                            enbar.flashRed();
                        }
                    }
                }
            }
            else if (selectedSkill == 2)
            {
                if (shield == null)
                {
                    if (inputActionAsset["UseSkill"].IsPressed())
                    {

                        if (enbar.CheckEnergy())
                        {
                            enbar.EmptyBar();
                            shield = (GameObject) Instantiate(shieldReference, bulletSpawn.position, bulletSpawn.rotation);
                            shield.transform.parent = transform; 
                            shield.transform.localPosition = Vector3.zero;
                            StartCoroutine(ActivateShieldForTime());
                        }
                        else {
                            enbar.flashRed();
                        }
                    }
                }
            }
        }        

        if (waveBomb)
        {
            waveBomb.transform.localScale = Vector3.MoveTowards(waveBomb.transform.localScale, new Vector3(100, waveBomb.transform.localScale.y, 100), 20 * Time.deltaTime);
            Collider[] colliders = Physics.OverlapSphere(waveBomb.transform.position, 0.1f, LayerMask.GetMask("Default"));
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.tag == "EnemyBullet")
                {
                    Debug.Log("Enemy Bullet Deleted");
                    Destroy(collider.gameObject);
                    break;
                }
            }

            if (waveBomb.transform.localScale.x >= 30) {
                Destroy(waveBomb);
            }
        }
    }

    void switchSkill()
    {
        this.selectedSkill = (selectedSkill+1)%3;
        var selectedSkillDisplay = GameObject.Find("Selected Skill Text (TMP)").GetComponent<TextMeshProUGUI>();
        switch (selectedSkill)
        {
            case 0:
                selectedSkillDisplay.text = "Skill : << Wave Bomb >>";
                break;
            case 1:
                selectedSkillDisplay.text = "Skill : << Superbullet >>";
                break;
            case 2:
                selectedSkillDisplay.text = "Skill : << Forcefield >>";
                break;
            default:
                selectedSkillDisplay.text = "Skill : << Wave Bomb >>";
                break;
        }
    }
    void FireBullet()
    {
        for (int i = 0; i < bullletCount; i++)
        {
            Vector3 position = bulletSpawn.position + new Vector3(0 - (bullletCount/2) + i, 0, 0);
            if (bullletCount%2 == 0)  position = position + new Vector3(0.5f, 0, 0);
            var bullet = (GameObject) Instantiate(bulletPrefab, position, bulletSpawn.rotation);
            var speed = bullet.GetComponent<PlayerBullet>().speed;
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * speed;
            Destroy(bullet, 2.0f);
        }
        
    }

    void FireSuperbullet()
    {
        superbullet = (GameObject) Instantiate(superbulletReference, bulletSpawn.position, bulletSpawn.rotation);
        var speed = superbullet.GetComponent<PlayerBullet>().speed;
        superbullet.GetComponent<Rigidbody>().velocity = superbullet.transform.forward * speed;
        Destroy(superbullet, 5.0f);
    }

    public void TakeDamage(int damage)
    {
        if (!victoryScreen.activeSelf)
        {
            if (shield == null)
            {
                health -= damage;

                var gameController = FindObjectOfType<GameController>();
                if (gameController != null)
                {
                    gameController.incrementDamageTaken(damage); 
                }

                hpbar.DecrementHealth();

                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    IEnumerator ActivateShieldForTime()
    {
        yield return new WaitForSeconds(3); 
        Destroy(shield);
    }

    void OnEnable()
    {
        inputActionAsset["Pause"].started += OnPausePress;

        inputActionAsset["SwitchSkill"].started += OnSwitchPress;
    }

    void OnDisable()
    {
        inputActionAsset["Pause"].started -= OnPausePress;

        inputActionAsset["SwitchSkill"].started -= OnSwitchPress;
    }

    void OnPausePress(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f; 
        pauseScreen.SetActive(isPaused);
    }

    void OnSwitchPress(InputAction.CallbackContext context)
    {
        switchSkill();
    }

}
