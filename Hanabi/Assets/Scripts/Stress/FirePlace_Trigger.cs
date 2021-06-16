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
                PlayerStress.instance.isTouchingFire = true;
            else
            {
                PlayerStressSolo.instance.isTouchingFire = true;
            }
            
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") | collider.CompareTag("Player1"))
        {
            
            if (PhotonNetwork.IsConnected)
                PlayerStress.instance.isTouchingFire = false;
            else
            {
                PlayerStressSolo.instance.isTouchingFire = false;
            }
        }
    }
}
