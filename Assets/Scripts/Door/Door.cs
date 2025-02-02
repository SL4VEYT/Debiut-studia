using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public BoxCollider2D Lever;
    public BoxCollider2D door;
    public Animator animator;
    public SpriteRenderer sprite_drzwi;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Delete()
    {
        sprite_drzwi.enabled = false;
        door.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("Open", true);
            Invoke("Delete", 1f);
        }

    }
}
