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

    private bool founded = false;

    public PlayerMovementSolo player;
    private PlayerMovement playerM;


    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsConnected)
        {
            if (!founded)
            {
                if (isDown)
                    player = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerMovementSolo>();
                else
                    player = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerMovementSolo>();
            }
        }
        else
        {
            if (!founded)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                GameObject up;
                GameObject down;
                if (players[0].transform.position.y > players[1].transform.position.y)
                {
                    up = players[0];
                    down = players[1];
                }
                else
                {
                    up = players[1];
                    down = players[0];
                }

                if (isDown)
                    playerM = down.GetComponent<PlayerMovement>();
                else
                    playerM = up.GetComponent<PlayerMovement>();
                founded = playerM != null;
            }
        }

        if (captor1.IsActive && captor2.IsActive && captor3.IsActive && captor4.IsActive && captor5.IsActive &&
            captor6.IsActive)
        {
            door.OpenDoor();
        }

        if (!PhotonNetwork.IsConnected)
        {
            if (player.hasFallen)
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
            if (playerM.hasFallen)
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
