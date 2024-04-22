using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class SingleShot : BaseWeapon
{
    public SingleShot() : base("Single Shot", 1, 1f, 1, 1, 1)
    {
    }
    public override void Fire(Transform bulletSpawn)
    {
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Vector3 playerPosition = player.transform.position;

        Vector3 directionToPlayer = (playerPosition - bulletSpawn.position).normalized;

        bullet.GetComponent<Rigidbody>().velocity = directionToPlayer * 12;

        Destroy(bullet, 2.0f);
    }
}
