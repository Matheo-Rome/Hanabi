using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ActivateInstanciate : MonoBehaviour
{

    [SerializeField] private GameObject instanciate;
    private Instantiate _instantiate;
    public int p1;
    public int p2;

    private void Start()
    {
        _instantiate = instanciate.GetComponent<Instantiate>();
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            _instantiate.P1 = p1;
            _instantiate.P2 = p2;
            instanciate.SetActive(true);
        }
    }
}
