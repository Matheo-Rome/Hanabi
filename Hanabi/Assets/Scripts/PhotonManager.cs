﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    
<<<<<<< HEAD
    private bool connect;

    public Transform SpawnPoint1;
    public Transform SpawnPoint2;
    private Transform sp;


=======
>>>>>>> parent of 02ecb0b (Multi)
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
<<<<<<< HEAD
        /*if (PhotonNetwork.PlayerList.Length <= 1)
        {
            sp = SpawnPoint1;
        }
        else
        {
            sp = SpawnPoint2;
        }*/
        PhotonNetwork.Instantiate("roger", SpawnPoint1.position, quaternion.identity, 0);
=======
        PhotonNetwork.Instantiate("roger", new Vector2(0,0),Quaternion.identity);
        
>>>>>>> parent of 02ecb0b (Multi)
    }
}
