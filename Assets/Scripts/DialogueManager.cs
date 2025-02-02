using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{

    public TMP_Text dialogueText; // Text UI element for displaying dialogue
    public float dialogueDelay = 0.4f; // Delay between displaying lines (seconds)
    public string TagToDisable;

    private int currentLineIndex = 0; // Index of the current line being displayed

    public void StartDialogue(string[] dialogueLines)
    {
        currentLineIndex = 0; // Reset index for new dialogue
        
        StartCoroutine(DisplayDialogue(dialogueLines));
    }

    IEnumerator DisplayDialogue(string[] dialogueLines)
    {
        for (int i = 0; i < dialogueLines.Length; i++)
        {
            dialogueText.text = dialogueLines[i];
            yield return new WaitForSeconds(dialogueDelay);

            // Check for user input (e.g., left mouse click)
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("mouse click noticed");
                break; // Exit the coroutine if clicked
            }

            if(i == dialogueLines.Length - 2)
            {
                disablebox();
            }
            
        }

        
        
    }

    void disablebox()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(TagToDisable);
        foreach (GameObject go in taggedObjects)
        {
            go.SetActive(false);
        }
    }
}
