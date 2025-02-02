using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scren_Shake : MonoBehaviour
{
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.2f;

    [SerializeField] public Transform target;
    float elapsedTime = 0f;

    public bool isScreenShaking = false;


    public void TriggerScreenShake()
    {
        isScreenShaking = true;
        elapsedTime = 0f;
    }

    void Update()
    {
        if (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;

            float x = Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = Random.Range(-shakeMagnitude, shakeMagnitude);

            transform.position = target.position + new Vector3(x, y, -10);
        }
        else
        {
            isScreenShaking = false;
           //transform.position = target.position;
        }
    }
}
