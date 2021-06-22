using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _content;
    [SerializeField] private PlayerListing _playerListing;
    [SerializeField] private TextMeshProUGUI _readyUpText;

    private List<PlayerListing> _listings = new List<PlayerListing>();
    private RoomsCanvases _roomsCanvases;
    private bool _ready = false;

    [SerializeField] private Text _classicText;
    [SerializeField] private Text _bouncyText;
    [SerializeField] private Text _lightText;

    private bool _classic = false;
    private bool _bouncy = false;
    private bool _light = false;

    [SerializeField] private GameObject sound;

    [SerializeField] private ActivateInstanciate _activateInstanciate;


    private void Awake()
    {
        GetCurrentRoomPlayers();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }

    private void SetReadyUp(bool state)
    {
        _ready = state;
        if (_ready)
            _readyUpText.text = "Ready";
        else
            _readyUpText.text = "Not Ready";
    }

    public override void OnLeftRoom()
    {
        _content.DestroyChildren();
    }

    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddplayerListing(playerInfo.Value);
        }
    }

    private void AddplayerListing(Player player)
    {
        PlayerListing listing = Instantiate(_playerListing, _content);
        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            _listings.Add(listing);
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _roomsCanvases.CurrentRoomCanvas.LeaveRoomMenu.OnClick_LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddplayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }

    public void OnClick_StartGame()
    {
        if (_listings.Count == 2)
        {
            PlayerListing p1 = _listings[0];
            PlayerListing p2 = _listings[1];
            if (PhotonNetwork.IsMasterClient && (p1.Classic || p1.Bouncy || p1.Light) &&
                (p2.Classic || p2.Bouncy || p2.Light) && p1.Classe != p2.Classe)
            {
                for (int i = 0; i < _listings.Count; i++)
                {
                    if (_listings[i].Player != PhotonNetwork.LocalPlayer)
                    {
                        if (!_listings[i].Ready)
                            return;
                    }
                }

                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
                sound.SetActive(false);
                base.photonView.RPC("RPC_Desactivate",RpcTarget.All);
                base.photonView.RPC("RPC_Class",RpcTarget.All);
                PhotonNetwork.LoadLevel(66);
            }
        }
    }

    [PunRPC]
    private void RPC_Desactivate()
    {
        sound.SetActive(false);
    }

    [PunRPC]
    private void RPC_Class()
    {
        if (_listings[0].Classic)
            _activateInstanciate.p1 = 1;
        else if (_listings[0].Bouncy)
            _activateInstanciate.p1 = 2;
        else if (_listings[0].Light)
            _activateInstanciate.p1 = 3;

        if (_listings[1].Classic)
            _activateInstanciate.p2 = 1;
        else if (_listings[1].Bouncy)
            _activateInstanciate.p2 = 2;
        else if (_listings[1].Light)
            _activateInstanciate.p2 = 3;
        
    }

    public void OnClick_ReadyUp()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!_ready);
            base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, _ready);
        }
    }

    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listings[index].Ready = ready;
            if (ready)
                _readyUpText.text = "Ready";
            else
                _readyUpText.text = "Not Ready";
        }
    }

    private void ClassicChosen(bool choice)
    {
        _classic = choice;
        if (_classic)
            _classicText.text = "X";
        else
            _classicText.text = "";
    }

    private void BouncyChosen(bool choice)
    {
        _bouncy = choice;
        if (_bouncy)
            _bouncyText.text = "X";
        else
            _bouncyText.text = "";
    }

    private void LightChosen(bool choice)
    {
        _light = choice;
        if (_light)

            _lightText.text = "X";
        else
            _lightText.text = "";
    }

    public void OnClick_ChosenClassic()
    {
        ClassicChosen(!_classic);
        base.photonView.RPC("RPC_ChangeClassicState", RpcTarget.All, PhotonNetwork.LocalPlayer);
        LightChosen(false);
        BouncyChosen(false);
    }

    [PunRPC]
    private void RPC_ChangeClassicState(Player player)
    {
        if (_listings.Count == 2)
       {
           if (player.IsMasterClient)
           {
               _listings[0].Classic = true;
               _listings[0].Bouncy = false;
               _listings[0].Light = false;
               _listings[0].Classe = "Classic";
           }
           else
           {
               _listings[1].Classic = true;
               _listings[1].Bouncy = false;
               _listings[1].Light = false;
               _listings[1].Classe = "Classic";
           }

           if(!player.IsLocal) 
               ClassicChosen(false);
       }       
    }
    
    public void OnClick_ChosenBouncy()
    {
        BouncyChosen(!_bouncy);
        base.photonView.RPC("RPC_ChangeBouncyState", RpcTarget.All, PhotonNetwork.LocalPlayer);
        LightChosen(false);
       ClassicChosen(false);
    }

    [PunRPC]
    private void RPC_ChangeBouncyState(Player player)
    {
        if (_listings.Count == 2)
        {
            if (player.IsMasterClient)
            {
                _listings[0].Classic = false; 
                _listings[0].Bouncy = true; 
                _listings[0].Light = false;
                _listings[0].Classe = "Bouncy";
            }
            else
            { 
                _listings[1].Classic = false;
                _listings[1].Bouncy = true;
                _listings[1].Light = false;
                _listings[1].Classe = "Bouncy";
            }
            if(!player.IsLocal) 
                BouncyChosen(false);
        }
        
    }
    
    public void OnClick_ChosenLight()
    {
        LightChosen(!_light);
        base.photonView.RPC("RPC_ChangeLightState", RpcTarget.All, PhotonNetwork.LocalPlayer);
        ClassicChosen(false);
        BouncyChosen(false);
    }

    [PunRPC]
    private void RPC_ChangeLightState(Player player)
    {
        if (_listings.Count == 2)
        {
            if (player.IsMasterClient)
            {
                _listings[0].Classic = false;
                _listings[0].Bouncy = false;
                _listings[0].Light = true;
                _listings[0].Classe = "Light";
            }
            else
            {
                _listings[1].Classic = false;
                _listings[1].Bouncy = false;
                _listings[1].Light = true;
                _listings[1].Classe = "Light";
            }
            if(!player.IsLocal) 
                LightChosen(false);
        }
    }
    
}
