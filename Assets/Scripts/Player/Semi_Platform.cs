using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semi_Platform : MonoBehaviour
{
    Ladder ladderScript;
    public bool Fallthrough;

    private void Awake()
    {
        ladderScript = GetComponent<Ladder>();
    }
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
