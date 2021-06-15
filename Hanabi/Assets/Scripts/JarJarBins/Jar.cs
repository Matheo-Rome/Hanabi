using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;


public class Jar : MonoBehaviourPunCallbacks
{
    [SerializeField] private Animator Animator;
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject pot;
    private int maxP = 50;
    private Random rnd = new Random();

    private GameObject p1;
    private GameObject p2;
    private bool founded = false;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        Animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            Animator.Play("Jar");
            int add = rnd.Next(0, maxP + 1);
            if (!PhotonNetwork.IsConnected)
            {
                p1.GetComponent<inventory>().Addcoins(add,false);
                p2.GetComponent<inventory>().Addcoins(add,false);
                Destroy(pot, 0.3f);
            }
            else
            {
                if (photonView.IsMine)
                {
                    GameObject[] inventories = GameObject.FindGameObjectsWithTag("InventaireM");
                    foreach (var inventory in inventories)
                    {
                        inventory.GetComponent<inventory>().Addcoins(add,true);
                    }
                    Destroy(pot, 0.3f);
                }
                
            }

            add = rnd.Next(0, maxP + 1);
        }
    }
    
    
}
