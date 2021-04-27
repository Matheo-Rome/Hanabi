using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlace_Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerStress.instance.isTouchingFire = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerStress.instance.isTouchingFire = false;
        }
    }
}
