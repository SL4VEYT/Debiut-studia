using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Ladder : MonoBehaviour
{
    private float vertical;
    private float speed = 4f;
    private bool Isladder;
    private bool IsClimbing;

    private Transform ladder; //ai

    Animator animator;
    GameObject player;

    [SerializeField] private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (Isladder && Mathf.Abs(vertical) > 0f)
        {
            IsClimbing = true;
            
        }
    }
    private void FixedUpdate()
    {
        if (IsClimbing)
        {

            if(Input.GetKey(KeyCode.Space))
            {
                rb.gravityScale = 2f;
                IsClimbing = false;
                animator.SetBool("Climbing", false);

            }
            else
            {

                rb.gravityScale = 0f;
                rb.velocity = new Vector2(rb.velocity.x, vertical * speed);

                // Align player's X position with the ladder
                transform.position = new Vector3(ladder.position.x, transform.position.y, transform.position.z); //ai

                animator.SetBool("Climbing", true);
            }
        }
        
        else
        {
            rb.gravityScale = 2f;
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            Isladder = true;
            ladder = collision.transform; //ai
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            Isladder = false;
            IsClimbing = false;
            animator.SetBool("Climbing", false);
        }   
    }
}
