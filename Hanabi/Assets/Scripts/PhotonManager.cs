using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
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
    private Camera cam;
    private Camera came;


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
            cam = cam1;
            came = cam2;
        }
        else
        {
            sp = SpawnPoint2;
            cam = cam2;
            came = cam1;
        }
        GameObject player = PhotonNetwork.Instantiate("roger", sp.position, quaternion.identity, 0) as GameObject;
        cam.enabled = true;
        came.enabled = false;
    }
}
