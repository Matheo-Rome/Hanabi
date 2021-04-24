using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class NickName : MonoBehaviour
{
   private ExitGames.Client.Photon.Hashtable _myCustomeProperties = new ExitGames.Client.Photon.Hashtable();
   [SerializeField] private Text _text;

   private void SetNickName()
   {
       _myCustomeProperties["NickName"] = _text.text;
      PhotonNetwork.SetPlayerCustomProperties(_myCustomeProperties);
      //PhotonNetwork.LocalPlayer.CustomProperties = _myCustomeProperties;
   }
   
   public void OnClick_Button()
   {
     SetNickName();
   }
}
