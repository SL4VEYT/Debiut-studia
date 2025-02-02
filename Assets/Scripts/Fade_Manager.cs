using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_Manager : MonoBehaviour
{
    public Image fadeImage; // Reference to the fade image UI element 
    public float fadeSpeed = 0.3f; // Speed of the fade transition (seconds)
    private bool isFading = false;
    private float targetAlpha; // Target alpha value for the fade

    void Start()
    {
        // Ensure fade image starts completely transparent
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
    }

    public void FadeToBlack()
    {
        if (!isFading)
        {
            isFading = true;
            targetAlpha = 1f; // Fade to fully opaque (black)
        }
    }

    public void FadeFromBlack()
    {
        if (!isFading)
        {
            isFading = true;
            targetAlpha = 0f; // Fade to fully transparent (clear)
        }
    }

    void Update()
    {
        
            if (isFading)
            {
                float alpha = Mathf.Lerp(fadeImage.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
                fadeImage.color = new Color(0f, 0f, 0f, alpha);
                if (Mathf.Abs(alpha - targetAlpha) < 0.01f) // Consider the fade complete when near target alpha
                {
                    isFading = false;
                }
            }
        if (GameManager.gameManager.Player_health.Health == 0)
        {
            Invoke("FadeToBlack", 1.3f);
            
        }
        if (targetAlpha == 1f)
        {
            Invoke("FadeFromBlack", 3.5f);
        }
        
            
        

    }
}
