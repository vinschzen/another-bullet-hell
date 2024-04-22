using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class ScytheSwing : BaseWeapon
{
    public float bulletSpeed = 2f;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private float increment = 10f;

    private float amount = 5f;

    private float delay = 5f;
    private float delayCount = 0f;
    public ScytheSwing() : base("Scythe Swing", 10, 1f, 0.5f, 15f, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    private float waitDuration = 4.5f; 

    public override void Fire(Transform bulletSpawn)
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, rotation);
        bullet.transform.parent = bulletSpawn; 

        Vector3 initialDirectionToPlayer = (GameObject.FindGameObjectWithTag("Player").transform.position - bulletSpawn.position).normalized;

        StartCoroutine(LaunchAfterDelay(bullet, initialDirectionToPlayer));
        Destroy(bullet, 9.0f);
    }

    IEnumerator LaunchAfterDelay(GameObject bullet, Vector3 direction)
    {
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = Vector3.zero;

        float startTime = Time.time;
        while (Time.time - startTime < waitDuration)
        {
            bullet.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform); 
            bullet.transform.rotation = Quaternion.Euler(0f, bullet.transform.eulerAngles.y + 90f, 0f);
            yield return null;
        }

        startTime = Time.time;
        while (Time.time - startTime < 1.5f)
        {
            bullet.transform.rotation = Quaternion.Euler(bullet.transform.eulerAngles.x, bullet.transform.eulerAngles.y + 0.06f, bullet.transform.eulerAngles.z);
            yield return null;
        }

        startTime = Time.time;
        while (Time.time - startTime < 0.3f)
        {
            Transform camera = GameObject.Find("Main Camera").transform;
            camera.transform.rotation = Quaternion.Euler(camera.transform.eulerAngles.x, camera.transform.eulerAngles.y - 0.3f, camera.transform.eulerAngles.z);
            bullet.transform.rotation = Quaternion.Euler(bullet.transform.eulerAngles.x, bullet.transform.eulerAngles.y - 3f, bullet.transform.eulerAngles.z);
            yield return null;
        }

        startTime = Time.time;
        while (Time.time - startTime < 2.4f)
        {
            Transform camera = GameObject.Find("Main Camera").transform;
            camera.transform.rotation = Quaternion.Euler(camera.transform.eulerAngles.x, camera.transform.eulerAngles.y + 0.0375f, camera.transform.eulerAngles.z);
            yield return null;
        }


    }

}
