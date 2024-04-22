using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class WorldStar : BaseWeapon
{
    public float bulletSpeed = 4;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private float increment = 0f;

    private float amount = 5f;
    public WorldStar() : base("World Star", 10, 0.1f, 10, 40, 1)
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

            if (increment > 5)
            {
                Quaternion rotation1 = Quaternion.Euler(0f, i * (360f/amount) - increment, 0f);
                GameObject bullet1 = Instantiate(bulletPrefab, bulletSpawn.position, rotation1);

                bullet1.GetComponent<Rigidbody>().velocity = bullet1.transform.forward * bulletSpeed;

                Destroy(bullet1, 20.0f);

                
                Quaternion rotation2 = Quaternion.Euler(0f, i * (360f/amount) + increment, 0f);
                GameObject bullet2 = Instantiate(bulletPrefab, bulletSpawn.position, rotation2);

                bullet2.GetComponent<Rigidbody>().velocity = bullet2.transform.forward * bulletSpeed;

                Destroy(bullet2, 20.0f);
            }
            else {
                Quaternion rotation = Quaternion.Euler(0f, i * (360f/amount), 0f);
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, rotation);

                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

                Destroy(bullet, 20.0f);
            }
        }


        if (increment > 20)
        {
            increment = 0;
        }
        increment += 5;
        
    }
}
