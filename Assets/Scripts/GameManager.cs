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
