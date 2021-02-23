using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractPressureButton : MonoBehaviour
{

    /*[SerializeField] private GameObject doorGameObject;
    private IDoor door;
    
    private float timer;
    

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                door.CloseDoor();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            door.OpenDoor();
            timer = 5f;
        }
    }
   
   /*/
   private PlayerMovement player;
   
   private float timer;

   public bool ButtonActive = false;

   void Start()
   {
       player = FindObjectOfType<PlayerMovement>();
   }

   private void Update()
   {
       if (timer > 0)
       {
           timer -= Time.deltaTime;
           if (timer <= 0f)
           {
               ButtonActive = false;
           }
       }
   }

   private void OnTriggerEnter2D(Collider2D collider)
   {
       if (collider.CompareTag("Player"))
       {
           timer = 5f;
           ButtonActive = true;

       }
   }
   
}
