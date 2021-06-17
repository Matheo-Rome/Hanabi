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
    
    /*private bool founded = false;
    private GameObject p1;
    private GameObject p2;*/
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
        if(Upgrade.name.Contains("Midas"))
        {
            ValueOfUpgrade.instance.AmeliorationJar += 1;
        }
        
        else if(Upgrade.name.Contains("FeudecampStonks"))
        {
            ValueOfUpgrade.instance.AmeliorationFeuDeCamps += 0.1f;
        }
        
        else if(Upgrade.name.Contains("Bank"))
        {
            ValueOfUpgrade.instance.AmelioriationBank += 25;
        }
        
        else if(Upgrade.name.Contains("Oscillococcinum"))
        {
            ValueOfUpgrade.instance.AmeliorationStress += 20;
        }
        
    /*//test
    PlayerMovement.instance.jumpVelocity += Upgrade.jumpBoostGiven;

    // Pour la jar
    ValueOfUpgrade.instance.addGiventByJar += Upgrade.coinDropUpgrade;
    
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
    
    //Pour la banque
    GameObject[] packages = GameObject.FindGameObjectsWithTag("Package");
    foreach (var package in packages)
    {
        package.GetComponent<SaveData>().UpdateBank(Upgrade.addMaxBank,true);
    }
    
    //Pour le feu de camp
    if (PhotonNetwork.IsConnected)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        players[0].GetComponent<PlayerStress>().firecampValue += Upgrade.FireCamp;
        int saveStress = (int)(players[0].GetComponent<PlayerStress>().currentStress * (players[0].GetComponent<PlayerStress>().firecampValue));
        foreach (var player in players)
        {
            player.GetComponent<PlayerStress>().HealStressplayer(player.GetComponent<PlayerStress>().currentStress - saveStress);
        }
    }

    else
    {
        int saveStress = (int)(p1.GetComponent<PlayerStress>().currentStress * (p1.GetComponent<PlayerStress>().firecampValue));
        p1.GetComponent<PlayerStress>().HealStressplayer(p1.GetComponent<PlayerStress>().currentStress - saveStress);
        p2.GetComponent<PlayerStress>().HealStressplayer(p2.GetComponent<PlayerStress>().currentStress - saveStress);
    }*/
    }
    /*public void Update()
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
    }*/
}
        