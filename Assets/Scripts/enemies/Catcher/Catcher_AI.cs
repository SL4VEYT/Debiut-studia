using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher_AI : MonoBehaviour
{
   public bool See_Player;
     Animator animator;
    public Transform playerTransform;
     SpriteRenderer spriteRenderer;
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LookAtTarget()
    {
        Vector3 direction = playerTransform.position - transform.position;
        float distanceX = direction.x; // Get the X distance between enemy and player

        // Check if player is to the left or right of the enemy
        spriteRenderer.flipX = distanceX < 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            See_Player = true;
            animator.SetBool("Player_Inrange", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            See_Player = false;
            animator.SetBool("Player_Inrange", false);
        }
    }
    private void Update()
    {
        if (See_Player == true)
        {
            LookAtTarget();
        }
    }
}
