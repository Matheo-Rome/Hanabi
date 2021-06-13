using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class Instantiate : MonoBehaviour
{
    [SerializeField] private GameObject sp1;
    [SerializeField] private GameObject sp2;
    [SerializeField] private GameObject theEndIsNear;
    [SerializeField] private GameObject TheOtherOne;
    public int P1;
    public int P2;
    
    

    
    private void Awake()
    {if (PhotonNetwork.IsMasterClient)
             {
                 //MasterManager.NetworkInstantiate(_prefab1, sp1.transform.position, Quaternion.identity);
                 if(P1 == 1)
                     PhotonNetwork.Instantiate("ALL_IN_ONE_R", sp1.transform.position, Quaternion.identity);
                 else if(P1 == 2)
                     PhotonNetwork.Instantiate("ALL_IN_ONE_S", sp1.transform.position, Quaternion.identity);
                 else if(P1 == 3)
                     PhotonNetwork.Instantiate("ALL_IN_ONE_B", sp1.transform.position, Quaternion.identity);
     
             }
             else
             {
                 if(P2 == 1)
                     PhotonNetwork.Instantiate("ALL_IN_ONE_R", sp2.transform.position, Quaternion.identity);
                 else if (P2== 2)
                     PhotonNetwork.Instantiate("ALL_IN_ONE_S", sp2.transform.position, Quaternion.identity);
                 else if (P2 == 3)
                     PhotonNetwork.Instantiate("ALL_IN_ONE_B", sp2.transform.position, Quaternion.identity);
                 //MasterManager.NetworkInstantiate(_prefab2, sp2.transform.position, Quaternion.identity);
             }
             
             Destroy(TheOtherOne);
             Destroy(theEndIsNear);
             Destroy(sp1);
             Destroy(sp2);
             Destroy(this);
         }
        
}
