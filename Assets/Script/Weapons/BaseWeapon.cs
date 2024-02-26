using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public string Name { get; set; }
    public int Damage { get; set; }
    public float FireRate { get; set; }
    public GameObject bulletPrefab;

    public BaseWeapon(string name, int damage, float firerate)
    {
        Name = name;
        Damage = damage;
        FireRate = firerate;
    }

    public abstract void  Fire(Transform bulletSpawn);
}
