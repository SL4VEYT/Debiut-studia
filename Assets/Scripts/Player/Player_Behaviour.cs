using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behaviour : MonoBehaviour
{
    bool A = false;

    public Scren_Shake ss;

    public void OnCollisionEnter2D(Collision2D enemy) 
    {
        if (enemy.gameObject.layer == 8)
        {
            
            A = true;
        }
    }
   
    void Update()
    {
        if(A == true || Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayerTakesDamage(1);
            A = false;
            Debug.Log("Player Took Damage!");
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PlayerHeals(1);
            Debug.Log("Player Was Healed!");
        }
    }

    private void PlayerTakesDamage(int DMG)
    {
        GameManager.gameManager.Player_health.DmgUnit(DMG);
        ss.TriggerScreenShake();
    }

    private void PlayerHeals(int HEAL)
    {
        GameManager.gameManager.Player_health.HealUnit(HEAL);
        
        
    }

    

    
}
