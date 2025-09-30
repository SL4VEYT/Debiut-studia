using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Important

public class ScreenTransition : MonoBehaviour
{
    public string targetSceneName;
    private Fade_Manager fade;

    private void Start()
    {
        fade = FindObjectOfType<Fade_Manager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            fade.FadeToBlack();
            SceneManager.LoadScene(targetSceneName, LoadSceneMode.Single);
            
        }
    }
}
