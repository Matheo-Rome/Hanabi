using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public GameObject Inventaire;
    private inventory _inventory;

    private string saveSeparator = "%VALUE%";

    private void Start()
    {
        _inventory = Inventaire.GetComponent<inventory>();
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
            Save();
        if(Input.GetKeyDown(KeyCode.C))
            Load();

    }

     public void Save()
    {
        string[] content = new[] {_inventory.NombreDePièce.ToString(), _inventory.NombreDeRaspberries.ToString()};
        string saveString = string.Join(saveSeparator,content);
        File.WriteAllText(Application.dataPath + "/sauvgarde.txt", saveString.ToString());
        Debug.Log("Saved" + _inventory.NombreDePièce.ToString() + " " + _inventory.NombreDeRaspberries.ToString());
    }

    public void Load()
    {
        string saveString = File.ReadAllText(Application.dataPath + "/sauvgarde.txt");
        string[] content = saveString.Split(new[] {saveSeparator},System.StringSplitOptions.None);
        _inventory.NombreDePièce = int.Parse(content[0]);
        _inventory.compteurdecoinstext.text =  _inventory.NombreDePièce.ToString();
        _inventory.NombreDeRaspberries = int.Parse(content[1]);
        _inventory.compteurdeRaspberries.text = _inventory.NombreDeRaspberries.ToString();
        Debug.Log("Loaded" + _inventory.NombreDePièce.ToString() + " " + _inventory.NombreDeRaspberries.ToString());
    }
}
