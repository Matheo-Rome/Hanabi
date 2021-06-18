﻿using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    private Text interactUI;
    private Text interactUI2;
    private bool isInRange;

    public Items item;
    public bool isDown;
    private bool founded = false;
    private GameObject test;
    private inventory Inventory;

    

    void Update()
    {
        if (!founded)
        {
            if (PhotonNetwork.IsConnected)
            {
                foreach (var UI in GameObject.FindGameObjectsWithTag("InteractUI"))
                {
                    if (UI.GetPhotonView().IsMine)
                        interactUI = UI.GetComponent<Text>();
                   /* else
                    {
                        interactUI2 = UI.GetComponent<Text>();
                        interactUI2.text = "";
                    }*/
                }

                foreach (var inventaire in GameObject.FindGameObjectsWithTag("InventaireM"))
                {
                    if (inventaire.GetPhotonView().IsMine)
                        Inventory = inventaire.GetComponent<inventory>();
                }
            }
            else
            {
                var UI1 = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
                var UI2 = GameObject.FindGameObjectWithTag("InteractUI2").GetComponent<Text>();
                if (isDown)
                {
                    interactUI = UI1;
                    //interactUI2 = UI2;
                    Inventory = GameObject.FindGameObjectWithTag("Inventaire").GetComponent<inventory>();
                }
                else
                {
                    interactUI = UI2;
                    //interactUI2 = UI1;
                    Inventory = GameObject.FindGameObjectWithTag("Inventaire2").GetComponent<inventory>();
                }
            }
            founded = interactUI != null;
        }

        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            TakeItem();
        }
    }

    void TakeItem()
    {
        Inventory.remplacementItem(item);
        Inventory.updateinventoryImage();
        interactUI.enabled = false;
        //interactUI2.text = "PRESS   E   TO   INTERACT";
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" &&  collision.transform.parent.gameObject.GetPhotonView().IsMine|| collision.CompareTag("Player1")|| collision.CompareTag("Player2"))
        {
            interactUI.enabled = true;
            //interactUI2.enabled = false;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.CompareTag("Player1")|| collision.CompareTag("Player2"))
        {
            interactUI.enabled = false;
            isInRange = false;
        }
    }
}