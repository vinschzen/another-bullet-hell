using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class SwordSpiral : BaseWeapon
{
    public float bulletSpeed = 2f;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;

    private int amount = 5;
    private int increment = 0;

    private bool swinging = false;
    public SwordSpiral() : base("Sword Spiral", 10, 0.2f, 3f, 15f, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    private float waitDuration = 4.5f; 

    public override void Fire(Transform bulletSpawn)
    {
        // for (int i = 0; i < amount; i++)
        // {
            Quaternion rotation = Quaternion.Euler(0f, increment * (360f/ (Duration/FireRate)), 0f);
            Vector3 position = bulletSpawn.position + new Vector3(0, 0, 0);
            GameObject bullet = Instantiate(bulletPrefab, position, rotation);
            increment++;

            // bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        // }

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
            yield return null;
        }

         startTime = Time.time;
        while (Time.time - startTime < 1.5f)
        {
            bulletRigidbody.velocity = -bulletRigidbody.transform.forward * bulletSpeed ;
            yield return null;
        }

        bulletRigidbody.velocity = bulletRigidbody.transform.forward * bulletSpeed * 24.5f;
    }

}
