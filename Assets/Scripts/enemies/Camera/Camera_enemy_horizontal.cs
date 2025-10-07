using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera_enemy_horizontal : MonoBehaviour
{

    public Transform[] waypoints;
    public float waitTime = 1f;
    private int currentWaypointIndex = 0;
    private bool isPatrolling = true; 
    private bool isWaiting = false;

    public float speed = 2f;
    public float faster_speed = 5f;
    public float range;
    public bool See_Player;
    public float chaseOffset; 
    float startingX;
    public CapsuleCollider2D hitbox;
    int direction;

    public bool StartLeft = false;
    float effectiveRange;
    float effectiveoffset;

    private Transform playerTransform;
    private float minPatrolX;
    private float maxPatrolX;

    void Awake()
    {

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("Camera Enemy is missing waypoints!");
            enabled = false;
            return;
        }

        startingX = transform.position.x;
        if (StartLeft)
        {
            direction = -1; // Start moving to the left
            effectiveRange = -range;
            effectiveoffset = -chaseOffset;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        else
        {
            effectiveoffset = chaseOffset;
            direction = 1;
            effectiveRange = range;// Start moving to the right (default)
        }

        minPatrolX = float.MaxValue;
        maxPatrolX = float.MinValue;

        foreach (Transform waypoint in waypoints)
        {
            if (waypoint.position.x < minPatrolX)
            {
                minPatrolX = waypoint.position.x;
            }
            if (waypoint.position.x > maxPatrolX)
            {
                maxPatrolX = waypoint.position.x;
            }
        }
    }

    void Update()
    {
        if (!See_Player)
        {
            Patrol();
        }
    }

    void FixedUpdate()
    {
        if (See_Player)
        {
            Chase();
        }
    }

    private void Patrol()
    {
        if (isPatrolling && !isWaiting)
        {
            Vector2 targetPosition = waypoints[currentWaypointIndex].position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                isWaiting = true; // Stop movement
                Invoke(nameof(StartPatrolling), waitTime);
            }
        }
    }

    private void StartPatrolling()
    {
        // Select the next waypoint index
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        isWaiting = false; // Resume movement
    }

    private void Chase()
    {
        if (playerTransform != null)
        {
            // Stop patrol movement while chasing
            isWaiting = false;
            isPatrolling = false;

            // 1. Calculate the initial target X position (player's X + offset)
            float directionSign = Mathf.Sign(playerTransform.position.x - transform.position.x);
            float effectiveOffset = chaseOffset * directionSign;

            float targetX = playerTransform.position.x + effectiveOffset;

            // --- CRITICAL FIX: Clamp the target position to the rail bounds ---
            targetX = Mathf.Clamp(targetX, minPatrolX, maxPatrolX);

            Vector2 targetPosition = new Vector2(targetX, transform.position.y);

            // 2. Move towards the clamped target X position
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, faster_speed * Time.deltaTime);
        }
        else
        {
            See_Player = false;
            isPatrolling = true;
            hitbox.enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) 
        {
            if (!See_Player)
            {
                See_Player = true;
                hitbox.enabled = true;
                playerTransform = collision.gameObject.transform;
                CancelInvoke(nameof(StartPatrolling));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Invoke(nameof(forgor), 1f);
        }
    }

    private void forgor()
    {
        See_Player = false;
        isPatrolling = true;
        playerTransform = null;
        hitbox.enabled = false;
    }
}
