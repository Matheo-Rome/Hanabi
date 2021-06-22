using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ActivateInstanciate : MonoBehaviour
{

    [SerializeField] private GameObject instanciate;

    [SerializeField] private GameObject instantiateSolo;

    private Instantiate _instantiate;
    private InstantiateSolo _instantiateSolo;
    public int p1;
    public int p2;
    public bool solo = false;

    private void Start()
    {
        _instantiateSolo = instantiateSolo.GetComponent<InstantiateSolo>();
        _instantiate = instanciate.GetComponent<Instantiate>();
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 66)
        {
            if (solo)
            {
                _instantiateSolo.P1 = p1;
                _instantiateSolo.P2 = p2;
                instantiateSolo.SetActive(true);
            }
            else
            {
                _instantiate.P1 = p1;
                _instantiate.P2 = p2;
                instanciate.SetActive(true);
            }
        }
    }
}
