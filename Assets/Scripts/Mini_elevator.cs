using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_elevator : MonoBehaviour
{
    public BoxCollider2D Lever;
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Lever.enabled == false)
        {
            rb.velocity = new Vector2(0f, 1.5f);
        }
    }

    
}
