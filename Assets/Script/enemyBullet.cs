using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 1;

    void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject;
        var health = hit.GetComponent<PlayerController>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
