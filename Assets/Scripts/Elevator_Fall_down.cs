using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Falldown : MonoBehaviour
{
    public BoxCollider2D Lever;
    private Rigidbody2D rb;
    void Awake()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Lever.enabled == false)
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
