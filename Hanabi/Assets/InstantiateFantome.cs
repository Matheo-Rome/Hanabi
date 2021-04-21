using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateFantome : MonoBehaviour
{
    public bool Oneinstance = false;
    
    public GameObject Fantome;
    void Start()
    {
        
    }

    void Update()
    {
        if (PlayerStress.instance.currentStress == 200 && !Oneinstance)
        {
            Instantiate(Fantome, transform.position, Quaternion.identity);
            Oneinstance = false;
        }
    }
}