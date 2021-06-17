using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class crédit : MonoBehaviour
{
    void LoadMenu()
    {
        PhotonNetwork.LoadLevel(0);  
    }
}
