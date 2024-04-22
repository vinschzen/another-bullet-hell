using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class EchoShot : BaseWeapon
{
    public float bulletSpeed = 30f;
    public float spinSpeed = 5f;
    public float initialDuration = 0;
    private float increment = 3f;
    private float offset = 1.5f;
    private float amount = 5;
    public EchoShot() : base("Echo Shot", 10, 0.1f, 5, 12, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    public override void Fire(Transform bulletSpawn)
    {
        Vector3 position = bulletSpawn.position + new Vector3(0, 0, 0);
        Quaternion rotation = Quaternion.Euler(0f, -180f, 0f);
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        Destroy(bullet, 8.0f);
        
    }
}