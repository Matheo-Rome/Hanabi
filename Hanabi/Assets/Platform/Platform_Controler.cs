using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Platform_Controler : MonoBehaviour
{
    
    private float timer;
    
    //si true alors platform1 actif et 2 désactif
    private bool active;

    public ED_platform platform1;

    public ED_platform platform2;
    // Start is called before the first frame update
    void Start()
    {
        timer = 1f;
        active = true;
        platform2.Desactive();

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            Switch();
            timer = 1f;
        }
    }

    void Switch()
    {
        if (active)
        {
            platform1.Desactive();
            platform2.Active();
            active = false;
        }
        else
        {
            platform1.Active();
            platform2.Desactive();
            active = true;

        }
        
    }
}
