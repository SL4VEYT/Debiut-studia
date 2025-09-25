using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Technician_AI : MonoBehaviour
{
    public bool Player_in_range;
    public Electric_Box electricbox;
    BoxCollider2D hitbox;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    private void Update()
    {

        if(electricbox.Isfucked == true && hitbox.enabled == true)
        {
            Invoke("FuckOff", 2f);
        }
        if (electricbox.Isfucked == true && hitbox.enabled == false)
        {
            Invoke("FuckIn", 2f);
        }
    }

    void FuckOff()
    {
        hitbox.enabled = false;
        spriteRenderer.enabled = false;
    }

    void FuckIn()
    {
        hitbox.enabled = true;
        spriteRenderer.enabled = true;
    }
}
