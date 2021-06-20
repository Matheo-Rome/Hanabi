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
            SaveData saveData = collision.gameObject.transform.parent.gameObject.GetComponent<SaveData>();
            saveData.Save();
            upgradesInventory.instance.addUpgradeItems();
            new WaitForSeconds(0.3f);
            PhotonNetwork.LoadLevel(1);
        }
    }
}
