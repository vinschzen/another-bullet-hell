using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class SideSwords : BaseWeapon
{
    public float bulletSpeed = 0f;
    public float spinSpeed = 0f;
    public float initialDuration = 0f;
    private float increment = 3f;
    private float offset = 1.5f;
    private float amount = 13;

    private float delay = 0f;

    float accelerationDuration = 0.4f; // Example duration for acceleration

    public SideSwords() : base("Side Swords", 10, 15, 15, 30, 1)
    {
    }
    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    public override void Fire(Transform bulletSpawn)
    {

        float delay = 0f;

        bulletSpawn = GameObject.Find("Corner TopLeft").transform;
        for (int i = 0; i < amount; i++)
        {
            Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
            Vector3 position = bulletSpawn.position + new Vector3(0, 0, -16 + offset + i*3);
            GameObject bullet = Instantiate(bulletPrefab, position, rotation);

            StartCoroutine(ExtendSwords(bullet, delay));

            Destroy(bullet, 10.0f);
        }
        

        delay = 0.6f;

        bulletSpawn = GameObject.Find("Corner TopRight").transform;
        for (int i = 0; i < amount; i++)
        {
            Quaternion rotation = Quaternion.Euler(0f, -90f, 0f);
            Vector3 position = bulletSpawn.position + new Vector3(0, 0, -16 + offset + i*3);
            GameObject bullet = Instantiate(bulletPrefab, position, rotation);

            StartCoroutine(ExtendSwords(bullet, delay));

            Destroy(bullet, 10.0f);
        }


        
    }

    IEnumerator ExtendSwords(GameObject bullet, float delay)
    {
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = Vector3.zero;

        float startTime = Time.time;
        while (Time.time - startTime < delay)
        {
            yield return null;
        }

        startTime = Time.time;
        while (Time.time - startTime < 0.5f)
        {
            bulletRigidbody.velocity = bulletRigidbody.transform.forward * 23f;
            yield return null;
        }

        startTime = Time.time;
        while (Time.time - startTime < 3f)
        {
            
            bulletRigidbody.velocity = bulletRigidbody.transform.forward * 0f;
            yield return null;
        }

        startTime = Time.time;
        while (Time.time - startTime < 5f)
        {
            
            bulletRigidbody.velocity = -bulletRigidbody.transform.forward * 12f;
            yield return null;
        }
    }
}
