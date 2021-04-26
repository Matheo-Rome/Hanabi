using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    [SerializeField] private GameObject _prefab1;
    [SerializeField] private GameObject _prefab2;
    [SerializeField] private GameObject _prefab3;
    [SerializeField] private Transform sp1;
    [SerializeField] private Transform sp2;


    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("1");
            MasterManager.NetworkInstantiate(_prefab1, sp1.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("2");
            MasterManager.NetworkInstantiate(_prefab2, sp1.transform.position, Quaternion.identity);
        }

    }
}
