using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
        public Scren_Shake ss;
        private Vector3 offset = new Vector3(0f, 1.2f, -10f);
        private float smoothTime = 0.25f;
        private Vector3 velocity = Vector3.zero;
        GameObject player;
        public bool isStopped = false;

        [SerializeField] private Transform target;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        ss = GetComponent<Scren_Shake>();
    }
    private void LateUpdate()
    {
        if (isStopped)
        {
            //velocity = Vector3.zero;
            velocity.x = 0f;
            velocity.z = 0f;
            return;
        }
        
            if (target != null && !isStopped)
            {
                Vector3 targetPosition = target.position + offset;

                if (!ss.isScreenShaking)
                {
                    transform.position = Vector3.SmoothDamp(
                        transform.position,
                        targetPosition, 
                        ref velocity,
                        smoothTime
                    );
                }
            }
        
        
    }
}

