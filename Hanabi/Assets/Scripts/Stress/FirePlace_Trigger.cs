using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FirePlace_Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("Player1"))
        {
            if (PhotonNetwork.IsConnected)
                foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if(player.GetPhotonView().IsMine)
                        player.GetComponent<PlayerStress>().isTouchingFire = true;
                }
            else
            {
                GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerStressSolo>().isTouchingFire = true;
            }
            
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") | collider.CompareTag("Player1"))
        {
            
            if (PhotonNetwork.IsConnected)
                foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if(player.GetPhotonView().IsMine)
                        player.GetComponent<PlayerStress>().isTouchingFire = false;
                }
            else
            {
                GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerStressSolo>().isTouchingFire = false;
            }
        }
    }
}
