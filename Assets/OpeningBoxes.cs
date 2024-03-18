using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningBoxes : MonoBehaviour
{
    [SerializeField] private float easeSpeed = 0.005f;
    [SerializeField] private float topBoxTargetY = 400; 
    [SerializeField] private float bottomBoxTargetY = -400;

    void Start()
    {
        Transform topBoxTransform = transform.Find("TopBox");
        Transform bottomBoxTransform = transform.Find("BottomBox");

        if (topBoxTransform != null)
        {
            StartCoroutine(EaseOutOfScreen(topBoxTransform, topBoxTargetY, true)); 
        }

        if (bottomBoxTransform != null)
        {
            StartCoroutine(EaseOutOfScreen(bottomBoxTransform, bottomBoxTargetY, false)); 
        }
    }

    IEnumerator EaseOutOfScreen(Transform boxTransform, float targetY, bool easeUp)
    {
        Vector3 startPosition = boxTransform.localPosition;
        Vector3 targetPosition = new Vector3(startPosition.x, targetY, startPosition.z);

        float timeElapsed = 0f;

        while (timeElapsed < 1f)
        {
            float easedTime = Mathf.SmoothStep(0f, 1f, timeElapsed);
            float easedPosition = Mathf.Lerp(startPosition.y, targetPosition.y, EaseOutCubic(easedTime));

            boxTransform.localPosition = new Vector3(startPosition.x, easedPosition, startPosition.z);
            timeElapsed += Time.deltaTime * easeSpeed;
            yield return null;
        }

        boxTransform.localPosition = targetPosition;
        Destroy(gameObject); 
    }

    float EaseOutCubic(float t)
    {
        return 1f - Mathf.Pow(1f - t, 3f);
    }

}
