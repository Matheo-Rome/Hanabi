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
        
        //Random.org
        int[] broken = new[] {16, 9, 4, 20, 12, 1};
        int[] used = new[] {17, 11, 5, 21, 13, 2};
        int[] fresh = new[] {18, 10, 6, 22, 14, 3};
        switch (Upgrade.givenObjectLevel)
        {
            case 1 :
                var item1 = ScriptableObject.CreateInstance<Items>();
                InventairePassif.instance.content.Add(item1);
                InventairePassif.instance.Start();
                InventairePassif.instance.AddEffectItem(item1);
                break;
            case 2 :
                var item2 = ScriptableObject.CreateInstance<Items>();
                InventairePassif.instance.content.Add(item2);
                InventairePassif.instance.Start();
                InventairePassif.instance.AddEffectItem(item2);
                break;
            case 3 :
                var item3 = ScriptableObject.CreateInstance<Items>();
                InventairePassif.instance.content.Add(item3);
                InventairePassif.instance.Start();
                InventairePassif.instance.AddEffectItem(item3);
                break;
            case 4 :
                var item4 = ScriptableObject.CreateInstance<Items>();
                InventairePassif.instance.content.Add(item4);
                InventairePassif.instance.Start();
                InventairePassif.instance.AddEffectItem(item4);
                break;
            case 5 :
                var item5 = ScriptableObject.CreateInstance<Items>();
                InventairePassif.instance.content.Add(item5);
                InventairePassif.instance.Start();
                InventairePassif.instance.AddEffectItem(item5);
                break;
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
        