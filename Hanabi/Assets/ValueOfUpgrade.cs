using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueOfUpgrade : MonoBehaviour
{
    public int addGiventByJar = 3;
    
    
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
}
