using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public string Name { get; set; }
    public int Damage { get; set; }
    public float FireRate { get; set; }
    public float Duration { get; set; }
    public float nextDuration {get; set; }
    public float Cooldown { get; set; }
    public float nextCooldown {get; set; }
    public float Odds { get; set; }

    public float nextFire {get; set; }
    public GameObject bulletPrefab;

    public BaseWeapon(string name, int damage, float firerate, float duration, float cooldown, float odds)
    {
        Name = name;
        Damage = damage;
        FireRate = firerate;
        Duration = duration;
        Cooldown = cooldown;
        Odds = odds;
        nextFire = 0f;
        nextCooldown = 0f;
        nextDuration = 0f;
    }

    public abstract void  Fire(Transform bulletSpawn);
}
