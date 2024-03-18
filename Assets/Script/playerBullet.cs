using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 1;

    void Start() {
        damage = SaveManager.Instance.CurrentSave.stats[0].value;
    }

    void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject;
        var health = hit.GetComponent<EnemyBehavior>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }

    }

}
