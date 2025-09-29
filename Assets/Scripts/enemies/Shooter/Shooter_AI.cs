using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter_AI : MonoBehaviour
{
    /* public float amplitude = 0.1f; // How high and low the enemy bobs (e.g., 0.1 units)
     public float frequency = 1f;   // How fast the enemy bobs (e.g., 1 cycle per second)
     private Vector3 startPosition;

     void Start()
     {
         startPosition = transform.position;
     }


     void Update()
     {
         float yOffset = amplitude * Mathf.Sin(Time.time * frequency);
         transform.position = startPosition + new Vector3(0, yOffset, 0);
     }*/

    private const float ANIMATION_CYCLE_FREQUENCY = 2f * Mathf.PI;
    private const float FRAME_DELAY_TIME = 1f * (1f / 12f);
    private Animator animator;

    public float amplitude = 0.1f;
    public float power = 2.0f;
    private float EyeDryeness;

    public float rollInterval = 1f;

    private Vector3 startPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        StartCoroutine(EyeDryer());
    }

    void Update()
    {
        if (EyeDryeness == 4)
        {
            animator.SetBool("DryEye", true);
            EyeDryeness = 0;
            Invoke("ass", 1f);
        }

       

        float timeInput = Time.time - FRAME_DELAY_TIME;
        float sinValue = Mathf.Sin((timeInput * ANIMATION_CYCLE_FREQUENCY));

        float invertedSinValue = -sinValue;
        float yOffset;



        if (invertedSinValue >= 0)
        {
            yOffset = amplitude * Mathf.Pow(invertedSinValue, power);
            float smoothedValue = Mathf.Lerp(yOffset, amplitude * invertedSinValue, invertedSinValue);
            yOffset = smoothedValue;

        } //I have zero clue what any of this shit means
        else
        {
            yOffset = -amplitude * Mathf.Pow(Mathf.Abs(invertedSinValue), 1.0f);
        }

        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }

    public IEnumerator EyeDryer()
    {
        while (true)
        {
            float randomCheck = Random.value;

            if (randomCheck > 0.5f)
            {
                EyeDryeness += 1;
            }
            yield return new WaitForSeconds(rollInterval);
        }

    }
    void ass()
    {
        animator.SetBool("DryEye", false);
    }
}
