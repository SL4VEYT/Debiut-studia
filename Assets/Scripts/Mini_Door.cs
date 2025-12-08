using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Door : MonoBehaviour
{
    public CapsuleCollider2D enemy_camera;
    public float moveDistance = 5f; 
    public float moveSpeed = 4f;    

   
    private Rigidbody2D rb;
    private Vector2 closedPosition;
    private Vector2 openPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        closedPosition = rb.position;
        openPosition = closedPosition + new Vector2(0, moveDistance);
    }

    private void FixedUpdate()
    {
        Vector2 targetPosition;

      
            if (enemy_camera.enabled == true)
            {
                targetPosition = closedPosition;
            }

            else
            {
                targetPosition = openPosition;
            }


            rb.position = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        
        

       
    }
}
