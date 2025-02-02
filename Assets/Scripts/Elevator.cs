using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public BoxCollider2D Lever;
    private Rigidbody2D rb;
    bool ass = true;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Winda()
    {
        if (ass == true)
        {
            rb.velocity = new Vector2(0f, 3f);
            ass = false;
        }
    }
    
    void FixedUpdate()
    {
        if (Lever.enabled == false)
        {
            Winda();
        }
    }
}
