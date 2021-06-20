using System;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class ExitPermanentShop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            upgradesInventory.instance.addUpgradeItems();
            new WaitForSeconds(0.9f);
            PhotonNetwork.LoadLevel(1);
        }
    }
}
