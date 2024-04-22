using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyWaveBombController : MonoBehaviour
{
    public bool activate = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(100, transform.localScale.y, 100), 20 * Time.deltaTime);
            if (transform.localScale.x >= 30) {
                Destroy(transform);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Collided");
        Component component = other.gameObject.GetComponent<PlayerBullet>();
        if (component)
        {
            Destroy(other.gameObject);
        }
    }
}
