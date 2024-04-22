using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class DemonBomb : BaseWeapon
{
    public float bulletSpeed = 4.5f;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private int cornerIterate = 0;

    private float amount = 7f;
    public DemonBomb() : base("Demon Bomb", 10, 2f, 1f, 15f, 1)
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
            bullet.GetComponent<Rigidbody>().rotation = Quaternion.EulerAngles(0, 0, 0);
            
            Destroy(bullet, 20.0f);
        }
        cornerIterate = (cornerIterate + 1) % 4;

    }
}
