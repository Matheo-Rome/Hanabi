using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueOfUpgrade : MonoBehaviour
{
    public int AmeliorationJar = 0;
    public int AmelioriationBank = 0;
    public int AmeliorationStress = 200;
    public float AmeliorationFeuDeCamps = 0.6f;
    

    public static ValueOfUpgrade instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de GameObjectUpgrade dans la scène");
            return;
        }

        instance = this;
    }

    public void Start()
    {
        AmeliorationStress = 200;
        AmeliorationFeuDeCamps = 0.6f;
    }
}
