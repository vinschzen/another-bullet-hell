using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class SideBomb : BaseWeapon
{
    public float bulletSpeed = 2;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private float increment = 10f;
    private List<Transform> corners = new List<Transform>();
    private int cornerIterate = 0;

    private float amount = 12;
    public SideBomb() : base("Side Bomb", 10, 0.8f, 2.4f, 32, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
        corners.Add( GameObject.Find("Corner TopRight").transform );
        corners.Add( GameObject.Find("Corner TopLeft").transform );
    }
    public override void Fire(Transform bulletSpawn)
    {
        if ( Random.Range(0,2) == 1) corners.Reverse();

        foreach (var side in corners)
        {
            for (int i = 0; i < amount; i++)
            {
                Quaternion rotation = Quaternion.Euler(0f, i * (360f/amount) + spinSpeed, 0f);
                GameObject bullet = Instantiate(bulletPrefab, side.position, rotation);
                
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

                Destroy(bullet, 20.0f);
            }
        }

        

    }
}
