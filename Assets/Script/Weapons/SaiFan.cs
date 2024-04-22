using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class SaiFan : BaseWeapon
{
    public float bulletSpeed = 6f;
    public float spinSpeed = 5f;
    public float initialDuration = 0;
    private float increment = 3f;
    private float offset = 1.5f;
    public float amount = 7;
    public SaiFan() : base("Sai Fans", 10, 2f, 3, 25, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    public override void Fire(Transform bulletSpawn)
    {
        int halfAmount = (int) Math.Floor(amount/2);
        for (int i = 0; i < amount; i++)
        {
            
            PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            Vector3 playerPosition = player.transform.position;
            Vector3 directionToPlayer = (playerPosition - bulletSpawn.position).normalized;

            Quaternion playerRotation = Quaternion.LookRotation(directionToPlayer);
            

            Quaternion rotation = Quaternion.Euler(0f, playerRotation.eulerAngles.y + 30f - (i * (75f/amount)), 0f);
            

            Vector3 position = bulletSpawn.position + new Vector3(-halfAmount + i, 0, Math.Abs(halfAmount-i));
            GameObject bullet = Instantiate(bulletPrefab, position, rotation);

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

            Destroy(bullet, 8.0f);
        }
    }
}
