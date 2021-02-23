using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerButton : MonoBehaviour
{
    //[SerializeField] private DoorSetActive door;
    //[SerializeField] private IDoor door;
    
    [SerializeField] private GameObject doorGameObjectA;

    private IDoor doorA;

    private void Awake()
    {
        doorA = doorGameObjectA.GetComponent<IDoor>();
    }
    
    
    //Cheat code pour ouvrir et fermer la porte
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            doorA.OpenDoor();
        if (Input.GetKeyDown(KeyCode.G))
            doorA.CloseDoor();
    }

}
