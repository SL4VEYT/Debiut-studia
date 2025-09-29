using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera_enemy_horizontal : MonoBehaviour
{
    public float speed = 2f;
    public float faster_speed = 5f;
    public float range;
    public bool See_Player;
    public float chaseOffset; 
    float startingX;
    public CapsuleCollider2D hitbox;
    int direction;

    public bool StartLeft = false;
    float effectiveRange;
    float effectiveoffset;

    private Transform playerTransform;

    void Awake()
    {
        startingX = transform.position.x;
        if (StartLeft)
        {
            direction = -1; // Start moving to the left
            effectiveRange = -range;
            effectiveoffset = -chaseOffset;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        else
        {
            effectiveoffset = chaseOffset;
            direction = 1;
            effectiveRange = range;// Start moving to the right (default)
        }
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

    private void OnTriggerStay2D(Collider2D collision)
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
            //speed = 2f;
            Invoke("forgor", 1f);
        }
    }
    private void forgor()
    {
        playerTransform = null;
        speed = 2f;

    }
    private void movement()
    {
        /*transform.Translate(Vector2.right * speed * Time.deltaTime * direction);

       
        if (transform.position.x < startingX || transform.position.x > startingX + range)
        {
            direction *= -1;
           
            if (transform.position.x < startingX)
                transform.position = new Vector2(startingX, transform.position.y);
            else if (transform.position.x > startingX + range)
                transform.position = new Vector2(startingX + range, transform.position.y);
        }
        */
        transform.Translate(Vector2.right * speed * Time.deltaTime * direction);




        if (transform.position.x < startingX + Mathf.Min(0, effectiveRange) ||
        transform.position.x > startingX + Mathf.Max(0, effectiveRange))
        {
            direction *= -1;

            // Snap to the correct boundary (either the start or the end of the range)
            // Check if we hit the left boundary (startingX + negative range)
            if (transform.position.x < startingX)
            {
                transform.position = new Vector2(startingX + Mathf.Min(0, effectiveRange), transform.position.y);
            }
            // Check if we hit the right boundary (startingX + positive range)
            else if (transform.position.x > startingX)
            {
                transform.position = new Vector2(startingX + Mathf.Max(0, effectiveRange), transform.position.y);
            }
        }
    }

    private void chase()
    {
        /* if (playerTransform != null)
         {
             speed = faster_speed;
             // Calculate the desired X position based on player's X PLUS the offset
             float targetX = playerTransform.position.x + effectiveoffset;

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
     */

        if (playerTransform != null)
        {
            speed = faster_speed;

            // Use the pre-calculated effectiveoffset (which is negative when facing left)
            float targetX = playerTransform.position.x + effectiveoffset;

            // --- CORRECTED CLAMPING LOGIC ---
            // Clamp the targetX within the enemy's patrol range using Min/Max
            float minBoundary = startingX + Mathf.Min(0, effectiveRange);
            float maxBoundary = startingX + Mathf.Max(0, effectiveRange);

            targetX = Mathf.Clamp(targetX, minBoundary, maxBoundary);
            // ---------------------------------

            // Calculate the horizontal direction to the clamped target X
            Vector2 directionToClampedTarget = new Vector2(targetX - transform.position.x, 0).normalized;

            // If the enemy is very close to the clamped target X, stop horizontal movement
            if (Mathf.Abs(targetX - transform.position.x) < 0.05f)
            {
                directionToClampedTarget.x = 0;
            }

            // Move horizontally towards the clamped target X
            transform.Translate(directionToClampedTarget * speed * Time.deltaTime);
        }
    }

}
