using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayertouchedScript : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {                                                                        
            Destroy(transform.parent.gameObject);
        }
    }
}
