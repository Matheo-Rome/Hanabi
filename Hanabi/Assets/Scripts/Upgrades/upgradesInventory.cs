using System;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Photon.Pun;
using UnityEngine.UI;
using Random = System.Random;

public class upgradesInventory : MonoBehaviour
{
    public static upgradesInventory instance;
    public List<upgradesSO> content = new List<upgradesSO>(); // Liste où sont stocké tous les items
    public List<Items> items = new List<Items>();
    
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
        ValueOfUpgrade valueOfUpgrade = GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>();
        if(Upgrade.name.Contains("Midas"))
        { 
            valueOfUpgrade.AmeliorationJar++;
            //ValueOfUpgrade.instance.AmeliorationJar += 1;
        }
        
        else if(Upgrade.name.Contains("FeudecampStonks"))
        {
            valueOfUpgrade.AmeliorationFeuDeCamps -= 0.1f;
            //ValueOfUpgrade.instance.AmeliorationFeuDeCamps += 0.1f;
        }
        
        else if(Upgrade.name.Contains("Bank"))
        {
            valueOfUpgrade.AmelioriationBank += 25;
            //ValueOfUpgrade.instance.AmelioriationBank += 25;
        }
        
        else if(Upgrade.name.Contains("Oscillococcinum"))
        {
            valueOfUpgrade.AmeliorationStress += 20;
            //ValueOfUpgrade.instance.AmeliorationStress += 20;
        }
        
        else if (Upgrade.name.Contains("Random"))
        {
            valueOfUpgrade.AmeliorationRandomLevel++;
            //ValueOfUpgrade.instance.AmeliorationRandomLevel++;
        }
        /*if (PhotonNetwork.IsConnected)
            ValueOfUpgrade.instance.toUpdate = true;*/

        /*// Pour la jar
        ValueOfUpgrade.instance.AmeliorationJar += Upgrade.coinDropUpgrade;

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
            int saveStress = (int) (players[0].GetComponent<PlayerStress>().currentStress *
                                    (players[0].GetComponent<PlayerStress>().firecampValue));
            foreach (var player in players)
            {
                player.GetComponent<PlayerStress>()
                    .HealStressplayer(player.GetComponent<PlayerStress>().currentStress - saveStress);
            }
        }
        
        else 
        {
            int saveStress = (int)(p1.GetComponent<PlayerStress>().currentStress * (p1.GetComponent<PlayerStress>().firecampValue));
            p1.GetComponent<PlayerStress>().HealStressplayer(p1.GetComponent<PlayerStress>().currentStress - saveStress);
            p2.GetComponent<PlayerStress>().HealStressplayer(p2.GetComponent<PlayerStress>().currentStress - saveStress);
        }
        */
    }

    public void addUpgradeItems()
    {
        //Random.org
        var rd = new Random();
        int[] broken = new[] {16, 9, 4, 20, 12, 1};
        int[] used = new[] {17, 11, 5, 21, 13, 2};
        int[] fresh = new[] {18, 10, 6, 22, 14, 3};
        var nb1 = rd.Next(6);
        var nb2 = rd.Next(6);
        switch (ValueOfUpgrade.instance.AmeliorationRandomLevel)
        { 
            case 1 : 
                var item1 = items[broken[nb1] - 1];
                if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
                {
                    GameObject[] IPs = GameObject.FindGameObjectsWithTag("IP");
                    IPs[0].GetComponent<InventairePassif>().AddEffectItem(item1,true);
                    foreach (var Ip in IPs)
                    {
                        Ip.GetComponent<InventairePassif>().ContentAdd(item1,true);
                        Ip.GetComponent<InventairePassif>().UpdateImage(true);
                    }
                }
                else
                {
                    InventairePassif IP1 = GameObject.FindGameObjectWithTag("IP").GetComponent<InventairePassif>();
                    InventairePassif IP2 = GameObject.FindGameObjectWithTag("IP2").GetComponent<InventairePassif>();
                    IP1.content.Add(item1);
                    IP2.content.Add(item1);
                    IP1.Start();
                    IP2.Start();
                    IP1.AddEffectItem(item1, true);
                }
                break;
            case 2 : 
                var item2 = items[used[nb1] - 1];
                if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
                {
                    GameObject[] IPs = GameObject.FindGameObjectsWithTag("IP");
                    IPs[0].GetComponent<InventairePassif>().AddEffectItem(item2,true);
                    foreach (var Ip in IPs)
                    {
                        Ip.GetComponent<InventairePassif>().ContentAdd(item2,true);
                        Ip.GetComponent<InventairePassif>().UpdateImage(true);
                    }
                }
                else
                {
                    InventairePassif IP1 = GameObject.FindGameObjectWithTag("IP").GetComponent<InventairePassif>();
                    InventairePassif IP2 = GameObject.FindGameObjectWithTag("IP2").GetComponent<InventairePassif>();
                    IP1.content.Add(item2);
                    IP2.content.Add(item2);
                    IP1.Start();
                    IP2.Start();
                    IP1.AddEffectItem(item2, true);
                }
                break;
            case 3 : 
                var item3 = items[fresh[nb1] - 1];
                if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
                {
                    GameObject[] IPs = GameObject.FindGameObjectsWithTag("IP");
                    IPs[0].GetComponent<InventairePassif>().AddEffectItem(item3,true);
                    foreach (var Ip in IPs)
                    {
                        Ip.GetComponent<InventairePassif>().ContentAdd(item3,true);
                        Ip.GetComponent<InventairePassif>().UpdateImage(true);
                    }
                }
                else
                {
                    InventairePassif IP1 = GameObject.FindGameObjectWithTag("IP").GetComponent<InventairePassif>();
                    InventairePassif IP2 = GameObject.FindGameObjectWithTag("IP2").GetComponent<InventairePassif>();
                    IP1.content.Add(item3);
                    IP2.content.Add(item3);
                    IP1.Start();
                    IP2.Start();
                    IP1.AddEffectItem(item3, true);
                }
                break;
            case 4 : 
                var item4 = items[broken[nb1] - 1];
                var item5 = items[broken[nb2] - 1];
                if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
                {
                    GameObject[] IPs = GameObject.FindGameObjectsWithTag("IP");
                    IPs[0].GetComponent<InventairePassif>().AddEffectItem(item4,true);
                    IPs[0].GetComponent<InventairePassif>().AddEffectItem(item5,true);
                    foreach (var Ip in IPs)
                    {
                        Ip.GetComponent<InventairePassif>().ContentAdd(item4,true);
                        Ip.GetComponent<InventairePassif>().ContentAdd(item5,true);
                        Ip.GetComponent<InventairePassif>().UpdateImage(true);
                    }
                }
                else
                {
                    InventairePassif IP1 = GameObject.FindGameObjectWithTag("IP").GetComponent<InventairePassif>();
                    InventairePassif IP2 = GameObject.FindGameObjectWithTag("IP2").GetComponent<InventairePassif>();
                    IP1.content.Add(item4);
                    IP1.content.Add(item5);
                    IP2.content.Add(item4);
                    IP2.content.Add(item5);
                    IP1.Start();
                    IP2.Start();
                    IP1.AddEffectItem(item4, true);
                    IP1.AddEffectItem(item5,true);
                }
                break;
            case 5 : 
                var item6 = items[broken[nb1] - 1];
                var item7 = items[broken[nb1] - 1];
                if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
                {
                    GameObject[] IPs = GameObject.FindGameObjectsWithTag("IP");
                    IPs[0].GetComponent<InventairePassif>().AddEffectItem(item6,true);
                    IPs[0].GetComponent<InventairePassif>().AddEffectItem(item7,true);
                    foreach (var Ip in IPs)
                    {
                        Ip.GetComponent<InventairePassif>().ContentAdd(item6,true);
                        Ip.GetComponent<InventairePassif>().ContentAdd(item7,true);
                        Ip.GetComponent<InventairePassif>().UpdateImage(true);
                    }
                }
                else
                {
                    InventairePassif IP1 = GameObject.FindGameObjectWithTag("IP").GetComponent<InventairePassif>();
                    InventairePassif IP2 = GameObject.FindGameObjectWithTag("IP2").GetComponent<InventairePassif>();
                    IP1.content.Add(item6);
                    IP1.content.Add(item7);
                    IP2.content.Add(item6);
                    IP2.content.Add(item7);
                    IP1.Start();
                    IP2.Start();
                    IP1.AddEffectItem(item6, true);
                    IP1.AddEffectItem(item7,true);
                }
                break;
        }
    }
}
        