using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Important

public class ScreenTransition : MonoBehaviour
{
    private Movement playerMovementScript;

    private Transform playerTarget;
    public string targetSceneName;
    public bool Traveling;
    private Camera_Follow kamera;
    private Collider2D triggerCollider; 
    private bool triggerActivated = false; 
    public float resetDistance = 10f;
    private void Start()
        {
        triggerCollider = GetComponent<Collider2D>();
        Camera mainCam = Camera.main;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerMovementScript = playerObject.GetComponent<Movement>();
            playerTarget = playerObject.transform;
        }
        if (mainCam != null)
            {
                kamera = mainCam.GetComponent<Camera_Follow>();
            }
        }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && !triggerActivated)
        {
            GameManager manager = GameManager.Instance;
            triggerActivated = true;

            if (manager != null)
            {

                Traveling = true;


                Fade_Manager fadeManager = manager.GetComponent<Fade_Manager>();

                if (fadeManager != null)
                {
                    bool facingRight = playerMovementScript != null ? playerMovementScript.FacingRight : true;
                    fadeManager.StartTransitionWithMove(
                   targetSceneName,
                   1.3f,
                   3.5f,
                   facingRight 
               );
                }
            }
        }  
    }

    void LateUpdate()
    {
        if (triggerActivated || kamera == null || triggerCollider == null || playerTarget == null)
        {
            return;
        }

        float boundaryX = triggerCollider.bounds.min.x;
        float cameraRightEdgeX = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;

        if (!kamera.isStopped)
        {
            if (cameraRightEdgeX >= boundaryX)
            {
                kamera.isStopped = true;
            }
        }
        else 
        {
            float cameraCurrentX = Camera.main.transform.position.x;

            // Condition: Player is behind the frozen camera position AND is behind the reset buffer.
            if (playerTarget.position.x < cameraCurrentX - resetDistance)
            {
                kamera.isStopped = false;
            }
        }
    }
}
