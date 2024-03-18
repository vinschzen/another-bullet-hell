using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public int health = 3;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float fireRate = 1.0f;
    private float nextFire = 0.0f;
    private TextMeshProUGUI damageIndicatorText; 

    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Fire();
        }
    }

    void Fire()
    {
        var bullet = (GameObject) Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = -bullet.transform.forward * 12;
        Destroy(bullet, 2.0f);
    }

    void indicateDamage(int damage) {
        damageIndicatorText = Instantiate(Resources.Load<TextMeshProUGUI>("TextPrefab"), transform.position, Quaternion.identity);
        damageIndicatorText.text = damage.ToString();
        damageIndicatorText.transform.SetParent(GameObject.Find("Canvas").transform);

        // Optional: Set other text properties (color, font size, etc.)
        // temporaryText.color = Color.red;

        StartCoroutine(showIndicator());
    }

    IEnumerator showIndicator()
    {
        float duration = 10f;
        float moveSpeed = 0.1f;
        float elapsedTime = 0.0f;
        Vector3 startPos = damageIndicatorText.transform.position;
        Vector3 endPos = startPos + Vector3.up * (duration * moveSpeed);

        while (elapsedTime < duration)
        {
            damageIndicatorText.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(damageIndicatorText.gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        indicateDamage(damage);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
