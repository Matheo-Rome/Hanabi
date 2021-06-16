using System;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Photon.Pun;
using UnityEngine.UI;

public class upgradesInventory : MonoBehaviour
{
    public static upgradesInventory instance;
    public List<upgradesSO> content = new List<upgradesSO>(); // Liste où sont stocké tous les items
    
    private bool founded = false;
    private GameObject p1;
    private GameObject p2;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance d'inventaire d'upgrade dans la scène");
            return;
        }

        instance = this;
    }
    
    public void AddEffectAmelioration(upgradesSO Upgrade)
    {
        //test
        PlayerMovement.instance.jumpVelocity += Upgrade.jumpBoostGiven;

        // Pour la jar
        ValueOfUpgrade.instance.addGiventByJar = Upgrade.coinDropUpgrade;
        
        //Pour le Oscillococcinum
        if (!PhotonNetwork.IsConnected)
        {
            p1.GetComponent<PlayerStressSolo>().maxStress += Upgrade.addMaxStress;
            p2.GetComponent<PlayerStressSolo>().maxStress += Upgrade.addMaxStress;
        }
        
        else
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in players)
            {
                player.GetComponent<PlayerStress>().UpdateMaxStress(Upgrade.addMaxStress);
            }
        }

    }
    public void Update()
    {
        if (!PhotonNetwork.IsConnected)
        {
            if (!founded)
            {
                p1 = GameObject.FindGameObjectWithTag("Player1");
                p2 = GameObject.FindGameObjectWithTag("Player2");
                
                founded = p1 != null && p2 != null;
            }
        }
    }
}
        