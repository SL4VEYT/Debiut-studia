using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    /* public Scren_Shake ss;
     private Vector3 offset = new Vector3(0f, 1.2f, -10f);
     private float smoothTime = 0.25f;
     private Vector3 velocity = Vector3.zero;

     [SerializeField] private Transform target;


     void Update()
     {

             Vector3 targetPosition = target.position + offset;
             transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

     } 
    */
        public Scren_Shake ss;
        private Vector3 offset = new Vector3(0f, 1.2f, -10f);
        private float smoothTime = 0.25f;
        private Vector3 velocity = Vector3.zero;

        [SerializeField] private Transform target;

        void Update()
        {
            Vector3 targetPosition = target.position + offset;


            // Apply screen shake only if it's active
            if (!ss.isScreenShaking)
            {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            // targetPosition += ss.transform.position - ss.target.position; // Add screen shake offset
            }

            
        }
    }

