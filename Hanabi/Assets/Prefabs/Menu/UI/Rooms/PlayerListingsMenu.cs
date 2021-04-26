using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _content;
    [SerializeField]  private PlayerListing _playerListing;
    [SerializeField] private Text _readyUpText;
    
    private List<PlayerListing> _listings = new List<PlayerListing>();
    private RoomsCanvases _roomsCanvases;
    private bool _ready = false; 
    
    [SerializeField] private Text _classicText;
    [SerializeField] private Text _bouncyText;
    [SerializeField] private Text _lightText;

    private bool _classic = false;
    private bool _bouncy = false;
    private bool _light = false;
    

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
            _readyUpText.text = "Y";
        else
            _readyUpText.text = "N";
    }
    
    public override void OnLeftRoom()
    {
        _content.DestroyChildren();
    }

    private void GetCurrentRoomPlayers()
    {
        if(!PhotonNetwork.IsConnected)
            return;
        if(PhotonNetwork.CurrentRoom == null|| PhotonNetwork.CurrentRoom.Players == null)
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
            if (PhotonNetwork.IsMasterClient && (p1.Classic || p1.Bouncy || p1.Light) && (p2.Classic || p2.Bouncy || p2.Light) && p1.Classe != p2.Classe)
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
                PhotonNetwork.LoadLevel(1);
            }
        }
    }

    public void OnClick_ReadyUp()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!_ready);
            base.photonView.RPC("RPC_ChangeReadyState",RpcTarget.MasterClient,PhotonNetwork.LocalPlayer, _ready);
        }
    }
    [PunRPC]
    private void RPC_ChangeReadyState(Player player,bool ready)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listings[index].Ready = ready;
        }
    }
    
    private void ClassicChosen(bool choice)
    {
        _classic = choice;
        if (_classic)
            _classicText.text = "C";
        else
            _classicText.text = "";
    }
    
    private void BouncyChosen(bool choice)
    {
        _bouncy = choice;
        if (_bouncy)
            _bouncyText.text = "C";
        else
            _bouncyText.text = "";
    }
    
    private void LightChosen(bool choice)
    {
        _light = choice;
        if (_light)
            
            _lightText.text = "C";
        else
            _lightText.text = "";
    }

    public void OnClick_ChosenClassic()
    {
        ClassicChosen(!_classic);
        base.photonView.RPC("RPC_ChangeClassicState", RpcTarget.All, PhotonNetwork.LocalPlayer, _classic);
       /*int index = -1;
        if (PhotonNetwork.IsMasterClient)
            index = 0;
        else
            index = 1;
        _listings[index].Classic = true;
        _listings[index].Bouncy = false;
        _listings[index].Light = false;
        _listings[index].Classe = "Classic";
        LightChosen(false);
        BouncyChosen(false);*/
        
    }

    [PunRPC]
    private void RPC_ChangeClassicState(Player player, bool classic)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listings[index].Classic = true;
            _listings[index].Bouncy = false;
            _listings[index].Light = false;
            _listings[index].Classe = "Classic";
            LightChosen(false);
            BouncyChosen(false);
        }
    }
    
    public void OnClick_ChosenBouncy()
    {
        BouncyChosen(!_bouncy);
        base.photonView.RPC("RPC_ChangeBouncyState", RpcTarget.All, PhotonNetwork.LocalPlayer, _bouncy);
       /* int index = -1;
        if (PhotonNetwork.IsMasterClient)
            index = 0;
        else
            index = 1;
        _listings[index].Classic = false;
        _listings[index].Bouncy = true;
        _listings[index].Light = false;
        _listings[index].Classe = "Bouncy";
        LightChosen(false);
        ClassicChosen(false);*/
    }

    [PunRPC]
    private void RPC_ChangeBouncyState(Player player, bool classic)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listings[index].Classic = false;
            _listings[index].Bouncy = true;
            _listings[index].Light = false;
            _listings[index].Classe = "Bouncy";
            LightChosen(false);
            ClassicChosen(false);
        }
    }
    
    public void OnClick_ChosenLight()
    {
        LightChosen(!_light);
        base.photonView.RPC("RPC_ChangeLightState", RpcTarget.All, PhotonNetwork.LocalPlayer, _light);
        /*int index = -1;
        if (PhotonNetwork.IsMasterClient)
            index = 0;
        else
            index = 1;
        _listings[index].Classic = false;
        _listings[index].Bouncy = false;
        _listings[index].Light = true;
        _listings[index].Classe = "Light";
        BouncyChosen(false);
        ClassicChosen(false);*/
    }

    [PunRPC]
    private void RPC_ChangeLightState(Player player, bool light)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listings[index].Classic = false;
            _listings[index].Bouncy = false;
            _listings[index].Light = true;
            _listings[index].Classe = "Light";
            BouncyChosen(false);
            ClassicChosen(false);
        }
    }
    
}
