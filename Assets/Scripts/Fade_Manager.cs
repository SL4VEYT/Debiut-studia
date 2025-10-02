using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade_Manager : MonoBehaviour
{
    public Image fadeImage; // Reference to the fade image UI element 
    public float fadeSpeed = 0.3f; // Speed of the fade transition (seconds)
    private bool isFading = false;
    private float targetAlpha; // Target alpha value for the fade

    GameObject[] ST;
    void Start()
    {
        // Ensure fade image starts completely transparent
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
        ST = GameObject.FindGameObjectsWithTag("SCreenT");
    }

    public void StartTransition(string sceneName, float fadeOutTime, float waitTime)
    {
        StartCoroutine(TransitionSequence(sceneName, fadeOutTime, waitTime));
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ST = null;
        ST = GameObject.FindGameObjectsWithTag("SCreenT");
    }

    void Update()
    {
        if (ST == null) return;


        foreach (GameObject obj in ST)
        {

            if (obj == null) continue; // Skip to the next iteration if the object is gone

            ScreenTransition script = obj.GetComponent<ScreenTransition>();

            // You should also check if the script is null, just in case
            if (script == null) continue;


            if (script.Traveling == true)
            {
                Invoke("FadeToBlack", 1.3f);

            }
            if (targetAlpha == 1f)
            {
                Invoke("FadeFromBlack", 3.5f);
            }
        }


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


            private IEnumerator TransitionSequence(string sceneName, float fadeOutTime, float waitTime)
    {
        GameManager.Instance.IsTransitioning = true;
        // 1. Fade Out (using the delay from the trigger)
        yield return new WaitForSeconds(fadeOutTime); // Wait for the 1.3 seconds delay
        FadeToBlack(); // Sets targetAlpha = 1f

        // Wait until the fade actually completes (assuming fadeSpeed is your transition time)
        yield return new WaitForSeconds(fadeSpeed);

        // 2. Load the New Scene
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

        // Wait one frame to ensure the new scene is initialized
        yield return null;

        // 3. Wait for the final delay (3.5 seconds)
        yield return new WaitForSeconds(waitTime);

        // 4. Fade In
        FadeFromBlack(); // Sets targetAlpha = 0f
        GameManager.Instance.IsTransitioning = false;
    }


}

