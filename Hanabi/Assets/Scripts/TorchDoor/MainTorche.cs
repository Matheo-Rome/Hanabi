using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTorche : MonoBehaviour
{
    public DoorSetActive door;
    
    public TorchTrigger torch1;
    public TorchTrigger torch2;
    public TorchTrigger torch3;
    public TorchTrigger torch4;
    public TorchTrigger torch5;

    // Start is called before the first frame update
    void Start()
    {
        torch1.IsActive = false;
        torch2.IsActive = false;
        torch3.IsActive = false;
        torch4.IsActive = false;
        torch5.IsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (torch1.IsActive)
        {
            if (torch2.IsActive)
            {
                if (torch3.IsActive)
                {
                    if (torch4.IsActive)
                    {
                        if (torch5.IsActive)
                            door.OpenDoor();
                    }
                }
                else if (torch4.IsActive || torch5.IsActive)
                    LightOff();
            }
            else if (torch3.IsActive || torch4.IsActive || torch5.IsActive)
                LightOff();
        }
        else if (torch2.IsActive || torch3.IsActive ||torch4.IsActive || torch5.IsActive)
           LightOff();
    }

    public void LightOff()
    {
        torch1.Off();
        torch2.Off();
        torch3.Off();
        torch4.Off();
        torch5.Off();
    }
    
    
}
