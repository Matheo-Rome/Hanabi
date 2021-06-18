using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class HeartAnimation : MonoBehaviour
{
    private int stress;
    public Animator heartAnimation;
    private bool founded = false;
    private PlayerStressSolo _playerStressSolo;
    private PlayerStress _playerStress;
        
    void Start()
    {
        stress = 0;
    }

    
    void Update()
    {
        if (!founded)
        {
            if (!PhotonNetwork.IsConnected)
                _playerStressSolo = gameObject.transform.parent.transform.parent.gameObject.GetComponentInChildren<PlayerStressSolo>();
            else
                _playerStress = gameObject.transform.parent.transform.parent.gameObject.GetComponentInChildren<PlayerStress>();
        }

        if (PhotonNetwork.IsConnected)
            stress = _playerStress.currentStress;
        else
            stress = _playerStressSolo.currentStress;
        heartAnimation.SetInteger("Stress", stress);
    }
}
