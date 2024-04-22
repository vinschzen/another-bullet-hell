using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class ShieldSpiral : BaseWeapon
{
    public float bulletSpeed = 1.3f;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private int cornerIterate = 0;

    private float amount = 9f;
    private int increment = 0;
    public ShieldSpiral() : base("Shield Spiral", 10, 1f, 2.9f, 12f, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    private float waitDuration = 4.5f; 

    public override void Fire(Transform bulletSpawn)
    {
  
        Quaternion rotation = Quaternion.Euler(0f, increment * (360f/ (Duration/FireRate)), 0f);
        Vector3 position = bulletSpawn.position + new Vector3(0, 0, 0f);
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);
        bullet.transform.parent = bulletSpawn; 
        increment++;

        StartCoroutine(LaunchAfterDelay(bullet));
        Destroy(bullet, 8.0f);
    }

    IEnumerator LaunchAfterDelay(GameObject bullet)
    {
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = Vector3.zero;

        float startTime = Time.time;
        while (Time.time - startTime < 3f)
        {
            
            bulletRigidbody.velocity = bulletRigidbody.transform.forward * 0.5f;
            bullet.transform.rotation = Quaternion.Euler(0f, bullet.transform.eulerAngles.y - 2f, 0f);
            yield return null;
        }
        bulletRigidbody.velocity = bulletRigidbody.transform.forward * 0f;

        startTime = Time.time;
        while (Time.time - startTime < 1.5f)
        {
            bullet.transform.rotation = Quaternion.Euler(0f, bullet.transform.eulerAngles.y - 2f, 0f);
            // bulletRigidbody.velocity = -bulletRigidbody.transform.forward * bulletSpeed;
            yield return null;
        }

        bulletRigidbody.velocity = bulletRigidbody.transform.forward * bulletSpeed * 24.5f;
    }
}
