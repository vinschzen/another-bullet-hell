using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private Enemy _enemy;
    protected float Speed
    {
        get { return _enemy.speed; }
        private set { _enemy.speed = value; }
    }

    protected int Health
    {
        get { return _enemy.health; }
        private set { _enemy.health = value ; }
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
    }

    
    void OnTriggerEnter(Collider other) {}

    void DeathSequence() {}

    void PlayAudio(int index) {}

    private float nextFire = 0.0f;

    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + Weapon.FireRate;
            Weapon.Fire( _enemy.bulletSpawn );
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

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    
}
