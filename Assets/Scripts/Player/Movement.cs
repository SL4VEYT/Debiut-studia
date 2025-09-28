using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{

    private float horizontal;
    private float speed = 6f;
    private float jump = 9f;
    bool Crouch = false;

    public Ledge_Locator ledgeLocator;

    bool AudioPlayingRN;

    public bool isKnockedBack;

    public bool fallthrough;
    private bool FacingRight = true;
    public Animator animator;
    public SpriteRenderer PlayerSprite;

    private float JumpAfterGround = 0.15f;
    private float JumpAfterGrounCounter;

    [SerializeField] public bool gc; //rzecz do ledge locator

    private float JumpBuffer = 0.2f;
    private float JumpBufferCounter;

    public Ladder ladderscript;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public BoxCollider2D Standing_Hitbox; 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundlayer;
    void Update()
    {
        if (ladderscript.IsClimbing == false && !ledgeLocator.grabbingLedge && !ledgeLocator.climbing && GameManager.gameManager.Player_health.Health != 0)
        { horizontal = Input.GetAxisRaw("Horizontal"); }
        else
        { horizontal = 0; }

        if (Crouch == true)
        {
            animator.SetBool("IsCrouching", true);
            if (Input.GetButton("Horizontal"))
            {
                animator.SetFloat("Speed", 1);
            }
            if (Input.GetButtonUp("Horizontal"))
            {
                animator.SetFloat("Speed", 0);
            }


        }
        else
        {
            animator.SetBool("IsCrouching", false);
            if (Input.GetButton("Horizontal"))
            {
                animator.SetFloat("Speed", 2);
            }
            if (Input.GetButtonUp("Horizontal"))
            {
                animator.SetFloat("Speed", 0);
            }
        }

        if (!IsGround())
        {
            Standing_Hitbox.enabled = true;
            Crouch = false;
            animator.SetBool("IsCrouching", false);
            gc = false;
            animator.SetBool("gc", false);
            speed = 6f;

            if (rb.velocity.y > 0.01) //animacja spadania i skakania
            {
                animator.SetBool("YVelocity", true);
            }
            else
            {
                animator.SetBool("YVelocity", false);
            }
        }

        if (GameManager.gameManager.Player_health.Health != 0)
        {
            if (IsGround())
            {
                JumpAfterGrounCounter = JumpAfterGround;
                gc = true;
                animator.SetBool("gc", true);
            }
            else
            {
                JumpAfterGrounCounter -= Time.deltaTime;
            }



            if (Input.GetButtonDown("Jump"))
            {
                JumpBufferCounter = JumpBuffer;
            }
            else
            {
                JumpBufferCounter -= Time.deltaTime;
            }

            if (JumpBufferCounter > 0f && JumpAfterGrounCounter > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
                JumpBufferCounter = 0f;
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f) //eksperymentalne opóŸnienie l¹dowania
            {

                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                JumpAfterGrounCounter = 0f;
            }
        }

        if (!isKnockedBack)
        {
            flip();
        }
          
        if (Input.GetButtonDown("Crouch")) //kucanie (works)
        {
            if (Crouch == true)
            {
                //Stand up
                Crouch = false;
                Standing_Hitbox.enabled = true;
                animator.SetBool("IsCrouching", false);
                speed = 0f;
                jump = 0f;
                Invoke("resume", 0.5f);
            }
            else
            {
                //Crouch
                Crouch = true;
                Standing_Hitbox.enabled = false;
                animator.SetBool("IsCrouching", true);
                speed = 0f;
                jump = 0f;
                Invoke("resume2", 0.5f);
            }
        }


        

    }
    private void FixedUpdate()
    {
        if (GameManager.gameManager.Player_health.Health != 0)
        {
            if (!isKnockedBack)
            {
                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); //to zostaje

                
                
            }
        }
       
    }

    public bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);
        

    }
private void flip()
    {
        if (FacingRight && horizontal < 0f || !FacingRight && horizontal > 0f)
        {
            FacingRight = !FacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

        }
    }
    void resume()
    {
        speed = 6f;
        jump = 9f;
    }
   void resume2()
    {
        speed = 3f;
        jump = 9f;
    }

}
