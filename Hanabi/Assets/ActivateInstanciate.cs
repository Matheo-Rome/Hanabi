using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ActivateInstanciate : MonoBehaviour
{

    [SerializeField] private GameObject instanciate;

    public void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            instanciate.SetActive(true);
        }
    }
}
