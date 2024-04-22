using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Linq;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float moveDuration = 3f;     
    public float moveMinDistance = 1f; 
    public float moveMaxDistance = 10f; 
    private float idleTimer = 0f;
    private Vector3 targetPosition;
    private GameObject limitLeft, limitRight;

    [SerializeField]
    private Enemy _enemy;
    private TextMeshProUGUI damageIndicatorText;
    private GameObject visualNovel;
    protected float Speed
    {
        get { return _enemy.speed; }
        private set { _enemy.speed = value; }
    }

    protected int Health
    {
        get { return _enemy.health; }
        private set { _enemy.health = value; }
    }

    protected List<BaseWeapon> Weapons
    {
        get { return _enemy.weapons; }
        private set { _enemy.weapons = value; }
    }

    private List<BaseWeapon> _activeWeapons = new List<BaseWeapon>();

    private float _canShoot = -1f;
    private bool _dead;
    private PlayerData data;


    public virtual void Start() {
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
        _enemy.anim = GetComponent<Animator>();
        _enemy.properAudioSource = GetComponent<AudioSource>();
        _enemy.health = _enemy.health  * (this.data.difficulty + 1 + this.data.newgameplus);

        visualNovel = GameObject.Find("Visual Novel");


        limitLeft = GameObject.Find("limitLeft");
        limitRight = GameObject.Find("limitRight");

        GenerateNewTargetPosition();
    }

    public float getHealth()
    {
        return this.Health;
    }

    
    void OnTriggerEnter(Collider other) {}

    void DeathSequence() {}

    void PlayAudio(int index) {}

    private float nextFire = 0.0f;

    void Update()
    {
        if (!visualNovel.activeSelf)
        {
            foreach (var w in Weapons)
            {
                if (Time.time > w.nextCooldown)
                {
                    w.nextCooldown = Time.time + w.Cooldown;
                    w.nextDuration = Time.time + w.Duration;
                    _activeWeapons.Add(w);
                    // Debug.Log("Added " + w.name + " at " +  Time.time);
                }
            }

            foreach (var w in _activeWeapons)
            {
                if (Time.time > w.nextDuration)
                {
                    _activeWeapons.Remove(w);
                    // Debug.Log("Removed " + w.name + " at " +  Time.time);
                }

                if (Time.time > w.nextFire)
                {
                    w.nextFire = Time.time + w.FireRate;
                    w.Fire( _enemy.bulletSpawn );
                } 
            }

            idleTimer += Time.deltaTime;

            if (idleTimer >= moveDuration)
            {
                idleTimer = 0f;
                GenerateNewTargetPosition();
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
        }

        
    }

    // void Fire()
    // {
    //     var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    //     // bullet.GetComponent<Rigidbody>().velocity = -bullet.transform.forward * 12;
        
    //     PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    //     Vector3 playerPosition = player.transform.position;

    //     Vector3 directionToPlayer = (playerPosition - bulletSpawn.position).normalized;

    //     bullet.GetComponent<Rigidbody>().velocity = directionToPlayer * 12;

    //     Destroy(bullet, 2.0f);
    // }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        indicateDamage(damage);

        var gameController = FindObjectOfType<GameController>();
        if (gameController != null)
        {
            gameController.incrementDamageDealt(damage); 
        }

        if (Health <= 0)
        {
            if (gameController != null)
            {
                gameController.incrementEnemyKilled(1); 
            }
            Destroy(gameObject);
        }
    }

    void indicateDamage(int damage) {
        GameObject canvas = GameObject.Find("Canvas");
        TextMeshProUGUI template = canvas.transform.Find("Damage Indicator Text (TMP)").GetComponent<TextMeshProUGUI>();
        damageIndicatorText = Instantiate(template , transform.position, Quaternion.identity);

        damageIndicatorText.text = damage.ToString();

        GameObject parentObject = new GameObject("Damage Indicator Parent");
        damageIndicatorText.transform.SetParent(parentObject.transform);
        parentObject.AddComponent<DamageIndicator>();
        parentObject.transform.SetParent(GameObject.Find("Canvas").transform);

        parentObject.transform.localRotation = Quaternion.identity;
        parentObject.transform.localScale = Vector3.one;
        parentObject.transform.position = transform.position;
        damageIndicatorText.transform.localRotation = Quaternion.identity;
        damageIndicatorText.transform.localScale = new Vector3(3, 3, 3);
    }

    void GenerateNewTargetPosition()
    {
        float leftOrRight = Random.Range(0,2);
        float distance = Random.Range(moveMinDistance, moveMaxDistance);
        float randomX = 0;
        switch (leftOrRight)
        {
            case 0:
                randomX = -distance; 
                break;
            case 1:
                randomX = distance;
                break;
            default:
                break;
        }

        // print("left or right : " + leftOrRight);

        // print("distance : " + randomX);

        targetPosition = new Vector3(randomX, transform.position.y, transform.position.z);
    }

    
}
