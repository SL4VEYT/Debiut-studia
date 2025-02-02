using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dai_ko : MonoBehaviour
{

    public string[] dialogueLines; // Array of dialogue lines for this NPC

    public bool playerInTrigger;
    private GameObject dialogueManagerObject; // Reference to DialogueManager GameObject
    private bool dialogueStarted; // Track if dialogue is currently ongoing
    public GameObject dialoguebox;
    
    void Start()
    {
        dialogueManagerObject = GameObject.Find("DialogueManager"); // Find DialogueManager GameObject
        dialoguebox.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            dialogueStarted = false; // Reset dialogue state when player leaves
            dialoguebox.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInTrigger && !dialogueStarted && Input.GetMouseButtonDown(0)) // Check for left mouse click
        {
            dialoguebox.SetActive(true);
            // Get DialogueManager component
            DialogueManager dialogueManager = dialogueManagerObject.GetComponent<DialogueManager>();
            if (dialogueManager != null)
            {
                dialogueStarted = true; // Mark dialogue as started
                // Start dialogue
                dialogueManager.StartDialogue(dialogueLines);
               
            }
            
            
        }
    }
}
