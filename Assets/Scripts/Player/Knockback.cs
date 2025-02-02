using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    public float knockbackForce = 10f;
    public float knockbackDuration = 0.5f; // Adjust duration as needed

    private Rigidbody2D rb;
    private Movement playerMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<Movement>(); // Assuming both scripts are on the same GameObject
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Calculate knockback direction
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

            // Apply knockback force
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            // Disable movement
            playerMovement.isKnockedBack = true;

            Physics2D.IgnoreLayerCollision(8, 6, true); //eksperyment

            // Start a coroutine to re-enable movement after duration
            StartCoroutine(EnableMovementAfterTime(knockbackDuration));
        }
    }

    IEnumerator EnableMovementAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        playerMovement.isKnockedBack = false;
        Physics2D.IgnoreLayerCollision(8, 6, false);
    }


    
}
