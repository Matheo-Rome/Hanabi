using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.Mathematics;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    
    private bool connect;

    public Transform SpawnPoint1;
    public Transform SpawnPoint2;
    private Transform sp;

    public Camera cam1;
    public Camera cam2;
  
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
        if (PhotonNetwork.PlayerList.Length <= 1)
        {
            sp = SpawnPoint1;

        }
        else
        {
            sp = SpawnPoint2;
        }
        PhotonNetwork.Instantiate("roger", sp.position, quaternion.identity, 0);
    }
}
