using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBombController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if (other.gameObject.tag == "EnemyBullet")
        {
            Debug.Log("Collided with Bullet");
            Destroy(other.gameObject);
        }
    }
}
