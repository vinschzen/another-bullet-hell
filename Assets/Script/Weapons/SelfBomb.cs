using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class SelfBomb : BaseWeapon
{
    public float bulletSpeed = 1.3f;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private int cornerIterate = 0;

    private float amount = 9f;
    public SelfBomb() : base("Self Bomb", 10, 1f, 3f, 50, 1)
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
            Quaternion rotation = Quaternion.Euler(0f, i * (360f/amount), 0f);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, rotation);
            

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            bullet.GetComponent<Rigidbody>().rotation = Quaternion.EulerAngles(0, 0, 0);
            

            Destroy(bullet, 20.0f);
        }

    }
}
