using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class HeartWeb : BaseWeapon
{
    public float bulletSpeed = 4;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private float increment = 10f;

    private float amount = 4f;
    public HeartWeb() : base("Heart Web", 10, 0.1f, 3f, 20, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    public override void Fire(Transform bulletSpawn)
    {
        const int heartPoints = 30; 
        const float heartRadius = 1f; 
        const float heartDepth = 0.5f; 

        for (int i = 0; i < heartPoints; i++)
        {
            float angle = Mathf.PI * 2 * i / (heartPoints - 1); 

            float x = Mathf.Cos(angle) * heartRadius * (1f + Mathf.Abs(Mathf.Sin(angle)));
            float z = Mathf.Sin(angle) * heartRadius * (1 - Mathf.Abs(Mathf.Cos(angle)));

            Vector3 bulletPosition = bulletSpawn.position + new Vector3(x, 0, z + heartDepth);

            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);

            bullet.transform.LookAt(bulletSpawn.position + new Vector3(0, 0, heartDepth));

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            Destroy(bullet, 20.0f);
        }
    }
}
