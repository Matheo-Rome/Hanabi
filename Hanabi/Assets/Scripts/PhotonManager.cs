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
    private bool player1;
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

        if (PhotonNetwork.PlayerList.Length <= 1)
        {
            sp = SpawnPoint1;
            prefabname = "roger1";
        }
        else
        {
            sp = SpawnPoint2;
            prefabname = "roger2";
        }
        GameObject player = PhotonNetwork.Instantiate(prefabname, sp.position, quaternion.identity, 0) as GameObject;
    }
    
}
