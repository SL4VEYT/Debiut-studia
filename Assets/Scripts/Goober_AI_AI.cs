using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goober_AI_AI : MonoBehaviour
{
    public float speed = 5f;
    public float changeDirectionDistance = 5f;
    private float leftmostX;
    private float rightmostX;
    private bool isMovingLeft = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        leftmostX = transform.position.x - changeDirectionDistance / 2;
        rightmostX = transform.position.x + changeDirectionDistance / 2;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MoveEnemy();
        FlipSprite();
    }

    void MoveEnemy()
    {
        float movement = isMovingLeft ? -speed * Time.deltaTime : speed * Time.deltaTime;
        transform.Translate(new Vector2(movement, 0f));

        if (isMovingLeft && transform.position.x <= leftmostX)
        {
            isMovingLeft = false;
        }
        else if (!isMovingLeft && transform.position.x >= rightmostX)
        {
            isMovingLeft = true;
        }
    }

    void FlipSprite()
    {
        spriteRenderer.flipX = isMovingLeft;
    }
}
