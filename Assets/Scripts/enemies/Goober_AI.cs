using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goober_AI : MonoBehaviour
{

    public Transform[] waypoints; // Array of waypoints to patrol between
    public float moveSpeed; // Speed of movement
    public float waitTime; // Time to wait at each waypoint
    Animator animator;

    private int currentWaypointIndex = 0;
    private bool moving = true;
    private bool PlayerSeen = false;

    public bool IsAvailable = true; //¿eby nie da³o siê go stunlockowaæ
    public float CooldownDuration = 6.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position)
 < 0.1f)
            {
                moving = false;
                Invoke("StartMoving", waitTime);
                Invoke("RoTaTe", 1f);
            }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {
            PlayerSeen = true;
            if(IsAvailable == true)
            { 
              STOPRIGHTTHERE();
                animator.SetTrigger("Alert");
                Invoke("STARTRIGHTTHERE", 0.5f);
                StartCoroutine(StartCooldown());
                


            }
            else
            {
                //tu powinno byæ STARTRIGHTTHERE();
            }

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {
            PlayerSeen = false;
            moveSpeed = 3f;
            
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


    public IEnumerator StartCooldown() //cooldown
    {
        IsAvailable = false;
        
        yield return new WaitForSeconds(CooldownDuration);
        
        IsAvailable = true;
        
    }




}
