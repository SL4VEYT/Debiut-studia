using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goober_ass : MonoBehaviour
{
    public bool Player_Backdoor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player_Backdoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player_Backdoor = false;
        }
    }
}
