﻿using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class InventairePassif : MonoBehaviour
{
    public static InventairePassif instance;
    public List<Items> content = new List<Items>();
    public int currentIndex;
    public Image ImageItem0;
    public Image ImageItem1;
    public Image ImageItem2;
    public Image ImageItem3;
    public Image ImageItem4;
    public Image ImageItem5;
    public Image ImageItem6;
    public Image ImageItem7;
    public Image ImageItem8;
    public Image ImageItem9;
    public Image ImageItem10;
    public Image ImageItem11;
    public Image ImageItem12;
    public Image ImageItem13;
    public Image ImageItem14;
    public Image ImageItem15;
    public Image ImageItem16;
    public Image ImageItem17;
    public Image ImageItem18;
    public Image ImageItem19;
    public Image ImageItem20;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance d'inventaire Passif dans la scène");
            return;
        }

        instance = this;
    }

    public void Start()
    {
        Item0();
        Item1();
        Item2();
        Item3();
        Item4();
        Item5();
        Item6();
        Item7();
        Item8();
        Item9();
        Item10();
        Item11();
        Item12();
        Item13();
        Item14();
        Item15();
        Item16();
        Item17();
        Item18();
        Item19();
        Item20();
    }
    public void Item0()
    {
        if (content.Count != 0)
        {
            UpdateImageInventory(0);
        }
    }
    
    public void Item1()
    {
        if (1 < content.Count)
        {
            UpdateImageInventory(1);
        }
    }
    
    public void Item2()
    {
        if (2 < content.Count)
        {
            UpdateImageInventory(2);
        }
    }
    
    public void Item3()
    {
        if (3 < content.Count)
        {
            UpdateImageInventory(3);
        }
    }
    
    public void Item4()
    {
        if (4 < content.Count)
        {
            UpdateImageInventory(4);
        }
    }
    
    public void Item5()
    {
        if (5 < content.Count)
        {
            UpdateImageInventory(5);
        }
    }
    
    public void Item6()
    {
        if (6 < content.Count)
        {
            UpdateImageInventory(6);
        }
    }
    
    public void Item7()
    {
        if (7 < content.Count)
        {
            UpdateImageInventory(7);
        }
    }
    
    public void Item8()
    {
        if (8 < content.Count)
        {
            UpdateImageInventory(8);
        }
    }
    
    public void Item9()
    {
        if (9 < content.Count)
        {
            UpdateImageInventory(9);
        }
    }
    
    public void Item10()
    {
        if (10 < content.Count)
        {
            UpdateImageInventory(10);
        }
    }
    
    public void Item11()
    {
        if (11 < content.Count)
        {
            UpdateImageInventory(11);
        }
    }
    
    public void Item12()
    {
        if (12 < content.Count)
        {
            UpdateImageInventory(12);
        }
    }
    
    public void Item13()
    {
        if (13 < content.Count)
        {
            UpdateImageInventory(13);
        }
    }
    
    public void Item14()
    {
        if (14 < content.Count)
        {
            UpdateImageInventory(14);
        }
    }
    
    public void Item15()
    {
        if (15 < content.Count)
        {
            UpdateImageInventory(15);
        }
    }
    
    public void Item16()
    {
        if (16 < content.Count)
        {
            UpdateImageInventory(16);
        }
    }
    
    public void Item17()
    {
        if (17 < content.Count)
        {
            UpdateImageInventory(17);
        }
    }
    
    public void Item18()
    {
        if (18 < content.Count)
        {
            UpdateImageInventory(18);
        }
    }
    
    public void Item19()
    {
        if (19 < content.Count)
        {
            UpdateImageInventory(19);
        }
    }
    
    public void Item20()
    {
        if (20 < content.Count)
        {
            UpdateImageInventory(20);
        }
    }

    public void UpdateImageInventory(int CurrentItemIndex)
    {
        if (CurrentItemIndex == 0)
        {
            ImageItem0.sprite = content[CurrentItemIndex].image;
        }

        if (CurrentItemIndex == 1)
        {
            ImageItem1.sprite = content[CurrentItemIndex].image; 
        }
        
        if (CurrentItemIndex == 2)
        {
            ImageItem2.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 3)
        {
            ImageItem3.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 4)
        {
            ImageItem4.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 5)
        {
            ImageItem5.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 6)
        {
            ImageItem6.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 7)
        {
            ImageItem7.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 8)
        {
            ImageItem8.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 9)
        {
            ImageItem9.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 10)
        {
            ImageItem10.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 11)
        {
            ImageItem11.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 12)
        {
            ImageItem12.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 13)
        {
            ImageItem13.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 14)
        {
            ImageItem14.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 15)
        {
            ImageItem15.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 16)
        {
            ImageItem16.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 17)
        {
            ImageItem17.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 18)
        {
            ImageItem18.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 19)
        {
            ImageItem19.sprite = content[CurrentItemIndex].image; 
            
        }
        
        if (CurrentItemIndex == 20)
        {
            ImageItem20.sprite = content[CurrentItemIndex].image; 
            
        }
        
    }

}