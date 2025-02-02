using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Checkpoints : MonoBehaviour
{
    public Vector2 Startpos;
    public Animator animator;
    bool deathonlyonce = true;
    
    void Start()
    {
       
        Startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameManager.Player_health.Health == 0)
        {
            if (deathonlyonce == true)
            { animator.SetBool("IsDead", true); }
            deathonlyonce = false;
            Invoke("Die", 0.1f);
            Invoke("Respawn", 2.5f);
            
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", false);
    }
    void Respawn()
    {
        deathonlyonce = true;
        GameManager.gameManager.Player_health.Health = GameManager.gameManager.Player_health.MaxHealth;
        transform.position = Startpos;
    }
}
