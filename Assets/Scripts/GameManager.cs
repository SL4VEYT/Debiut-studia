using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsPlayerHidden;
    public bool IsTransitioning { get; internal set; } = false;

    GameObject Player;
    public static GameManager Instance { get; private set; }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Player = null;
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (Player != null)
        {
            if (Player.GetComponent<SpriteRenderer>().enabled == false)
            {
                IsPlayerHidden = true;

            }
            else
            {
                IsPlayerHidden = false;
            }
        }
    }
    




    public static GameManager gameManager
    {
        get;
        private set;
    }

  /*  private IEnumerator TransitionSequence(string sceneName, float fadeDuration, float delayBeforeFadeIn)
    {
        IsTransitioning = true; 
        Fade_Manager fadeManager = GetComponent<Fade_Manager>();

        if (fadeManager == null)
        {
            Debug.LogError("Fade_Manager component is missing on the GameManager object!");
            IsTransitioning = false;
            yield break; // Stop the coroutine immediately
        }

        fadeManager.FadeToBlack();

        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        yield return null;
        yield return new WaitForSeconds(delayBeforeFadeIn);
        fadeManager.FadeFromBlack();
        yield return new WaitForSeconds(fadeDuration);

        IsTransitioning = false; 
    }*/

    public HP_Player Player_health = new HP_Player(4,4);

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this.gameObject);
        }

      

        if (gameManager != null && gameManager == this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    
}
