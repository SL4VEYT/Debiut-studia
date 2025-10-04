using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semi_Platform : MonoBehaviour
{
    public Ladder ladderScript;
    public bool Fallthrough;
    GameObject Player;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        ladderScript = Player.GetComponent<Ladder>();
    }
    void Update()
    {
        
            if (ladderScript.IsClimbing == true || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
            Fallthrough = true;
            }
            else
            {
            Fallthrough = false;
        }
    }
}
