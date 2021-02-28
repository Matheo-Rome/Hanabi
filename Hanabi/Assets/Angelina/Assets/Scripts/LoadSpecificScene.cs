using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{
    private int i = 1;
    private int y;
    private bool incrementation = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
}
