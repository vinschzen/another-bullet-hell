using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class ScytheSides : BaseWeapon
{
    public float bulletSpeed = 4;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private float increment = 10f;

    private float amount = 4f;
    private List<Transform> corners = new List<Transform>();
    public ScytheSides() : base("Scythe Sides", 10, 3f, 12f, 40, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
        corners.Add( GameObject.Find("Corner TopRight").transform );
        corners.Add( GameObject.Find("Corner TopLeft").transform );
    }

    private int size = 0;
    public override void Fire(Transform bulletSpawn)
    {
        int mirrorX = 180;
        foreach (var corner in corners)
        {
            mirrorX -= 180;
            bulletSpawn = corner;

            Quaternion rotation = Quaternion.Euler(mirrorX, -90, 0f);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, rotation);
            bullet.transform.localScale = new Vector3(80 + (size*10), 80 + (size*10), 80 + (size*10));

            bullet.GetComponent<Rigidbody>().velocity = -bullet.transform.right * bulletSpeed;

            Destroy(bullet, 20.0f);
            
        }

        size = (size + 1) % 4;
    }
}
