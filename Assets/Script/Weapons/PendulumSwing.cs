using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeapon))]
public class PendulumSwing : BaseWeapon
{
    public float bulletSpeed = 2f;
    public float spinSpeed = 20f;
    public float initialDuration = 0f;
    private float swing = 180f;

    private bool swinging = false;
    private GameObject pendulum;
    public PendulumSwing() : base("Pendulum Swing", 10, 1f, 0.5f, 15f, 1)
    {
    }

    void Awake()
    {
        this.nextCooldown = Time.time + initialDuration;
        this.pendulum = GameObject.Find("Pendulum Parent");
    }
    private float waitDuration = 4.5f; 

    public override void Fire(Transform bulletSpawn)
    {
        if (!swinging)
        {
            swinging = true;
            StartCoroutine(Swing());
        }
    }

    IEnumerator Swing()
    {
        float startTime = Time.time;
        while (pendulum.transform.eulerAngles.y != swing)
        {
            pendulum.transform.rotation = Quaternion.Euler(0f, pendulum.transform.eulerAngles.y - (swing/1200), 0f);
            if (pendulum.transform.eulerAngles.y > -180 && pendulum.transform.eulerAngles.y < -135) 
            {
                this.swing = -this.swing;
            }

            if (pendulum.transform.eulerAngles.y < 180 && pendulum.transform.eulerAngles.y > 135) 
            {
                this.swing = -this.swing;
            }


            yield return null;
        }
    }

}
