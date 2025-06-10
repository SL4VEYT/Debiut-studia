using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Door : MonoBehaviour
{
    // Start is called before the first frame update
    
    public CapsuleCollider2D enemy_camera;
    private Rigidbody2D rb;
    private bool a = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {

        if (enemy_camera.enabled == true)
        {
            rb.velocity = new Vector2(0f, -15f);
            a = true;
        }


        else
        if (a == true)
        {
            rb.velocity = new Vector2(0f, 4f);
            a = false;
        }
        
    }
}
