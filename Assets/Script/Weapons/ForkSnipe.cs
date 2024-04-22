using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class ForkSnipe : BaseWeapon
{
    public float bulletSpeed = 2f;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private float increment = 10f;

    private float amount = 5f;

    private float delay = 5f;
    private float delayCount = 0f;
    public ForkSnipe() : base("Fork Snipe", 10, 1f, 0.1f, 14.5f, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    public float waitDuration = 4.5f; 

    public override void Fire(Transform bulletSpawn)
    {
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        Vector3 initialDirectionToPlayer = (GameObject.FindGameObjectWithTag("Player").transform.position - bulletSpawn.position).normalized;

        StartCoroutine(LaunchAfterDelay(bullet, initialDirectionToPlayer));
        Destroy(bullet, 20.0f);
    }

    IEnumerator LaunchAfterDelay(GameObject bullet, Vector3 direction)
    {
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = Vector3.zero;

        float startTime = Time.time;
        while (Time.time - startTime < waitDuration)
        {
            bullet.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform); 
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
