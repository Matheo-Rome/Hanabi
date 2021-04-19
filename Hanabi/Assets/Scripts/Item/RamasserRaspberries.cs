using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamasserRaspberries : MonoBehaviour
{
    //Si le joueur rentre dans la Raspberries, elle s'enlève

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            inventory.instance.AddRaspberries(1);
            Destroy(gameObject);
        }
    }
}
