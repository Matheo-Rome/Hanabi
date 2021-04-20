using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCaptor : MonoBehaviour
{
    public DoorSetActive door;
    
    public CaptorTrigger captor1;
    public CaptorTrigger captor2;
    public CaptorTrigger captor3;
    public CaptorTrigger captor4;
    public CaptorTrigger captor5;
    public CaptorTrigger captor6;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (captor1.IsActive && captor2.IsActive && captor3.IsActive && captor4.IsActive && captor5.IsActive &&
            captor6.IsActive)
        {
            door.OpenDoor();
        }
    }
    
}
