using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goober_AI : MonoBehaviour
{
   
    public Transform[] waypoints; 
    public float moveSpeed; 
    public float waitTime; 
    Animator animator;

    private int currentWaypointIndex = 0;
    private bool moving = true;
    private bool PlayerSeen = false;

    public bool IsAvailable = true; 
    public float CooldownDuration = 6.0f;

    private Goober_ass Goober_ass;
    private walldetector walldetector;

    public LayerMask ground; //raycast shit
    private void Awake()
    {
        animator = GetComponent<Animator>();
        Goober_ass = GetComponentInChildren<Goober_ass>();
        walldetector = GetComponentInChildren<walldetector>();
    }
    private void Update()
    {
        if (moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);
            

            if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position)< 0.1f)
            {
                moving = false;
                Invoke("StartMoving", waitTime);
                Invoke("RoTaTe", waitTime);
            }
        }
        if (IsAvailable == true)
        {
            Asshole_Protector();
        }
    }

    private void StartMoving()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        moving = true;
    }

    private void RoTaTe()
    {
        transform.Rotate(0, 180, 0, Space.World);
    }

    private void OnTriggerStay2D(Collider2D collision) //previously OnTriggerEnter
    {

        if (collision.CompareTag("Barrel") && PlayerSeen == true)
        {
            Barrel_Hide barrelScript = collision.GetComponent<Barrel_Hide>();
            if (barrelScript != null)
            {
                barrelScript.ForceUnhide(); //works finally
                
            }
        }



        if (collision.CompareTag("Player") || collision.CompareTag("loudradius"))
        {
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            float distance = Vector2.Distance(transform.position, collision.transform.position);

            Vector2 origin = new Vector2(transform.position.x, transform.position.y + 0.2f);
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, ground);
            Debug.DrawRay(origin, direction * distance, hit.collider == null ? Color.green : Color.red);

            if (hit.collider == null)
            {
                PlayerSeen = true;
                if (IsAvailable == true)
                {
                    STOPRIGHTTHERE();
                    animator.SetTrigger("Alert");
                    StartCoroutine(StartCooldown());
                    Invoke("STARTRIGHTTHERE", 0.5f);
                    
                }
            }
            else
            {
                PlayerSeen = false;
            }
            

           

        }

    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("loudradius"))
        {

            STOPRIGHTTHERE();
            Invoke("STARTRIGHTTHERE", 1f);
        }
    } */

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Invoke("forget", 2f);           
        }

    }

    private void STOPRIGHTTHERE()
    {
        moveSpeed = 0f;
    }

    private void STARTRIGHTTHERE()
    {
        moveSpeed = 10f;
        animator.SetBool("Alert", false);
    }
    void forget()
    {
        PlayerSeen = false;
        moveSpeed = 3f;
    }

    public IEnumerator StartCooldown() //cooldown
    {
        IsAvailable = false;
        
        yield return new WaitForSeconds(CooldownDuration);
        
        IsAvailable = true;
        
    }
    private float turnCooldown = 0.5f;
    private float lastTurnTime;

    void Asshole_Protector()
    {
        if(Goober_ass.Player_Backdoor || walldetector.Iswall && Time.time > lastTurnTime + turnCooldown)
        {
            currentWaypointIndex = (currentWaypointIndex - 1 + waypoints.Length) % waypoints.Length;

            // Immediately start moving towards the new target waypoint
            moving = true;
            RoTaTe();
            
        }
    }

   
}
