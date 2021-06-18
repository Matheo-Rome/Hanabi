using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_credit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            new WaitForSeconds(77.7f);
            LoadCredit();
        }
    }

    
    public void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Stay in trigger");
    }

    void LoadCredit()
    {
        List<GameObject> toDestroy = new List<GameObject>();
        if (PhotonNetwork.IsConnected)
        { 
            PhotonNetwork.LeaveRoom(); 
            PhotonNetwork.LeaveLobby(); 
            
        }
        
        toDestroy = GameObject.FindGameObjectsWithTag("Package").ToList();
        
        Destroy(toDestroy[0]); 
        Destroy(toDestroy[1]);

        PhotonNetwork.LoadLevel(65);
        
    }
}
