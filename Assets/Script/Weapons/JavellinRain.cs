using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class JavelinRain : BaseWeapon
{
    public float bulletSpeed = 4f;
    public float spinSpeed = 5f;
    public float initialDuration = 0f;
    private float increment = 3f;
    private float offset = 1.5f;
    private float amount = 13;
    public JavelinRain() : base("Javelin Rain", 10, 2f, 12f, 55, 1)
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
            Quaternion rotation = Quaternion.Euler(0f, 180f, 0f);
            Vector3 position = bulletSpawn.position + new Vector3(-16 + offset + i*3, 0, 0);
            GameObject bullet = Instantiate(bulletPrefab, position, rotation);

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

            Destroy(bullet, 20.0f);
        }

        increment--;
        if (increment < 0) 
        {
            increment = 3;
            offset = (offset - 1.5f) % 3;
        }

    }
}
