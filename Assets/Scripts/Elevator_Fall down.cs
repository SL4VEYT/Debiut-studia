using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Fall_down : MonoBehaviour
{
    public BoxCollider2D Lever;
    private Rigidbody2D rb;
    private bool PlayerT;
    private bool Hit_Ground;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            PlayerT = true;
        }

        if (collision.gameObject.layer == 10)
        {
            Hit_Ground = true;
        }

    }
    void Update()
    {
        if (Lever.enabled == false && PlayerT == true)
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }

        if(Hit_Ground == true)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        
    }
}
