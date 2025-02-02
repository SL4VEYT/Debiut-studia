using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screamer_AI : MonoBehaviour
{
    public BoxCollider2D hitbox;
    bool Crouch = false;
    public Animator animator;
    public Transform playerTransform;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D hurtbox;

    public bool PlayerInRange = false;
    void Start()
    {
        hurtbox.enabled = false;
        hitbox.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Crouch")) //kucanie (works)
        {
            if (Crouch == true)
            {
                //Stand up
                Crouch = false;

            }
            else
            {
                //Crouch
                Crouch = true;

            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            Crouch = false;
        }

        if(PlayerInRange == false)
        {
            hurtbox.enabled = false;

            hitbox.enabled = false;
        }
        if (hurtbox.enabled == true)
        {
            Invoke("Stop", 0.1f);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Crouch == false)
            {
                LookAtTarget();
                hitbox.enabled = true;
                animator.SetBool("IsAwakened", true); 
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
            if (collision.CompareTag("Player"))
            {
              if (Crouch == false)
              {
                PlayerInRange = true;
                Invoke("Attack", 0.7f);
              }
            }

            if (collision.CompareTag("Finish"))   //spadaj¹ca winda
            {
            PlayerInRange = false;
            Destroy(this);
            }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hurtbox.enabled = false;
            hitbox.enabled = false;
            PlayerInRange = false;
            animator.SetBool("IsAwakened", false);
            
        }
    }
    void LookAtTarget()
    {
        Vector3 direction = playerTransform.position - transform.position;
        float distanceX = direction.x; // Get the X distance between enemy and player

        // Check if player is to the left or right of the enemy
        spriteRenderer.flipX = distanceX > 0f;
    }

    void Attack()
    {
        hurtbox.enabled = true;
    }
    void Stop()
    { 
        hurtbox.enabled = false;
    }
}
