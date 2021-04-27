using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using Unity.Mathematics;

public class PhotonManager : MonoBehaviourPunCallbacks
{

    public Transform SpawnPoint1;
    public Transform SpawnPoint2;
    private Transform sp;
    
    private string prefabname;
    private bool connect;
    
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    
public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions {MaxPlayers = 2},TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {

        /*if (PhotonNetwork.PlayerList.Length <= 1)
        {
            GameObject player1 = PhotonNetwork.Instantiate("ALL_IN_ONE_R", SpawnPoint1.position, 
                quaternion.identity, 0) as GameObject;
            player1.GetComponent<PlayerMovement>().player1 = true;
        }
        else
        {
            GameObject player2 = PhotonNetwork.Instantiate("ALL_IN_ONE_R", SpawnPoint2.position, 
                quaternion.identity, 0) as GameObject;
            player2.GetComponent<PlayerMovement>().player2 = true;
        }*/
        
    }
    
}
