using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text _text;

    public Player Player { get; private set; }
    public bool Ready = false;

    public void SetPlayerInfo(Player player)
    {
        Player = player;
        SetPlayerText(player);
    }

    public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(target, changedProps);
        if (target != null && target == Player)
        {
            if(changedProps.ContainsKey("NickName"))
                SetPlayerText(target);
        }
    }
    
    private void SetPlayerText(Player player)
    {
        string result = "Bernard";
        if (player.CustomProperties.ContainsKey("NickName"))
        {
            result = (string) player.CustomProperties["NickName"];
            player.NickName = result;
        }

        _text.text = player.NickName;
    }
}
