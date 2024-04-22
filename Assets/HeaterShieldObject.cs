using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaterShieldObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject hit = other.gameObject;

        PlayerBullet playerBullet = hit.GetComponent<PlayerBullet>();
        if (playerBullet != null)
        {
            EnemyBullet enemyBullet = hit.AddComponent<EnemyBullet>(); 

            float deflectionAngleMin = 0f;
            float deflectionAngleMax = 30f;

            float deflectionAngle = Random.Range(deflectionAngleMin, deflectionAngleMax);

            Vector3 randomRotationAxis = Random.insideUnitSphere; 
            // randomRotationAxis.y = 0f; 

            Quaternion deflectionRotation = Quaternion.Euler(randomRotationAxis * deflectionAngle);
            Vector3 deflectedDirection = deflectionRotation * -hit.transform.forward;

            hit.GetComponent<Rigidbody>().velocity = deflectedDirection * playerBullet.speed;


            Destroy(playerBullet, 5f);
        }
    }
}
