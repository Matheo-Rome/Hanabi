using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class SellButtonItem : MonoBehaviourPunCallbacks
{
    public Text ItemName;
    public Image ItemImage;
    public Text ItemPrice;
    public Items item;

    int reduction = 0;
    private int nouveauxprix = 0;

    public void BuyItem()
    {
        int nbPiece = 0;
        if (PhotonNetwork.IsConnected)
            nbPiece = GameObject.FindGameObjectWithTag("InventaireM").GetComponent<inventory>().NombreDePièce;
        else
            nbPiece = GameObject.FindGameObjectWithTag("Inventaire").GetComponent<inventory>().NombreDePièce;
        if (nbPiece >= item.Price )
        {
            reduction = 0;
            nouveauxprix = 0;
            foreach (var objet in InventairePassif.instance.content)
            {
                reduction += objet.ReducePrice;
            }


            nouveauxprix = item.Price - reduction;
            if (nouveauxprix < 0)
            {
                nouveauxprix = 0;
            }

            if (PhotonNetwork.IsConnected)
            {
                GameObject[] IPs = GameObject.FindGameObjectsWithTag("IP");
                GameObject[] Invs = GameObject.FindGameObjectsWithTag("InventaireM");
                foreach (var inv in Invs)
                {
                    inv.GetComponent<inventory>().Addcoins(-nouveauxprix, true);
                }
                IPs[0].GetComponent<InventairePassif>().AddEffectItem(item,true);
                foreach (var IP in IPs)
                {
                    IP.GetComponent<InventairePassif>().ContentAdd(item,true);
                    IP.GetComponent<InventairePassif>().UpdateImage(true);
                }
                
            }
            else
            {
                InventairePassif IP1 = GameObject.FindGameObjectWithTag("IP").GetComponent<InventairePassif>();
                InventairePassif IP2 = GameObject.FindGameObjectWithTag("IP2").GetComponent<InventairePassif>();
                inventory I1 = GameObject.FindGameObjectWithTag("Inventaire").GetComponent<inventory>();
                inventory I2 = GameObject.FindGameObjectWithTag("Inventaire2").GetComponent<inventory>();
                IP1.content.Add(item);
                IP2.content.Add(item);
                IP1.Start();
                IP2.Start();
                IP1.AddEffectItem(item, true);
                I1.Addcoins(-nouveauxprix, false);
                I2.Addcoins(-nouveauxprix, false);
            }
        }
    }
}

