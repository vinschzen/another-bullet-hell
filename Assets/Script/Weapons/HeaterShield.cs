using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class HeaterShield : BaseWeapon
{
    public float initialDuration = 0f;

    public HeaterShield() : base("Heater Shield", 10, 0.1f, 0.1f, 30f, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    public float waitDuration = 4.5f; 

    public override void Fire(Transform bulletSpawn)
    {
        Vector3 bulletPosition = bulletSpawn.position + new Vector3(0, 0, -0.3f);
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletPosition, bulletSpawn.rotation);
        bullet.transform.parent = bulletSpawn; 
        

        Destroy(bullet, 5.0f);
    }

}
