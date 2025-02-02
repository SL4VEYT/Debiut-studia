using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HP_Visual : MonoBehaviour
{
    int HP = GameManager.gameManager.Player_health.MaxHealth;
    public SpriteRenderer spriteRenderer;
    public Sprite drzewo4;
    public Sprite drzewo3;
    public Sprite drzewo2;
    public Sprite drzewo1;
    public Sprite drzewo0;
    bool WasHit = false;

    void Update()
    {
        if (GameManager.gameManager.Player_health.Health == 4)
        {
            if (WasHit == true)
            {
                Invoke("ass", 2f);
                WasHit = false;
            }
        }

        if (GameManager.gameManager.Player_health.Health == 3)
        {
            spriteRenderer.sprite = drzewo3;
        }

        if (GameManager.gameManager.Player_health.Health == 2)
        {
            spriteRenderer.sprite = drzewo2;
        }

        if (GameManager.gameManager.Player_health.Health == 1)
        {
            spriteRenderer.sprite = drzewo1;
        }

        if (GameManager.gameManager.Player_health.Health == 0)
        {

            spriteRenderer.sprite = drzewo0;
            WasHit = true;
        }

        
    }
    void ass()
    {
        spriteRenderer.sprite = drzewo4;
    }
}
