using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlantPlayer2 : MonoBehaviourPunCallbacks
{
    public bool IsTrigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsTrigger = true;
            if(PhotonNetwork.IsConnected)
                base.photonView.RPC("RPC_PlantP2", RpcTarget.Others,true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsTrigger = false;
            if(PhotonNetwork.IsConnected)
                base.photonView.RPC("RPC_PlantP2", RpcTarget.Others,false);
        }
    }

    [PunRPC]
    private void RPC_PlantP2(bool trigger)
    {
        IsTrigger = trigger;
    }
}
