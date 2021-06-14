using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDoor : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("Player1") || collider.CompareTag("Player2"))
        {
            Destroy(GetComponent<BoxCollider2D>());
        }
    }
}
