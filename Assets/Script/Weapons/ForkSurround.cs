using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class ForkSurround : BaseWeapon
{
    public float bulletSpeed = 2f;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private float increment = 10f;

    private float amount = 5f;
    public ForkSurround() : base("Fork Surround", 10, 0.2f, 0.1f, 12f, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    public override void Fire(Transform bulletSpawn)
    {
        bulletSpawn = GameObject.Find("Player").transform;

        for (int i = 0; i < amount; i++)
        {
            Quaternion rotation = Quaternion.Euler(0f, i * (360f/amount) + spinSpeed, 0f);
            Vector3 bulletPosition = bulletSpawn.position - rotation * Vector3.forward * 20; 
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, rotation);

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

            Destroy(bullet, 20.0f);
        }
        spinSpeed += increment;

    }
}
