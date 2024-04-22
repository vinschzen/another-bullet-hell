using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class FlipCamera : BaseWeapon
{
    public float spinSpeed = 2f;
    public float initialDuration = 0;
    public FlipCamera() : base("Flip Camera", 10, 0.1f, 0.05f, 120, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
    }
    public override void Fire(Transform bulletSpawn)
    {
        bulletSpawn = GameObject.Find("Main Camera").transform;
        StartCoroutine(RotateCamera(bulletSpawn, spinSpeed));
    }

    IEnumerator RotateCamera(Transform cameraTransform, float speed)
    {
        Quaternion targetRotation = Quaternion.Euler(90f, cameraTransform.rotation.y + 180f, 0f);
        Quaternion startRotation = cameraTransform.rotation;

        float duration = 80.0f; 

        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            cameraTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, timeElapsed / duration);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        cameraTransform.rotation = targetRotation;
    }
}
