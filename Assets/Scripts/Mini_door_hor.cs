using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_door_hor : MonoBehaviour
{
    public BoxCollider2D lever;
    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {



        if (lever.enabled == false)
        {
           
            rb.velocity = new Vector2(-10f, 0f);
        }
        

    }
}
