using System.Collections;
using System.Collections.Generic;
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
    private float nextFire = 0.0f;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;

    private GameObject waveBombReference;

    private GameObject waveBomb;

    private GameObject visualNovel;
    private bool isPaused = false;
    private HealthBar hpbar;
    private EnergyBar enbar;



    void Start() {
        this.hpbar = FindObjectOfType<HealthBar>();
        this.enbar = FindObjectOfType<EnergyBar>();
        this.health = SaveManager.Instance.CurrentSave.stats[2].value;
        this.visualNovel = GameObject.Find("Visual Novel");
        this.waveBombReference = GameObject.Find("Wave Bomb");
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
                Fire();
            }

            if (inputActionAsset["Pause"].IsPressed())
            {
                isPaused = !isPaused; 

                Time.timeScale = isPaused ? 0f : 1f; 
                pauseScreen.SetActive(isPaused);
            } 

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
            else
            {
                waveBomb.transform.localScale = Vector3.MoveTowards(waveBomb.transform.localScale, new Vector3(100, waveBomb.transform.localScale.y, 100), 20 * Time.deltaTime);
                Collider[] colliders = Physics.OverlapSphere(waveBomb.transform.position, 0.1f, LayerMask.GetMask("Default"));
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.tag == "EnemyBullet")
                    {
                        Debug.Log("Enemy Bullet Deleted");
                        Destroy(collider.gameObject);
                        break; // Destroy only the first bullet we find
                    }
                }

                if (waveBomb.transform.localScale.x >= 30) {
                    Destroy(waveBomb);
                }
            }

        }

               
        
    }
    void Fire()
    {
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 30;
        Destroy(bullet, 2.0f);
    }

    public void TakeDamage(int damage)
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
