using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinFountain : BaseWeapon
{
    public float bulletSpeed = 30f;
    public float spinSpeed = 20f; 
    public SpinFountain() : base("Spin Fountain", 10, 1f)
    {
    }
    public override void Fire(Transform bulletSpawn)
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion rotation = Quaternion.Euler(0f, i * 90f, 0f); // Rotate by 90 degrees
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, rotation);

            // Set bullet velocity
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

            // Apply clockwise spin
            bullet.GetComponent<Rigidbody>().angularVelocity = Vector3.up * spinSpeed;
            
            // Destroy the bullet after a delay
            Destroy(bullet, 2.0f);
        }
    }
}
