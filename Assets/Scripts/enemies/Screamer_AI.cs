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

    bool IsAvailable = true; //¿eby nie da³o siê go stunlockowaæ
    public float CooldownDuration = 6.0f;

    public bool PlayerInRange = false;

    public Rigidbody2D playerRb;
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
        if (collision.CompareTag("Player") && IsAvailable && !Crouch)
        {
            // Check if the player is moving
            if (IsPlayerMoving())
            {
                hitbox.enabled = true;
                PlayerInRange = true;
                LookAtTarget();
                animator.SetBool("IsAwakened", true);
                Invoke("Attack", 0.7f);
            }
        }
    }

    private bool IsPlayerMoving()
    {
        if (playerRb != null)
        {
            // Check if there is any horizontal or vertical velocity
            return Mathf.Abs(playerRb.velocity.x) > 0.1f || Mathf.Abs(playerRb.velocity.y) > 0.1f;
            

        }
        return false;
    }


    private void ResetState()
    {
        // This check prevents an error if the enemy is destroyed before the call
        if (hurtbox != null)
        {
            hurtbox.enabled = false;
        }

        
            
        

        PlayerInRange = false;
        animator.SetBool("IsAwakened", false);

        // Start the cooldown coroutine
        StartCoroutine(StartCooldown());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ResetState();
            hitbox.enabled = false;
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

    public IEnumerator StartCooldown() //cooldown
    {
        IsAvailable = false;

        yield return new WaitForSeconds(CooldownDuration);

        IsAvailable = true;

    }
}
