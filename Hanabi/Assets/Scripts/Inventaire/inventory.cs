using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Pun;

public class inventory : MonoBehaviourPunCallbacks
{
    public int NombreDePièce;
    public int NombreDeRaspberries;

    public Text compteurdecoinstext;
    public Text compteurdeRaspberries;
    
    public List<Items> contenu = new List<Items>();
    public int currentindexitem = 0;
    public Image itemUIimage;
    public Text itemUIName;
    public Image Invisibleimage;
    
    public float timeStart;
    public Text displayTime;
    public float cooldown;

    public static inventory instance;
    
    
    private void Awake()
    {
        // Il faut qu'il n'y ai qu'un seul et unique inventaire
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance d'inventaire dans la scène");
            return;
        }
        
        instance = this;
    }

    public void Addcoins(int pièce,bool again)
    {
        NombreDePièce += pièce; 
        compteurdecoinstext.text = NombreDePièce.ToString();
        if (PhotonNetwork.IsConnected && again)
        {
            photonView.RPC("RPC_AddCoins", RpcTarget.Others,pièce);
        }
    }

    [PunRPC]
    public void RPC_AddCoins(int pièce)
    {
        Addcoins(pièce,false);
    }
    
    public void AddRaspberries(int Raspberries)
    {
        NombreDeRaspberries += Raspberries; 
        compteurdeRaspberries.text = NombreDeRaspberries.ToString();
        
    }
    
    public void Start()
    {
        updateinventoryImage();
        UpdateTextUI();
        displayTime.text = "";
    }


    public void ConsommerItems()
    {
        if (contenu.Count == 0)
        {
            return;
        }

        Items currentItem = contenu[currentindexitem];

        if (Time.time > cooldown)
        {
            switch (currentItem.id)
            {
                //if the item is "updraft"
                case 8:
                    PlayerMovement.instance.itemJump = true;
                    cooldown = Time.time + 15f;
                    timeStart = 15f;
                    break;
                //if the item is "plutôt deux fois Khune"
                case 15:
                    PlayerMovement.instance.hasDashed = false;
                    cooldown = Time.time + 15f;
                    timeStart = 15f;
                    break;
                //if the item is "sandwich triangle"
                case 7:
                    PlayerStress.instance.HealStressplayer(20);
                    cooldown = Time.time + 40f;
                    timeStart = 40f;
                    break;
                //if the item is "The World"
                case 19:
                    PlayerMovement.instance.itemTp = true;
                    cooldown = Time.time + 15f;
                    timeStart = 15f;
                    break;
            }
        }
    }

    private void Update() //updates the countdown for the current active item
    {
        useitem();
        
        if (contenu.Count > 0)
        {
            if (timeStart > 0)
            {
                timeStart -= Time.deltaTime;
                displayTime.text = Mathf.Round(timeStart).ToString();
            }
            else
            {
                displayTime.text = "ready";
            }
        }

        else
        {
            displayTime.text = "";
        }
    }

    public void updateinventoryImage()
    {
        if (contenu.Count > 0)
        {
            itemUIimage.sprite = contenu[currentindexitem].image;
            itemUIName.text = contenu[currentindexitem].name;
        }

        else
        {
            itemUIimage.sprite = Invisibleimage.sprite;
            itemUIName.text = "";
        }
    }

    public void remplacementItem(Items item)
    {
        if (contenu.Count == 1)
        {
            contenu.Remove(contenu[0]);
            contenu.Add(item);
        }
        else
        {
            contenu.Add(item);
        }
    }
    
    public void UpdateTextUI()
    {
        compteurdecoinstext.text = NombreDePièce.ToString();
        compteurdeRaspberries.text = NombreDeRaspberries.ToString();
    }

    public void useitem()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ConsommerItems();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ConsommerItems();
        }
    }

}
