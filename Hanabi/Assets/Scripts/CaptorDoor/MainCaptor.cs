using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainCaptor : MonoBehaviour
{
    public DoorSetActive door;
    
    public CaptorTrigger captor1;
    public CaptorTrigger captor2;
    public CaptorTrigger captor3;
    public CaptorTrigger captor4;
    public CaptorTrigger captor5;
    public CaptorTrigger captor6;

    [SerializeField] private bool isDown;
    

    // Update is called once per frame
    void Update()
    {
        if (captor1.IsActive && captor2.IsActive && captor3.IsActive && captor4.IsActive && captor5.IsActive &&
            captor6.IsActive)
        {
            door.OpenDoor();
        }

        if (!PhotonNetwork.IsConnected)
        {
            if (isDown)
            {
                if (PlayerMovementSolo.instance.hasFallen)
                {
                    captor1.Desactivate();
                    captor2.Desactivate();
                    captor3.Desactivate();
                    captor4.Desactivate();
                    captor5.Desactivate();
                    captor6.Desactivate();
                }
            }
            else if (PlayerMovementSolo.instance.hasFallen)
            {
                captor1.Desactivate();
                captor2.Desactivate();
                captor3.Desactivate();
                captor4.Desactivate();
                captor5.Desactivate();
                captor6.Desactivate();
            }
        }
        else
        {
            if (PlayerMovement.instance.hasFallen)
            {
                captor1.Desactivate();
                captor2.Desactivate();
                captor3.Desactivate();
                captor4.Desactivate();
                captor5.Desactivate();
                captor6.Desactivate();
            }
        }
    }
    
    
}
