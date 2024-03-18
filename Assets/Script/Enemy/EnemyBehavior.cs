using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class EnemyBehavior : MonoBehaviour
{
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

    protected BaseWeapon Weapon
    {
        get { return _enemy.weapon; }
        private set { _enemy.weapon = value; }
    }

    private float _canShoot = -1f;
    private bool _dead;


    public virtual void Start() {
        _enemy.anim = GetComponent<Animator>();
        _enemy.properAudioSource = GetComponent<AudioSource>();
        _enemy.health = _enemy.health  * (SaveManager.Instance.CurrentSave.difficulty + 1);

        visualNovel = GameObject.Find("Visual Novel");
    }

    
    void OnTriggerEnter(Collider other) {}

    void DeathSequence() {}

    void PlayAudio(int index) {}

    private float nextFire = 0.0f;

    void Update()
    {
        if (!visualNovel.activeSelf)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + Weapon.FireRate;
                Weapon.Fire( _enemy.bulletSpawn );
            } 
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

    
}
