using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public Camera cam; 
    public float tiltSpeed = 0.001f; 
    public float shakeAmount = 0.005f; 

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        Vector3 dir = mousePos - cam.transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir);
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, rotation, tiltSpeed * Time.deltaTime);

        // cam.transform.rotation *= Quaternion.Euler(Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount));
    }
}
