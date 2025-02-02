using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel_Hide : MonoBehaviour
{
    bool PlayerTouch = false;
    public GameObject player;
    public bool touch;

    public void OnTriggerEnter2D(Collider2D Player)
    {
        if(Player.CompareTag("Player"))
        {
            PlayerTouch = true;
            touch = true;
        }
        
        
    }
    

    public void OnTriggerStay2D(Collider2D Player)
    {
        if (Player.CompareTag("Player"))
        {
            PlayerTouch = true;
            touch = true;
        }


    }

    private void OnTriggerExit2D(Collider2D Player)
    {
        if(Player.CompareTag("Player"))
        {
            PlayerTouch = false;
            touch = false;
        }
    }



    void FixedUpdate()
    {
        if (PlayerTouch == true && Input.GetKey(KeyCode.F))
        {
            
            player.GetComponent<SpriteRenderer>().enabled = false;
            
            Physics2D.IgnoreLayerCollision(8,6, true);
           
        }
        if (PlayerTouch == false || player.GetComponent<SpriteRenderer>().enabled == false && Input.GetKey(KeyCode.W) || player.GetComponent<SpriteRenderer>().enabled == false && Input.GetKey(KeyCode.D) || player.GetComponent<SpriteRenderer>().enabled == false && Input.GetKey(KeyCode.A))
        {
            
            player.GetComponent<SpriteRenderer>().enabled = true;
            Physics2D.IgnoreLayerCollision(8,6, false);
        }

    }
}
