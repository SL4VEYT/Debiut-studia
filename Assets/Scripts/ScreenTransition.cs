using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Important

public class ScreenTransition : MonoBehaviour
{
    public string targetSceneName;
    public bool Traveling;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            GameManager manager = GameManager.Instance;

            if (manager != null)
            {

                Traveling = true;


                Fade_Manager fadeManager = manager.GetComponent<Fade_Manager>();

                if (fadeManager != null)
                {
                    manager.GetComponent<Fade_Manager>().StartTransition(targetSceneName, 1.3f, 3.5f);
                }
            }

            this.enabled = false;
        }
    }
}
