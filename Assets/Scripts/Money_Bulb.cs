using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money_Bulb : MonoBehaviour
{
    bool open = false;
    public Sprite Opened;
    public int value;
    public ParticleSystem Particles;

    GameObject Currency_Counter;

    private void Start()
    {
        Particles.Stop();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (open == false)
        {
            if (collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
            {
                Currency_interface.Current_Cells = Currency_interface.Current_Cells + value;

                Particles.Play();
                open = true;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
        if (open == true)
        {
            GetComponent<SpriteRenderer>().sprite = Opened;
        }
        if(Input.GetKey(KeyCode.V))
        {
            Debug.Log("The Player now has " + Currency_interface.Current_Cells + " Cells!");
        }
    }
}
