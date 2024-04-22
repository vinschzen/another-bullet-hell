using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class EchoPulse : BaseWeapon
{
    public float bulletSpeed = 3.3f;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private int cornerIterate = 0;

    private float amount = 6f;
    private float increment = 5f;
    public EchoPulse() : base("Echo Pulse", 10, 0.3f, 30, 40, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    public override void Fire(Transform bulletSpawn)
    {
        for (int i = 0; i < amount; i++)
        {
            Quaternion rotation = Quaternion.Euler(0f, i * (360f/amount) + spinSpeed, 0f);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, rotation);

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

            Destroy(bullet, 20.0f);
        }
        spinSpeed += increment;

        if (amount > 5)
        {
            amount = amount - 2;
        }

        if (spinSpeed > 30 || spinSpeed < -30 )
        {
            increment = -increment;
            amount = amount*4;
        }

    }
}
