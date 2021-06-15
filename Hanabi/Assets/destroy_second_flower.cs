using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_second_flower : MonoBehaviour
{
    public LoadScene_z1 flower;
    
    void Update()
    {
        if (flower.destroy)
        {
            Destroy(gameObject);
        }
    }
}
