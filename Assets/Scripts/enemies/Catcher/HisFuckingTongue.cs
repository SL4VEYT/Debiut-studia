using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HisFuckingTongue : MonoBehaviour
{
    public Transform PlayerTransform;
    public Catcher_AI catcher_AI;
    private BoxCollider2D hitbox;

    public float activationDelay = 2f; // Time until the hitbox activates
    public float attackDuration = 1f; // How long the hitbox stays active

    // A flag to prevent starting multiple coroutines
    private bool isAmbushActive = false;

    void Awake()
    {
        hitbox = GetComponent<BoxCollider2D>();
        if (catcher_AI != null)
        {
            PlayerTransform = catcher_AI.playerTransform;
        }
        if (hitbox != null)
        {
            hitbox.enabled = false;
        }
    }
    void Update()
    {
        // If the Catcher_AI sees the player, start the ambush sequence
        if (catcher_AI != null && catcher_AI.See_Player)
        {
            if (!isAmbushActive)
            {
                isAmbushActive = true;
                StartCoroutine(AmbushSequence());
                StartCoroutine(AttackPhase());
            }
        }
        else
        {
            // If the player is no longer seen, reset the ambush
            if (isAmbushActive)
            {
                ResetAmbush();
            }
        }
    }

    private IEnumerator AmbushSequence()
    {
        // Phase 1: Invisible and following the player
        // The hitbox is still disabled here, but the object itself moves
        while (catcher_AI != null && catcher_AI.See_Player)
        {
            // Keep sticking to the player's position
            if (PlayerTransform != null)
            {
                transform.position = PlayerTransform.position;
            }
            yield return null; // Wait for the next frame
        }

        // The player must have left the range, so the coroutine stops here
        // If the coroutine reached this point, the player left.
        ResetAmbush();
    }

    private IEnumerator AttackPhase()
    {
        // Wait for the delay before the hitbox becomes active
        yield return new WaitForSeconds(activationDelay);

        // Check if the player is still in range before attacking
        if (catcher_AI != null && catcher_AI.See_Player)
        {
            // Phase 2: The hitbox activates
            if (hitbox != null)
            {
                hitbox.enabled = true;
                // You can add damage dealing logic here
            }

            // Wait for the attack duration
            yield return new WaitForSeconds(attackDuration);

            // Phase 3: The hitbox deactivates
            ResetAmbush();
        }
        else
        {
            // If player left before activation, reset immediately
            ResetAmbush();
        }
    }

    private void ResetAmbush()
    {
        isAmbushActive = false;
        StopAllCoroutines(); // Crucial to stop any pending ambush timers

        if (hitbox != null)
        {
            hitbox.enabled = false;
        }

        // Return the tongue to its parent's position
        transform.localPosition = Vector3.zero;
    }
}
