using System;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tp_to_z2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            new WaitForSeconds(0.3f);
            SceneManager.LoadScene(15);
        }
        
    }

}
