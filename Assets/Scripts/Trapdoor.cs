using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapdoor : MonoBehaviour
{
    private Camera_enemy_horizontal kamera;
    private BoxCollider2D box;
    public float Returno = 1.0f;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        box.enabled = true;
        kamera = FindObjectOfType<Camera_enemy_horizontal>();
    }

   
    void Update()
    {
        if (kamera != null && kamera.See_Player && box.enabled)
        {

            box.enabled = false;

            // Start the timer to re-enable the trapdoor after the delay
            StartCoroutine(RearmTrapdoorAfterDelay(Returno));
        }

    }

    private IEnumerator RearmTrapdoorAfterDelay(float delay)
    {
        
        yield return new WaitForSeconds(delay);

       
        if (kamera == null || !kamera.See_Player)
        {
            box.enabled = true;
        }
      
    }





}
