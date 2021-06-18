using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;


public class Jar : MonoBehaviourPunCallbacks
{
    [SerializeField] private Animator Animator;
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject pot;
    public float maxP;

    private GameObject p1;
    private GameObject p2;
    private bool founded = false;

    private void Start()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        collider = GetComponent<BoxCollider2D>();
        Animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxP = 3;
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
            int add = (int) Random.Range(0,maxP+1+ GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>().AmeliorationJar);
            if (!PhotonNetwork.IsConnected)
            {
                p1.GetComponent<inventory>().Addcoins(add,false);
                p2.GetComponent<inventory>().Addcoins(add,false);
                Destroy(gameObject, 0.3f);
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
                }

                Destroy(gameObject, 0.3f);

            }
        }
    }
}