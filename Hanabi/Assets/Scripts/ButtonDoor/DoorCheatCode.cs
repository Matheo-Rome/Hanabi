using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheatCode : MonoBehaviour
{
    public DoorSetActive door;
    
    private void Update()
    {
        // Cheat Code ouvre-ferme la porte
        if (Input.GetKeyDown(KeyCode.F))
            door.OpenDoor();
        
        if (Input.GetKeyDown(KeyCode.V))
            door.CloseDoor();
    }
}
