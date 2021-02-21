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
        updateinventoryImage(currentindexitem);
    }


    public void ConsommerItems()
    {
        if (contenu.Count == 0)
        {
            return;
        }
        
        Items currentItem = contenu[currentindexitem];
        PlayerStress.instance.HealStressplayer(currentItem.StressRemoved);
        PlayerMovement.instance.speed += currentItem.speedGiven;
        contenu.Remove(contenu[0]);
        GetNextItem();
    }

    public void GetNextItem()
    {
        if (contenu.Count == 0)
        {
            return;
        }
        
        currentindexitem ++;
        if (currentindexitem > contenu.Count - 1)
        {
            currentindexitem = 0;
        }
        updateinventoryImage(currentindexitem);
        
    }

    public void GetPreviousItem()
    {
        if (contenu.Count == 0)
        {
            return;
        }
        
        currentindexitem --;
        if (currentindexitem < 0)
        {
            currentindexitem = contenu.Count - 1;
        }
        updateinventoryImage(currentindexitem);
    }

    public void updateinventoryImage(int currentindexitem)
    {
        if (contenu.Count > 0)
        {
            itemUIimage.sprite = contenu[currentindexitem].image;
            itemUIName.text = contenu[currentindexitem].name;
        }

        else
        {
            itemUIimage.sprite = null;
            itemUIName.text = "";
        }
    }

}
