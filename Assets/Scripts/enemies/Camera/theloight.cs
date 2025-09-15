using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theloight : MonoBehaviour
{
    private Camera_enemy_horizontal kamera;
    SpriteRenderer spriteRenderer;
    public Sprite biel;
    public Sprite czerwien;
    public ParticleSystem partikle;
    private bool particlesPlayed = false;
    void Start()
    {
        kamera = GetComponentInParent<Camera_enemy_horizontal>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (kamera.See_Player)
        {
            spriteRenderer.sprite = czerwien; 
            if (!particlesPlayed)
            {
                partikle.Play();
                particlesPlayed = true;
            }
            
        }
        else
        {
            spriteRenderer.sprite = biel;
            particlesPlayed = false;
            partikle.Stop();
        }
    }
}
