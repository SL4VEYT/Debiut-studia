using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel_Hide : MonoBehaviour
{
    bool PlayerTouch = false;
    public GameObject player;
    public bool touch;
    private bool isPlayerHiding = false;

    // A public property to allow other scripts to check the hiding state
    public bool IsHiding()
    {
        return isPlayerHiding;
    }

    public void OnTriggerEnter2D(Collider2D Player)
    {
        if(Player.CompareTag("Player"))
        {
            PlayerTouch = true;
            touch = true;
        }
        
        
    }

    public void ForceUnhide()
    {
        if (isPlayerHiding)
        {
            player.GetComponent<SpriteRenderer>().enabled = true;
            Physics2D.IgnoreLayerCollision(8, 6, false);
            isPlayerHiding = false;
            // You might want to add a player "unhide" event here, like an animation or sound.
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
            isPlayerHiding = true;

        }
        if (isPlayerHiding && (PlayerTouch == false || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)))

        {
                
            player.GetComponent<SpriteRenderer>().enabled = true;
            Physics2D.IgnoreLayerCollision(8,6, false);
            isPlayerHiding = false;
        }

    }
}
