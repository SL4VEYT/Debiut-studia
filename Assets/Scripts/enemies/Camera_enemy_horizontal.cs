using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera_enemy_horizontal : MonoBehaviour
{
    private float speed = 2f;
    public float range;
    public bool See_Player;
    public float chaseOffset; 
    float startingX;
    public CapsuleCollider2D hitbox;
    int direction = 1;

    private Transform playerTransform;

    void Awake()
    {
        startingX = transform.position.x;
    }

    void FixedUpdate()
    {
        if (See_Player == false)
        {
            movement();
        }
        else
        {
            chase();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) 
        {
            See_Player = true;
            hitbox.enabled = true;
            playerTransform = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) 
        {
            See_Player = false;
            hitbox.enabled = false;
            speed = 2f;
            Invoke("forgor", 1f);
        }
    }
    private void forgor()
    {
        playerTransform = null;
        
    }
    private void movement()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime * direction);

       
        if (transform.position.x < startingX || transform.position.x > startingX + range)
        {
            direction *= -1;
           
            if (transform.position.x < startingX)
                transform.position = new Vector2(startingX, transform.position.y);
            else if (transform.position.x > startingX + range)
                transform.position = new Vector2(startingX + range, transform.position.y);
        }
    }

    private void chase()
    {
        if (playerTransform != null)
        {
            speed = 4f;
            // Calculate the desired X position based on player's X PLUS the offset
            float targetX = playerTransform.position.x + chaseOffset;

            // Clamp the targetX within the enemy's patrol range
            targetX = Mathf.Clamp(targetX, startingX, startingX + range);

            // Calculate the horizontal direction to the clamped target X
            Vector2 directionToClampedTarget = new Vector2(targetX - transform.position.x, 0).normalized;

            // If the enemy is very close to the clamped target X, stop horizontal movement
            if (Mathf.Abs(targetX - transform.position.x) < 0.05f) // Small threshold to stop movement
            {
                directionToClampedTarget.x = 0;
            }

            // Move horizontally towards the clamped target X
            transform.Translate(directionToClampedTarget * speed * Time.deltaTime);

        }
    }

}
