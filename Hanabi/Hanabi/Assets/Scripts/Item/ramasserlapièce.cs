using System;
using UnityEngine;

public class ramasserlapièce : MonoBehaviour
{ 
    //Si le joueur rentre dans la pièce, elle s'enlève

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            inventory.instance.Addcoins(1);
            Destroy(gameObject);
        }
    }
}
