using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class Teleport : BaseWeapon
{
    public float bulletSpeed = 1.3f;
    public float spinSpeed = 0f;
    public float initialDuration = 0f;
    private int cornerIterate = 0;

    private float amount = 9f;

    private Transform limitLeft;
    private Transform limitRight;
    private Light lights;
    public Teleport() : base("Teleport", 10, 5f, 5f, 15f, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
        this.lights = GameObject.Find("Directional Light (1)").GetComponent<Light>();
    }
    public override void Fire(Transform bulletSpawn)
    {
        Vector3 currentPosition = transform.position;
        float randomOffset = Mathf.Abs(Random.Range(-10f, 10f));
        Vector3 newPosition = currentPosition + new Vector3(randomOffset, 0f, 0f);
        transform.position = newPosition;        
        StartCoroutine(Flash());

    }

    IEnumerator Flash()
    {
        float startTime = Time.time;
        while (Time.time - startTime < 0.1f)
        {
            this.lights.intensity = 5;
            yield return null;
        }
        this.lights.intensity = 1.5f;
    }
}
