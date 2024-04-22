using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBombController : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Collided");
        Component component = other.gameObject.GetComponent<EnemyBullet>();
        if (component)
        {
            if (other.gameObject.tag == "EnemyBullet")
            {
                Destroy(other.gameObject);

            }
        }
    }
}
