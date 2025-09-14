using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Technician_AI : MonoBehaviour
{
    public bool Player_in_range;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.layer == 6)
        {
            Player_in_range = true;
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.gameObject.layer == 6)
        {
            Player_in_range = false;
        }
    }



}
