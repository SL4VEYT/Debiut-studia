using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Ladder : MonoBehaviour
{
    private float vertical;
    private float speed = 4f;
    private bool Isladder;
    public bool IsClimbing;

    private Transform ladder;

    Animator animator;
    GameObject player;

    [SerializeField] private Rigidbody2D rb;

    public float ladderCooldownDuration = 0.5f;

    private bool canGrabLadder = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (Isladder && Mathf.Abs(vertical) > 0f)
        {
            IsClimbing = true;
        }

        
        if (IsClimbing && Input.GetButtonDown("Jump"))
        {
            OFFTHELADDER();
            rb.velocity = new Vector2(rb.velocity.x, 9f); 
        }
    }
    private void FixedUpdate()
    {
        if (IsClimbing)
        {
            // Lock horizontal movement
            rb.velocity = new Vector2(0f, vertical * speed);
            rb.gravityScale = 0f;

            // Align player's X position with the ladder
            transform.position = new Vector3(ladder.position.x, transform.position.y, transform.position.z);

            animator.SetBool("Climbing", true);
        }
        else
        {
            rb.gravityScale = 2f;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Ladder") && canGrabLadder)
        {
            Isladder = true;
            ladder = collision.transform;
        }
    }


    public void OFFTHELADDER()
    {
        Isladder = false;
        IsClimbing = false;
        animator.SetBool("Climbing", false);
        StartCoroutine(LadderCooldown());
    }

    private IEnumerator LadderCooldown()
    {
        canGrabLadder = false;
        yield return new WaitForSeconds(ladderCooldownDuration);
        canGrabLadder = true;
    }
}
