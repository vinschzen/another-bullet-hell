using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damageScaling = 1;
    public int speed = 30;
    public bool destroyOnHit = true;
    private int damage;

    private PlayerData data;    

    void Start() {
        try
        {
            this.data = SaveManager.Instance.CurrentSave;
        }
        catch (System.Exception)
        {
            string path = Application.persistentDataPath + "/playerData_1.json";
            string json = File.ReadAllText(path);
            this.data = JsonUtility.FromJson<PlayerData>(json);
        }
        damage = data.stats[0].value * damageScaling;
    }

    void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject;
        var health = hit.GetComponent<EnemyBehavior>();
        if (health != null)
        {
            health.TakeDamage(damage);
            if (destroyOnHit)
            {
                Destroy(gameObject);
            }
        }

    }

}
