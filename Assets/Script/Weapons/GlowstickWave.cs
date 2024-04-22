using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class GlowstickWave : BaseWeapon
{
    public float bulletSpeed = 4;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private float increment = 0f;

    private float amount = 2f;

    public float waveAmplitude = 5f; 
    public float waveFrequency = 2f;  
    public GlowstickWave() : base("Glowstick Wave", 10, 0.5f, 10, 30, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    public override void Fire(Transform bulletSpawn)
    {

        for (int i = 0; i < amount; i++)
        {
            Quaternion rotation = Quaternion.Euler(0f, 90f, 90f);
            Vector3 position = bulletSpawn.position + new Vector3(-18 + i * 3, 0, -22);
            GameObject bullet = Instantiate(bulletPrefab, position, rotation);

            StartCoroutine(WaveMotion(bullet, waveAmplitude, waveFrequency));

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            Destroy(bullet, 20.0f);
        }
    }

    IEnumerator WaveMotion(GameObject bullet, float amplitude, float frequency)
    {
        while (true)
        {
            float waveOffset = amplitude * Mathf.Sin(Time.time * frequency);
            bullet.transform.position += new Vector3(0, 0, waveOffset) * Time.deltaTime;

            yield return null;
        }
    }
}
