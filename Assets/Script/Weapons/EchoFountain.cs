using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class EchoFountain : BaseWeapon
{
    public float bulletSpeed = 4;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private float increment = 10f;

    private float amount = 4f;
    private List<Transform> corners = new List<Transform>();
    public EchoFountain() : base("Echo Fountain", 10, 0.5f, 2f, 40, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
        corners.Add( GameObject.Find("Corner TopRight").transform );
        corners.Add( GameObject.Find("Corner BotRight").transform );
        corners.Add( GameObject.Find("Corner BotLeft").transform );
        corners.Add( GameObject.Find("Corner TopLeft").transform );
    }
    public override void Fire(Transform bulletSpawn)
    {
        foreach (var corner in corners)
        {
            bulletSpawn = corner;
            for (int i = 0; i < amount; i++)
            {
                Quaternion rotation = Quaternion.Euler(0f, i * (360f/amount) + spinSpeed, 0f);
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, rotation);

                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

                Destroy(bullet, 20.0f);
            }
            spinSpeed += increment;
        }
    }
}
