using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsPlayerHidden;
    public GameObject Player;

    private void Update()
    {
        if(Player.GetComponent<SpriteRenderer>().enabled == false)
        {
            IsPlayerHidden = true;
            
        }
        else
        {
            IsPlayerHidden = false;
        }    
    }




    public static GameManager gameManager
    {
        get;
        private set;
    }

    public HP_Player Player_health = new HP_Player(4,4);

    void Awake()
    {
        if(gameManager != null && gameManager == this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }

    
}
