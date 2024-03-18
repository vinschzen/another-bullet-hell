using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DamageIndicator : MonoBehaviour
{
    void Start()
    {

        StartCoroutine(showIndicator());
    }

    IEnumerator showIndicator()
    {
        float duration = 1.5f;
        float moveSpeed = 1f;
        float elapsedTime = 0.0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + new Vector3(0,0,1) * (duration * moveSpeed);

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
