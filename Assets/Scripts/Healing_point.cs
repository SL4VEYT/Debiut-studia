using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing_point : MonoBehaviour
{
    Player_Checkpoints Player_Checkpoints;
    Animator animator;
    bool used = false;
    void Awake()
    {
        animator = GetComponent<Animator>();
        Player_Checkpoints = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Checkpoints>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            Player_Checkpoints.Startpos = transform.position;
            animator.SetBool("Used", true);
            used = true;
        }
    }
}
