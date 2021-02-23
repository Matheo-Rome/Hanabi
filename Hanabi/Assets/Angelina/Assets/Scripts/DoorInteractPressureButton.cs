using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractPressureButton : MonoBehaviour
{

   /* [SerializeField] private GameObject doorGameObject;

    private IDoor door;
    private float timer;

    private void Awake()
    {
        door = doorGameObject.GetComponent<IDoor>();
    }

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

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            timer = 1f;
        }
    }*/
   
   
   private PlayerMovement player;

   //portefermée
   public SpriteRenderer theSR;
   //porte ouverte
   public Sprite doorOpenSprite;
    

   public bool doorOpen, waitingToOpen;
   private float timer;

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
               gameObject.SetActive(true);
           }
       }
   }

   private void OnTriggerEnter2D(Collider2D collider)
   {
       if (collider.CompareTag("Player"))
       {
           timer = 5f;
           doorOpen = true;
           
           theSR.sprite = doorOpenSprite;
           
           gameObject.SetActive(false);
       }
   }
   
}
