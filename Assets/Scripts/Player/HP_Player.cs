using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Player 

{
    int CurrentHealth;
    int CurrentMaxHealth;

    void Start() // Required for coroutines in MonoBehaviour
    {

    }
    public int Health
    {
        get
        {
            return CurrentHealth;
        }
        set
        {
            CurrentHealth = value;
        }
    }
    public int MaxHealth
    {
        get
        {
            return CurrentMaxHealth;
        }
        set
        {
            CurrentMaxHealth = value;
        }
    }

    public HP_Player(int Health, int maxHealth)
    {
        CurrentHealth = Health;
        CurrentMaxHealth = maxHealth;
    }

    public void DmgUnit(int DamageAmount)
    {
        if(CurrentHealth > 0)
        {
            CurrentHealth -= DamageAmount;
        }
    }

    public void HealUnit(int HealAmount)
    {
        if (CurrentHealth < CurrentMaxHealth)
        {
            CurrentHealth += HealAmount;
        }

        if (CurrentHealth > CurrentMaxHealth)
        {
            CurrentHealth = CurrentMaxHealth;
        }
        

    }

    

    public void SetBack()
    {
        
        
    }
}
