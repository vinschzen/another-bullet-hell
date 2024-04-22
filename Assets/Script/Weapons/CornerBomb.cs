using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class CornerBomb : BaseWeapon
{
    public float bulletSpeed = 2;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private float increment = 10f;
    private List<Transform> corners = new List<Transform>();
    private int cornerIterate = 0;

    private float amount = 7f;
    public CornerBomb() : base("Corner Bomb", 10, 0.2f, 0.8f, 50, 1)
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
        if ( nextDuration == 0 && Random.Range(0,2 ) == 1) corners.Reverse();
        bulletSpawn = corners[cornerIterate];

        for (int i = 0; i < amount; i++)
        {
            Quaternion rotation = Quaternion.Euler(0f, i * (360f/amount) + spinSpeed, 0f);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, rotation);
            

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            bullet.GetComponent<Rigidbody>().rotation = Quaternion.EulerAngles(0, 0, 0);
            

            Destroy(bullet, 20.0f);
        }
        cornerIterate = (cornerIterate + 1) % 4;

    }
}
