using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semi_Platform : MonoBehaviour
{
    public Ladder ladderScript;
    public bool Fallthrough;

    void Update()
    {
        
            if (ladderScript.IsClimbing == true || Input.GetKey(KeyCode.S))
            {
            Fallthrough = true;
            }
            else
            {
            Fallthrough = false;
        }
    }
}
