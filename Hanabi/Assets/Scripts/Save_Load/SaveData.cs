using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class SaveData : MonoBehaviourPunCallbacks
{
    public GameObject Inventaire;
    private inventory _inventory;

    private string saveSeparator = "%VALUE%";

    private bool already = false;
    public int Bank = 0;

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
    }
    
    public void Save()
    {
        int pièce = Bank;
        if (_inventory.NombreDePièce < Bank)
            pièce = _inventory.NombreDePièce;
        string[] content = new[] {pièce.ToString(), _inventory.NombreDeRaspberries.ToString()};
        string saveString = string.Join(saveSeparator,content);
        File.WriteAllText(Application.dataPath + "/sauvgarde.txt", saveString.ToString());
        Debug.Log("Saved" + _inventory.NombreDePièce.ToString() + " " + _inventory.NombreDeRaspberries.ToString());
    }

    public void Load()
    {
        string saveString = File.ReadAllText(Application.dataPath + "/sauvgarde.txt");
        if (saveString.Length != 0)
        {
            string[] content = saveString.Split(new[] {saveSeparator}, System.StringSplitOptions.None);
            if (!PhotonNetwork.IsConnected)
            {
                _inventory.NombreDePièce = int.Parse(content[0]);
                _inventory.compteurdecoinstext.text = _inventory.NombreDePièce.ToString();
                _inventory.NombreDeRaspberries = int.Parse(content[1]);
                _inventory.compteurdeRaspberries.text = _inventory.NombreDeRaspberries.ToString();
            }
            else if (PhotonNetwork.IsMasterClient)
            {
                GameObject[] inventories = GameObject.FindGameObjectsWithTag("InventaireM");
                foreach (var inventory in inventories)
                {
                    inventory.GetComponent<inventory>().Addcoins(-inventory.GetComponent<inventory>().NombreDePièce,true);
                    inventory.GetComponent<inventory>().Addcoins(int.Parse(content[0]),true);
                    inventory.GetComponent<inventory>().AddRaspberries(-inventory.GetComponent<inventory>().NombreDeRaspberries,true);
                    inventory.GetComponent<inventory>().AddRaspberries(int.Parse(content[1]),true);
                }
            }
        }
    }

    public void UpdateBank(int Update,bool again)
    {
        Bank += Update;
        if(PhotonNetwork.IsConnected && again)
            photonView.RPC("RPC_UpdateBank",RpcTarget.Others,Update);

    }

    [PunRPC]
    public void RPC_UpdateBank(int Update)
    {
        UpdateBank(Update,false);
    }
}