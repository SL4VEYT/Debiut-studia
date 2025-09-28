using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge_Locator : MonoBehaviour
{
    [SerializeField]
    protected AnimationClip clip;
    [SerializeField]
    protected float climbingHorizontalOffset = .5f;

    private Vector2 topOfPlayer;
    private GameObject ledge;
    private float animationTime = .5f;
    private bool falling;
    private bool moved;
    public bool climbing;

    
    [HideInInspector]
    public bool grabbingLedge;
    private Collider2D col;
    private Rigidbody2D rb;
    private Animator anim;
    

    private void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (clip != null)
        {
            animationTime = clip.length;
        }
    }
    

    

    protected virtual void FixedUpdate()
    {
        CheckForLedge();
        LedgeHanging();
        if (GameManager.gameManager.Player_health.Health == 0)
        {
            anim.SetBool("LedgeHanging", false);
            anim.SetBool("LedgeClimbing", false);
            rb.bodyType = RigidbodyType2D.Dynamic;
            falling = true;
        }
        }

    protected virtual void CheckForLedge()
    {
        if (!falling)
        {
            
            
                if (transform.localScale.x > 0)
                {
                    topOfPlayer = new Vector2(col.bounds.max.x + .1f, col.bounds.max.y);
                    RaycastHit2D hit = Physics2D.Raycast(topOfPlayer, Vector2.right, .2f);
                    if (hit && hit.collider.gameObject.GetComponent<Ledge>())
                    {
                        ledge = hit.collider.gameObject;
                        Collider2D ledgeCollider = ledge.GetComponent<Collider2D>();
                        if (col.bounds.max.y < ledgeCollider.bounds.max.y && col.bounds.max.y > ledgeCollider.bounds.center.y && col.bounds.min.x < ledgeCollider.bounds.min.x)
                        {
                            grabbingLedge = true;
                            anim.SetBool("LedgeHanging", true);
                        }
                    }
                }

                else
                {
                    topOfPlayer = new Vector2(col.bounds.min.x - .1f, col.bounds.max.y);
                    RaycastHit2D hit = Physics2D.Raycast(topOfPlayer, Vector2.left, .2f);
                    if (hit && hit.collider.gameObject.GetComponent<Ledge>())
                    {
                        ledge = hit.collider.gameObject;
                        Collider2D ledgeCollider = ledge.GetComponent<Collider2D>();
                        if (col.bounds.max.y < ledgeCollider.bounds.max.y && col.bounds.max.y > ledgeCollider.bounds.center.y && col.bounds.max.x > ledgeCollider.bounds.max.x)
                        {
                            anim.SetBool("LedgeHanging", true);
                            grabbingLedge = true;
                        }
                    }
                }
            
            if (ledge != null && grabbingLedge)
            {
                AdjustPlayerPosition();
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    protected virtual void LedgeHanging()
    {
        if (grabbingLedge && Input.GetAxis("Vertical") > 0 && !climbing)
        {
            climbing = true;
            // anim.SetBool("LedgeHanging", false);
            float forwardNudge = 0.5f;
            float targetY = ledge.GetComponent<Collider2D>().bounds.max.y + col.bounds.extents.y;
            if (transform.localScale.x > 0)
            {
                StartCoroutine(ClimbingLedge(new Vector2(
                 transform.position.x + climbingHorizontalOffset + forwardNudge,
                 targetY), animationTime));
            }
            else
            {
                StartCoroutine(ClimbingLedge(new Vector2(
               transform.position.x - climbingHorizontalOffset - forwardNudge,
               targetY), animationTime));
            }
        }
        if (grabbingLedge && Input.GetAxis("Vertical") < 0)
        {
            ledge = null;
            moved = false;
            grabbingLedge = false;
            anim.SetBool("LedgeHanging", false);
            falling = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            Invoke("NotFalling", .5f);
        }
    }

    //This method will handle moving the Character on top of the platform when this method is called
    protected virtual IEnumerator ClimbingLedge(Vector2 topOfPlatform, float duration)
    {
        float time = 0;
        Vector2 startValue = transform.position;
        while (time <= duration)
        {
            anim.SetBool("LedgeClimbing", true);
            transform.position = Vector2.Lerp(startValue, topOfPlatform, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        ledge = null;
        moved = false;
        climbing = false;
        grabbingLedge = false;
        anim.SetBool("LedgeClimbing", false);
        anim.SetBool("LedgeHanging", false);
    }

    //Method that snaps Character to the best on the platform based on a hangingHorizontalOffest and hangingVerticalOffset values found on the Ledge script of the platform
    protected virtual void AdjustPlayerPosition()
    {
 
        if (!moved)
        {
    
            moved = true;
      
            Collider2D ledgeCollider = ledge.GetComponent<Collider2D>();
        
            Ledge platform = ledge.GetComponent<Ledge>();
  
            if (transform.localScale.x > 0)
            {
               
                transform.position = new Vector2((ledgeCollider.bounds.min.x - col.bounds.extents.x) + platform.hangingHorizontalOffset, (ledgeCollider.bounds.max.y - col.bounds.extents.y - .5f) + platform.hangingVerticalOffset);
            }
       
            else
            {

                transform.position = new Vector2((ledgeCollider.bounds.max.x + col.bounds.extents.x) - platform.hangingHorizontalOffset, (ledgeCollider.bounds.max.y - col.bounds.extents.y - .5f) + platform.hangingVerticalOffset);
            }
        }
    }

   
    protected virtual void NotFalling()
    {
        
        falling = false;
    }
}



