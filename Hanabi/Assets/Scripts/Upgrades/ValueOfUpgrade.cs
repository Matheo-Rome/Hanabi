using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ValueOfUpgrade : MonoBehaviourPunCallbacks
{
    public int AmeliorationJar = 0;
    public int AmelioriationBank = 0;
    public int AmeliorationStress = 200;
    public float AmeliorationFeuDeCamps = 0.6f;
    public int AmeliorationRandomLevel = 0;


    public static ValueOfUpgrade instance;
    public bool toUpdate = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de GameObjectUpgrade dans la scène");
            return;
        }

        instance = this;
    }

    public void Start()
    {
        AmeliorationStress = 200;
        AmeliorationFeuDeCamps = 0.6f;
    }

    private void Update()
    {
        if (toUpdate)
        {
            photonView.RPC("RPC_UpdateVOU", RpcTarget.Others, AmeliorationJar, AmeliorationStress,
                AmeliorationStress, AmeliorationFeuDeCamps, AmeliorationRandomLevel);
            toUpdate = false;
        }
    }

    [PunRPC]
    public void RPC_UpdateVOU(int Jar, int Bank, int Stress, float Feu, int Lvl)
    {
        AmeliorationJar = Jar;
        AmelioriationBank = Bank;
        AmeliorationStress = Stress;
        AmeliorationFeuDeCamps = Feu;
        AmeliorationRandomLevel = Lvl;
    }
}
