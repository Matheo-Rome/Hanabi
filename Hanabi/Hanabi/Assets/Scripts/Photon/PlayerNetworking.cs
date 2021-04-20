using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworking : MonoBehaviour
{
    public MonoBehaviour[] scriptsToIgnore;


    private PhotonView photonView;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            foreach (var script in scriptsToIgnore)
            {
                script.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
