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
    [SerializeField] private GameObject sp1;
    [SerializeField] private GameObject sp2;
    [SerializeField] private GameObject theEndIsNear;
    
    

    
    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //MasterManager.NetworkInstantiate(_prefab1, sp1.transform.position, Quaternion.identity);
            PhotonNetwork.Instantiate("ALL_IN_ONE_R", sp1.transform.position, Quaternion.identity);

        }
        else
        {
            PhotonNetwork.Instantiate("ALL_IN_ONE_S", sp1.transform.position, Quaternion.identity);
            //MasterManager.NetworkInstantiate(_prefab2, sp2.transform.position, Quaternion.identity);
        }
        Destroy(theEndIsNear);
        Destroy(sp1);
        Destroy(sp2);
        Destroy(this);
    }
}
