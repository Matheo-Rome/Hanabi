using System;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tp_to_z2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            new WaitForSeconds(0.3f);
            SceneManager.LoadScene(15);
        }
        
    }

}
