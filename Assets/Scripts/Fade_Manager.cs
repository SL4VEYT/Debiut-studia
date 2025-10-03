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

    private bool savedDirectionIsRight;
    private bool shouldMoveAfterLoad = false;

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
        if (shouldMoveAfterLoad)
        {
            ApplyAutoMove();
        }
        GameManager.Instance.IsTransitioning = true;
        yield return new WaitForSeconds(fadeOutTime); 
        FadeToBlack();
        yield return new WaitForSeconds(fadeSpeed);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        yield return null;
        yield return new WaitForSeconds(waitTime);
        FadeFromBlack(); 
        GameManager.Instance.IsTransitioning = false;
        shouldMoveAfterLoad = false;
    }

    public void StartTransitionWithMove(string sceneName, float fadeDuration, float delayBeforeFadeIn, bool facingRight)
    {
        savedDirectionIsRight = facingRight;
        shouldMoveAfterLoad = true;

        StopAllCoroutines();
        StartCoroutine(TransitionSequence(sceneName, fadeDuration, delayBeforeFadeIn));
    }
    private void ApplyAutoMove()
    {
        GameObject newPlayer = GameObject.FindGameObjectWithTag("Player");

        if (newPlayer != null)
        {
            Rigidbody2D rb = newPlayer.GetComponent<Rigidbody2D>();
            Movement pm = newPlayer.GetComponent<Movement>(); // Assuming your script is named PlayerMovement

            if (rb != null && pm != null)
            {
                // Determine force based on direction (e.g., use the player's max speed)
                float moveForce = pm.speed; // Assumes 'speed' field is accessible in PlayerMovement

                // Set the horizontal velocity instantly
                float xVelocity = savedDirectionIsRight ? moveForce : -moveForce;

                rb.velocity = new Vector2(xVelocity, rb.velocity.y);

                // To prevent the player from stopping right away, you might want 
                // to disable input processing for a fraction of a second here.
            }
        }
    }
}

