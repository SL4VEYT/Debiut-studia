using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HisFuckingTongue : MonoBehaviour
{
    public Transform playerTransform;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void LookAtTarget()
    {
        Vector3 direction = playerTransform.position - transform.position;
        float distanceX = direction.x; // Get the X distance between enemy and player

        // Check if player is to the left or right of the enemy
        spriteRenderer.flipX = distanceX < 0f;
    }

    
    private void Update()
    {
        LookAtTarget();
    }
}
