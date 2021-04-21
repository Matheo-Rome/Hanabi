using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateFantome : MonoBehaviour
{
    public bool Oneinstance = false;
    
    public GameObject Fantome;
    public GameObject instance;
    void Start()
    {
        
    }
    void Update()
    {
        if (PlayerStress.instance.currentStress == 200 && !Oneinstance)
        {
            instance = Instantiate(Fantome, transform.position, Quaternion.identity);
            Oneinstance = true;
        }

        if (PlayerStress.instance.currentStress <= 189 && Oneinstance)
        {
            Oneinstance = false;
            Destroy(instance);
        }
    }
}