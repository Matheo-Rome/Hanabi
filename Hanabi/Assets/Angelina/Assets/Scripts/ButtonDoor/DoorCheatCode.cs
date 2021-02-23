using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerButton : MonoBehaviour
{
    [SerializeField] private DoorSetActive _doorSetActive;
    //IDoor GameObject

    private void Update()
    {
        // Cheat Code ouvre-ferme la porte
        if (Input.GetKeyDown(KeyCode.F))
            _doorSetActive.OpenDoor();
        
        if (Input.GetKeyDown(KeyCode.G))
            _doorSetActive.CloseDoor();
    }
}
