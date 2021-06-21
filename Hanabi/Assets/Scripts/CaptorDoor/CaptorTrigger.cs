using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class CaptorTrigger : MonoBehaviour
{
    public SpriteRenderer theSR;
    public Sprite buttonOff;
    public Sprite buttonOn;
    private BoxCollider2D collider2D;

    private bool founded = false;
    private MainCaptor MainCaptor;

    public bool IsActive;
    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        MainCaptor = GetComponentInParent<MainCaptor>();
    }
    

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("Player1") || collider.CompareTag("Player2"))
        {
            theSR.sprite = buttonOn;
            IsActive = true;
            collider2D.enabled = false;
            if (!PhotonNetwork.IsConnected)
            {
                /*bool t = PlayerMovementSolo.instance.hasDashed;
                bool t2 = PlayerMovementSolo.otherinstance.hasDashed;*/
                /*if (collider.CompareTag("Player1"))
                         PlayerMovementSolo.instance.hasDashed = false;
                else
                    PlayerMovementSolo.otherinstance.hasDashed = false;*/
                MainCaptor.player.hasDashed = false;
            }
            else
                collider.gameObject.GetComponent<PlayerMovement>().hasDashed = false;
        }
    }

    public void Desactivate()
    {
        IsActive = false;
        theSR.sprite = buttonOff;
        collider2D.enabled = true;
    }
}
