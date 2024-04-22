using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class HeartFountain : BaseWeapon
{
    public float bulletSpeed = 2;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private float increment = 10f;

    private float amount = 4f;
    public HeartFountain() : base("Heart Fountain", 10, 1.3f, 10f, 12, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    public override void Fire(Transform bulletSpawn)
    {
        const int bulletPoints = 25;
        const float scale = 1f; 

        for (int i = 0; i < bulletPoints; i++)
        {
            float theta = Mathf.PI * 2 * i / (bulletPoints - 1); 

            float r = 2f - 2f * Mathf.Sin(theta) + (Mathf.Sin(theta) * Mathf.Sqrt(Mathf.Cos(theta))) / (Mathf.Sin(theta) + 1.4f);
            float x = r * Mathf.Cos(theta) * scale;
            float z = r * Mathf.Sin(theta) * scale;

            Vector3 bulletPosition = bulletSpawn.position + new Vector3(x, 0f, z);

            Quaternion rotation = Quaternion.Euler(0f, 450f - i * (360f/bulletPoints), 0f);
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            Destroy(bullet, 12.0f);
        }

        for (int i = 0; i < bulletPoints; i++)
        {
            float theta = Mathf.PI * 2 * i / (bulletPoints - 1); 

            float r = 2f - 2f * Mathf.Sin(theta) + (Mathf.Sin(theta) * Mathf.Sqrt(Mathf.Cos(theta))) / (Mathf.Sin(theta) + 1.4f);
            float x = -r * Mathf.Cos(theta) * scale;
            float z = r * Mathf.Sin(theta) * scale;

            Vector3 bulletPosition = bulletSpawn.position + new Vector3(x, 0f, z);

            Quaternion rotation = Quaternion.Euler(0f, i * (360f/bulletPoints) - 90f, 0f);
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            Destroy(bullet, 12.0f);
        }


    }
}
