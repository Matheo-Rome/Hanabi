using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Load_credit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            new WaitForSeconds(0.3f);
            PhotonNetwork.LoadLevel(65);
        }
                
    }
}
