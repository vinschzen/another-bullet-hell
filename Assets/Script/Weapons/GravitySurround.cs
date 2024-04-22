using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class GravitySurround : BaseWeapon
{
    public float bulletSpeed = 2f;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;

    private float amount = 16f;
    public GravitySurround() : base("Gravity Surround", 10, 1f, 3f, 20f, 1)
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

            StartCoroutine(PullAfterDelay(bullet));
            
            Destroy(bullet, 8.0f);
        }

    }

    IEnumerator PullAfterDelay(GameObject bullet)
    {
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = Vector3.zero;

        float startTime = Time.time;
        while (Time.time - startTime < 5f)
        {
            
            bulletRigidbody.velocity = bulletRigidbody.transform.forward * 1.3f;
            yield return null;
        }
        bulletRigidbody.velocity = bulletRigidbody.transform.forward * -4f;
    }
}
