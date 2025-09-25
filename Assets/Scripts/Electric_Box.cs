using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric_Box : MonoBehaviour
{
    bool PlayerTouch;
    public bool Isfucked;
    SpriteRenderer spriteRenderer;
    BoxCollider2D hitbox;
    public float CDDuration = 2.0f;
    void Start()
    {
       hitbox = GetComponent<BoxCollider2D>();
       spriteRenderer = GetComponent<SpriteRenderer>();
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
            { 
                Isfucked = true;
                Invoke("UnFucker", 0.1f);
                StartCoroutine(CD());
            }
               
        }
    }

    public IEnumerator CD()
    {
        hitbox.enabled = false;
        yield return new WaitForSeconds(CDDuration);
        hitbox.enabled = true;
        
    }
    void UnFucker()
    {
        Isfucked = false;
    }
}
