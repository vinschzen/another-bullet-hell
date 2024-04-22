using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class RingOfFire : BaseWeapon
{
    public float bulletSpeed = 2f;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private float increment = 10f;

    private float amount = 25f;
    public RingOfFire() : base("Ring of Fire", 10, 0.2f, 0.1f, 32.5f, 1)
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
            Quaternion rotation = Quaternion.Euler(0f, i * (360f/amount), 0f);
            Vector3 bulletPosition = bulletSpawn.position - rotation * Vector3.forward * 10; 
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, rotation);

            Vector3 orbitAxis = Vector3.up; // Assuming vertical axis for orbit
            Vector3 orbitCenter = bulletSpawn.position;
            Vector3 velocity = Vector3.Cross(orbitAxis, bulletPosition - orbitCenter).normalized * bulletSpeed;

            bullet.GetComponent<Rigidbody>().velocity = velocity;

            Destroy(bullet, 10.0f);
        }
        spinSpeed += increment;

    }
}
