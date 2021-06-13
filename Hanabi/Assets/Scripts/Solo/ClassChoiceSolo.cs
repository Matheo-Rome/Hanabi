﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClassChoiceSolo : MonoBehaviour
{
    private bool _bouncy = false;
    private bool _light = false;
    private bool _classic = false;

    public int p1 = 0;
    public int p2 = 0;

    [SerializeField] private Text _classicText;
    [SerializeField] private Text _bouncyText;
    [SerializeField] private Text _lightText;
    [SerializeField] private Text _classicText2;
    [SerializeField] private Text _bouncyText2;
    [SerializeField] private Text _lightText2;
    
    [SerializeField] private GameObject sound;
    [SerializeField] private ActivateInstanciate _activateInstanciate;


    public void OnClick_StartGame()
    {
        if (p1 != p2 && p1 != 0)
        {
            sound.SetActive(false);
            _activateInstanciate.p1 = p1;
            _activateInstanciate.p2 = p2;
            StartCoroutine(Disconnect());
            Debug.Log(PhotonNetwork.IsConnected);
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator Disconnect()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }
    }

    public void OnClick_Chosen_Classic()
    {
        _classicText.text = "c";
        _classicText2.text = "";
        _bouncyText.text = "";
        _lightText.text = "";
        p1 = 1;
    }
    
    public void OnClick_Chosen_Classic2()
    {
        _classicText2.text = "c";
        _classicText.text = "";
        _bouncyText2.text = "";
        _lightText2.text = "";
        p2 = 1;
    }
    public void OnClick_Chosen_Bouncy()
    {
        _bouncyText.text = "c";
        _bouncyText2.text = "";
        _classicText.text = "";
        _lightText.text = "";
        p1 = 2;
    }
    
    public void OnClick_Chosen_Bouncy2()
    {
        _bouncyText2.text = "c";
        _bouncyText.text = "";
        _classicText2.text = "";
        _lightText2.text = "";
        p2 = 2;
    }
    
    public void OnClick_Chosen_Light()
    {
        _lightText.text = "c";
        _lightText2.text = "";
        _classicText.text = "";
        _bouncyText.text = "";
        p1 = 3;
    }
    
    public void OnClick_Chosen_Light2()
    {
        _lightText2.text = "c";
        _lightText.text = "";
        _classicText2.text = "";
        _bouncyText2.text = "";
        p2 = 3;
    }
    
    
    
}
