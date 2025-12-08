using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable_block : MonoBehaviour
{
    public GameObject target;
    public BoxCollider2D lever;
    public float moveSpeed = 1.0f; 

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool isMoving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (target != null)
        {
            targetPosition = target.transform.position;
        }
        else
        {
            Debug.LogError("Movable_block requires a 'target' GameObject to be assigned!");
        }
    }

    void FixedUpdate()
    {
        if (lever != null && lever.enabled == false && !isMoving)
        {
            isMoving = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true; 
        }

        if (isMoving)
        {
            Vector2 newPosition = Vector2.MoveTowards(
                rb.position,
                targetPosition,
                moveSpeed * Time.fixedDeltaTime
            );

            rb.MovePosition(newPosition);
            if (Vector2.Distance(rb.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }
}
