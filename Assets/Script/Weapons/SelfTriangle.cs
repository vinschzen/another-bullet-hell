using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class SelfTriangle : BaseWeapon
{
    public float bulletSpeed = 3.3f;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    public SelfTriangle() : base("Self Triangle", 10, 0.5f, 4.5f, 10f, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    public override void Fire(Transform bulletSpawn)
    {
        const int trianglePoints = 3; 
        float angleOffset = 360f / trianglePoints; 
        for (int i = 0; i < trianglePoints; i++)
        {
            Quaternion rotation = Quaternion.Euler(0f, i * angleOffset + spinSpeed, 0f);
            Vector3 position = bulletSpawn.position + new Vector3(0, 0, 0);
            GameObject bullet = Instantiate(bulletPrefab, position, rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            Destroy(bullet, 20.0f);
        }
    }
}
