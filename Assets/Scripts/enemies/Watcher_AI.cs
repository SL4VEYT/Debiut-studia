using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using UnityEngine.Assertions;

public class Watcher_AI : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    private bool IsTransformed = false;
    public Rigidbody2D Crabloom;
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;

    void ChangeSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D (Collider2D collider)
    {
        ChangeSprite(newSprite);
        
        this.gameObject.AddComponent<BoxCollider2D>();

        IsTransformed = true;
    }

    [Obsolete]
    private void Update()
    {
        if(IsTransformed == true)
        {

            if(GameManager.gameManager.IsPlayerHidden == false)
            {
                target = GameObject.Find("Player").transform;
                Crabloom.constraints = RigidbodyConstraints2D.None;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            
            if (target && GameManager.gameManager.IsPlayerHidden == false)
            {
                Vector2 direction = (target.position - transform.position).normalized;
                moveDirection = direction;
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

            }
        }
    }
    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

}


   
