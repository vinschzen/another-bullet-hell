using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class SwordBomb : BaseWeapon
{
    public float bulletSpeed = 1.3f;
    public float spinSpeed = 0f;
    public float initialDuration = 0f;
    private int cornerIterate = 0;

    private float amount = 9f;
    public SwordBomb() : base("Sword Bomb", 10, 0.75f, 6f, 8.5f, 1)
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
            
            Destroy(bullet, 12.0f);
        }
        spinSpeed = (spinSpeed + 5f)%15;

    }
}
