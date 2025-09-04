using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semi_Platform : MonoBehaviour
{
    public Ladder ladderScript;
    bool Switch = false;

    void Update()
    {
        if (Switch == false)
        {
            if (ladderScript.IsClimbing == true || Input.GetKey(KeyCode.S))
            {
                Physics2D.IgnoreLayerCollision(11, 6, true);
                Switch = true;
            }
        }


        else
        {
            if (Switch == true)
            {
                Physics2D.IgnoreLayerCollision(11, 6, false);
                Switch = false;
            }
        }
    }
}
