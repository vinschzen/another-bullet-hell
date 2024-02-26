using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health = 3;
    public float speed = 5.0f;
    public float acceleration = 1.0f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public float fireRate = 0.1f;
    private float nextFire = 0.0f;

    private Vector3 velocity = Vector3.zero;

    private Rigidbody rb;

    void Update()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 targetVelocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;
        velocity = Vector3.Lerp(velocity, targetVelocity, acceleration * Time.deltaTime);
        GetComponent<Rigidbody>().velocity = velocity;   

        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Fire();
        }
    }

    void Fire()
    {
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 30;
        Destroy(bullet, 2.0f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Debug.Log(other.gameObject.tag);

        // if (other.gameObject.tag == "Boundary") 
        // {
        //     rb.position = transform.position;
        // }
    }
}
