using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_door_hor : MonoBehaviour
{
    public BoxCollider2D lever;
    public BoxCollider2D secondlever;
    private Rigidbody2D rb;
  

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {



        if (lever.enabled == false || secondlever.enabled == false)
        {
           
            rb.velocity = new Vector2(0f, 10f);
        }
        

    }
}
