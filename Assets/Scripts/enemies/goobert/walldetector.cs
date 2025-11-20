using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walldetector : MonoBehaviour
{
    public bool Iswall;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.CompareTag("Door"))
        {
            Iswall = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.CompareTag("Door"))
        {
            Iswall = false;
        }
    }
}
