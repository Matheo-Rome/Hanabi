using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RamasserRaspberries : MonoBehaviourPunCallbacks
{
    private GameObject p1;
    private GameObject p2;
    private bool founded = false;
    
    private void Update()
    {
        if (!PhotonNetwork.IsConnected)
        {
            if (!founded)
            {
                p1 = GameObject.FindGameObjectWithTag("Inventaire");
                p2 = GameObject.FindGameObjectWithTag("Inventaire2");
                
                founded = p1 != null && p2 != null;
            }

        }
    }

    //Si le joueur rentre dans la Raspberries, elle s'enlève

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Player1") || col.CompareTag("Player2"))
        {
           
            
            if (!PhotonNetwork.IsConnected)
            {
                p1.GetComponent<inventory>().AddRaspberries(1,false);
                p2.GetComponent<inventory>().AddRaspberries(1,false);
                Destroy(gameObject);
            }
            else
            {
                if (col.gameObject.GetPhotonView().IsMine)
                {
                    GameObject[] inventories = GameObject.FindGameObjectsWithTag("InventaireM");
                    foreach (var inventory in inventories)
                    {
                        inventory.GetComponent<inventory>().AddRaspberries(1, true);
                    }
                }

                Destroy(gameObject);
            }

        }
    }
}
