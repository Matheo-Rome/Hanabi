using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviourPunCallbacks
{
    public GameObject Inventaire;
    private inventory _inventory;

    private string saveSeparator = "%VALUE%";

    private bool already = false;
    private bool already2 = false;
    public int Bank = 0;
    private bool founded = false;
    private ValueOfUpgrade ValueOfUpgrade;

    private void Start()
    {
        _inventory = Inventaire.GetComponent<inventory>();
    }

   

    void Update()
    {
        if (!already && Input.anyKey)
        {
            Load();
            already = true;
        }
        else if (!already2 && Input.anyKey && SceneManager.GetActiveScene().buildIndex == 1)
        {
            Load();
            already2 = true;
        }
        if (!founded)
        {
            ValueOfUpgrade = GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>();
            founded = ValueOfUpgrade != null;
        }
    }
    
    public void Save()
    {
        int pièce = ValueOfUpgrade.AmelioriationBank;
        //ValueOfUpgrade = GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>();
        if (_inventory.NombreDePièce < ValueOfUpgrade.AmelioriationBank)
            pièce = _inventory.NombreDePièce;
        string[] content = new[] {pièce.ToString(), _inventory.NombreDeRaspberries.ToString(),ValueOfUpgrade.AmeliorationJar.ToString(),
            ValueOfUpgrade.AmelioriationBank.ToString(),ValueOfUpgrade.AmeliorationStress.ToString(),
            ((int) (ValueOfUpgrade.AmeliorationFeuDeCamps*10)).ToString(),ValueOfUpgrade.AmeliorationRandomLevel.ToString()};
        string saveString = string.Join(saveSeparator,content);
        File.WriteAllText(Application.dataPath + "/sauvgarde.txt", saveString.ToString());
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/sauvgarde.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/sauvgarde.txt");
            //ValueOfUpgrade = GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>();
            if (saveString.Length != 0)
            {
                string[] content = saveString.Split(new[] {saveSeparator}, System.StringSplitOptions.None);
                if (!PhotonNetwork.IsConnected)
                {
                    _inventory.NombreDePièce = int.Parse(content[0]);
                    _inventory.compteurdecoinstext.text = _inventory.NombreDePièce.ToString();
                    _inventory.NombreDeRaspberries = int.Parse(content[1]);
                    _inventory.compteurdeRaspberries.text = _inventory.NombreDeRaspberries.ToString();
                    ValueOfUpgrade.AmeliorationJar = int.Parse(content[2]);
                    ValueOfUpgrade.AmelioriationBank = int.Parse(content[3]);
                    ValueOfUpgrade.AmeliorationStress = int.Parse(content[4]);
                    ValueOfUpgrade.AmeliorationRandomLevel = int.Parse(content[6]);
                    ValueOfUpgrade.AmeliorationFeuDeCamps = ((float) int.Parse(content[5])) / 10;
                    UpdateStress();
                }
                else if (PhotonNetwork.IsMasterClient)
                {
                    GameObject[] inventories = GameObject.FindGameObjectsWithTag("InventaireM");
                    foreach (var inventory in inventories)
                    {
                        inventory.GetComponent<inventory>()
                            .Addcoins(-inventory.GetComponent<inventory>().NombreDePièce, true);
                        inventory.GetComponent<inventory>().Addcoins(int.Parse(content[0]), true);
                        inventory.GetComponent<inventory>()
                            .AddRaspberries(-inventory.GetComponent<inventory>().NombreDeRaspberries, true);
                        inventory.GetComponent<inventory>().AddRaspberries(int.Parse(content[1]), true);
                    }

                    photonView.RPC("RPC_UpdateValue", RpcTarget.All, int.Parse(content[2]), int.Parse(content[3]),
                        int.Parse(content[4]), ((float) int.Parse(content[5])) / 10, int.Parse(content[6]));
                    photonView.RPC("RPC_UpdateStress", RpcTarget.All);
                }
            }
        }
    }

    public void UpdateStress()
    {
        if (PhotonNetwork.IsConnected)
        {
            foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
            {
                player.GetComponent<PlayerStress>().maxStress = ValueOfUpgrade.AmeliorationStress;
            }
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerStressSolo>().maxStress = ValueOfUpgrade.AmeliorationStress;
            GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerStressSolo>().maxStress = ValueOfUpgrade.AmeliorationStress;
            
        }
    }

    [PunRPC]
    public void RPC_UpdateStress()
    {
        UpdateStress();
    }

    
    public void UpdateValue(int _ameliorationJar, int _amelioriationBank, int _ameliorationStress, float _ameliorationFeuDeCamps, int _ameliorationRandomLevel)
    {
        ValueOfUpgrade.AmeliorationJar = _ameliorationJar;
        ValueOfUpgrade.AmelioriationBank = _amelioriationBank;
        ValueOfUpgrade.AmeliorationStress = _ameliorationStress;
        ValueOfUpgrade.AmeliorationFeuDeCamps = _ameliorationFeuDeCamps;
        ValueOfUpgrade.AmeliorationRandomLevel = _ameliorationRandomLevel;
    }

    [PunRPC]
    public void RPC_UpdateValue(int _ameliorationJar, int _amelioriationBank, int _ameliorationStress,
        float _ameliorationFeuDeCamps, int _ameliorationRandomLevel)
    {
        UpdateValue(_ameliorationJar, _amelioriationBank, _ameliorationStress, _ameliorationFeuDeCamps,_ameliorationRandomLevel);
    }
   
}