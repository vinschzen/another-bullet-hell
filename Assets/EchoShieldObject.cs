using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoShieldObject : MonoBehaviour
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
            Destroy(other.gameObject);
        }
    }
}
