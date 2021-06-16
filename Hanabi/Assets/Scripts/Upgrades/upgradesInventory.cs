using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class upgradesInventory : MonoBehaviour
{
    public static upgradesInventory instance;
    public List<upgradesSO> content = new List<upgradesSO>();
    public int currentIndex;

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
        
    }
}
