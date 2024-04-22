using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class SwordSurround : BaseWeapon
{
    public float bulletSpeed = 2.5f;
    public float initialDuration = 0f;
    private float increment = 0f;
    private float cycle = 0f;

    private bool lifting = false;
    public SwordSurround() : base("Sword Surround", 10, 0.1f, 6f, 60f, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    public override void Fire(Transform bulletSpawn)
    {
        if (!lifting)
        {
            StartCoroutine(LiftCamera());
            lifting = true;
        }

        bulletSpawn = GameObject.Find("Player").transform;
        Quaternion rotation = Quaternion.Euler(0f, increment * (360f/((Duration/FireRate)/3)), 0f);
        Vector3 bulletPosition = bulletSpawn.position - rotation * Vector3.forward * (20f + cycle*5.6f); 
        GameObject bullet = Instantiate(bulletPrefab, bulletPosition, rotation);
        increment++;

        if ( increment * (360f/((Duration/FireRate)/3)) >= 360 )
        {
            increment = 0f;
            cycle++;
        }

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        Destroy(bullet, 15.0f);
    }

    IEnumerator LiftCamera()
    {
        GameObject camera = GameObject.Find("Main Camera");

        float startTime = Time.time;
        while (Time.time - startTime < 6f)
        {
            camera.GetComponent<Rigidbody>().velocity = -camera.transform.forward * 5f;
            yield return null;
        }

        startTime = Time.time;
        while (Time.time - startTime < 6f)
        {
            camera.GetComponent<Rigidbody>().velocity = camera.transform.forward * 0;
            yield return null;
        }

        startTime = Time.time;
        while (Time.time - startTime < 6f)
        {
            camera.GetComponent<Rigidbody>().velocity = camera.transform.forward * 5f;
            yield return null;
        }
        camera.GetComponent<Rigidbody>().velocity = camera.transform.forward * 0;
        lifting = false;
        increment = 0f;
        cycle = 0f;
    }
}
