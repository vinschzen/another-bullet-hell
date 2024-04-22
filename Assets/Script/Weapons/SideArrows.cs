using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class SideArrows : BaseWeapon
{
    public float bulletSpeed = 4f;
    public float spinSpeed = 5f;
    public float initialDuration = 0f;
    private float increment = 3f;
    private float offset = 1.5f;
    private float amount = 13;

    float accelerationDuration = 0.4f; // Example duration for acceleration

    public SideArrows() : base("Side Rain", 10, 15f, 15, 30, 1)
    {
    }
    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    public override void Fire(Transform bulletSpawn)
    {


        switch (Random.Range(0,2))
        {
            case 0:
            
                bulletSpawn = GameObject.Find("Corner TopLeft").transform;
                for (int i = 0; i < amount; i++)
                {
                    Quaternion rotation = Quaternion.Euler(0f, -90f, 0f);
                    Vector3 position = bulletSpawn.position + new Vector3(0, 0, -16 + offset + i*3);
                    GameObject bullet = Instantiate(bulletPrefab, position, rotation);

                    bullet.GetComponent<Rigidbody>().AddForce(-bullet.transform.forward * bulletSpeed / accelerationDuration, ForceMode.VelocityChange);

                    StartCoroutine(DeccelerateBullet(bullet, bulletSpeed));

                    Destroy(bullet, 20.0f);
                }
                break;
            default:
                bulletSpawn = GameObject.Find("Corner TopRight").transform;
                for (int i = 0; i < amount; i++)
                {
                    Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
                    Vector3 position = bulletSpawn.position + new Vector3(0, 0, -16 + offset + i*3);
                    GameObject bullet = Instantiate(bulletPrefab, position, rotation);

                    bullet.GetComponent<Rigidbody>().AddForce(-bullet.transform.forward * bulletSpeed / accelerationDuration, ForceMode.VelocityChange);

                    StartCoroutine(DeccelerateBullet(bullet, bulletSpeed));

                    Destroy(bullet, 20.0f);
                }
                break;
        }

        

    }

    IEnumerator DeccelerateBullet(GameObject bullet, float initialSpeed)
    {
        yield return new WaitForSeconds(accelerationDuration); 
        while (bullet.GetComponent<Rigidbody>().velocity.magnitude > 12.5f)
        {
            bullet.GetComponent<Rigidbody>().AddForce(bullet.GetComponent<Rigidbody>().velocity.normalized * -2f, ForceMode.VelocityChange);
            yield return null;
        }
    }
}
