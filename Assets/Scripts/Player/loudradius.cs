using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loudradius : MonoBehaviour
{
    BoxCollider2D loud;
    Movement movement;
    void Awake()
    {
        loud = GetComponent<BoxCollider2D>();
        loud.enabled = false;
        movement = GetComponentInParent<Movement>();
    }
    void Update()
    {
        if(movement.loud == true)
        {
            loud.enabled = true;
        }
        else
        {
            loud.enabled = false;
        }
    }
}
