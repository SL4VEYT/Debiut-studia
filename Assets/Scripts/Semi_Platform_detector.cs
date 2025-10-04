using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semi_Platform_detector : MonoBehaviour
{
    Semi_Platform Asshole;
    public BoxCollider2D platform;
    bool donotclip = false;

    private void Awake()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        Asshole = playerObject.GetComponent<Semi_Platform>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (Asshole.Fallthrough == true)
            {
                platform.enabled = false;
                donotclip = true;
            }

            else
            {
                if (donotclip == false)
                {
                    platform.enabled = true; 
                } 
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            donotclip = false;
        }
    }
}
