using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public BoxCollider2D Lever;
    public BoxCollider2D door;
     Animator animator;
     SpriteRenderer sprite_drzwi;
   

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite_drzwi = GetComponent<SpriteRenderer>();
    }

    void Delete()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Lever.enabled == true)
        {
            animator.SetBool("Open", true);
            Invoke("Delete", 1f);
        }

    }
}
