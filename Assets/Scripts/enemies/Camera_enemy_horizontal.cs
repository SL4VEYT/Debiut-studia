using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera_enemy_horizontal : MonoBehaviour
{
    public float speed = 0.8f;
    public float range = 2;
    public bool See_Player;
    float startingX;
    public CapsuleCollider2D hitbox;
    int direction = 1;
    void Start()
    {
        startingX = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime * direction);
        if(transform.position.x < startingX || transform.position.x > startingX+range)
        
            direction *= -1;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            See_Player = true;
            hitbox.enabled = true;
        }
        
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            See_Player = false;
            hitbox.enabled = false;
        }
    }

}
