using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class upgradesInventory : MonoBehaviour
{
    public static upgradesInventory instance;
    public List<upgradesSO> content = new List<upgradesSO>(); // Liste où sont stocké tous les items

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
        //test
        PlayerMovement.instance.jumpVelocity += Upgrade.jumpBoostGiven;

        ValueOfUpgrade.instance.addGiventByJar = Upgrade.coinDropUpgrade;
    }

    public void Start()
    {
        
    }
}
        