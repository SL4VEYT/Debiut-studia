using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    bool PlayerTouch;
    public AudioSource a;
    public AudioClip b;
    public Sprite newSprite;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D hitbox;
    void Start()
    {
        
    }

    public void OnTriggerStay2D(Collider2D Player)
    {
        if (Player.gameObject.layer == 6)
        {
            PlayerTouch = true;
        }
        else
        {
            PlayerTouch = false;
        }
    }
    void Update()
    {
        if (PlayerTouch == true && Input.GetButton("Interact"))
        {
            if (hitbox.enabled == true)
            { a.PlayOneShot(b); }
            spriteRenderer.sprite = newSprite;
            hitbox.enabled = false;
        }
    }
}
