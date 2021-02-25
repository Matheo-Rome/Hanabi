using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class inventory : MonoBehaviour
{
    public int NombreDePièce;

    public Text compteurdecoinstext;
    
    public List<Items> contenu = new List<Items>();
    public int currentindexitem = 0;
    public Image itemUIimage;
    public Text itemUIName;
    public Image Invisibleimage;

    private float cooldown;


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

    public void Addcoins(int pièce)
    {
        NombreDePièce += pièce; 
        compteurdecoinstext.text = NombreDePièce.ToString();
        
    }
    
    public void Start()
    {
        updateinventoryImage();
        UpdateTextUI();
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
                    break;
                //if the item is "plutôt deux fois Khune"
                case 15:
                    PlayerMovement.instance.hasDashed = false;
                    cooldown = Time.time + 15f;
                    break;
                //if the item is "sandwich triangle"
                case 7:
                    PlayerStress.instance.HealStressplayer(20);
                    cooldown = Time.time + 40f;
                    break;
            }
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
    }

}
