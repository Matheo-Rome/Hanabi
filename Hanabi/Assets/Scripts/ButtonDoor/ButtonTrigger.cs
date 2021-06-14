using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    private float timer;
    public DoorSetActive door;
    public SpriteRenderer theSR;
    public Sprite buttonOff;
    public Sprite buttonOn;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")|| collider.CompareTag("Player1") || collider.CompareTag("Player2"))
        {
            theSR.sprite = buttonOn;
            door.OpenDoor();
            timer = 8f;
        }
    }
    
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                door.CloseDoor();
                theSR.sprite = buttonOff;
                
            }
        }
    }
}
