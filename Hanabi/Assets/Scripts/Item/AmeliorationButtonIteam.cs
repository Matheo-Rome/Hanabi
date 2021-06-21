using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class AmeliorationButtonIteam : MonoBehaviour
{
    public Text ItemName;
    public Image ItemImage;
    public Text ItemPrice;
    public upgradesSO Upgrade; // items que l'on regarde pour acheté

    public void BuyItem()
    {
        bool containLvLInferior = false;
        bool containLvL1 = false;
        int rasp = 0;
        if (PhotonNetwork.IsConnected)
            rasp = GameObject.FindGameObjectWithTag("InventaireM").GetComponent<inventory>().NombreDeRaspberries;
        else
            rasp = GameObject.FindGameObjectWithTag("Inventaire").GetComponent<inventory>().NombreDeRaspberries;
        if (rasp >= Upgrade.Price) 
        {
            if (Upgrade.name[Upgrade.name.Length - 1] == '1') // le dernier caractères du noms de l'items est son niveau
            {
                containLvLInferior = true;
            }

            else
            {
                containLvL1 = true;
            }
            
            string itemNameWithoutNumber = "";
            for (int i = 0; i < Upgrade.name.Length-2; i++)
            {
                itemNameWithoutNumber = itemNameWithoutNumber + Upgrade.name[i];
            }
            
            if (containLvL1) // Je vérifie que si on veut acheté l'item niv 2, il y a bien l'item niv 1
            {
                if (Upgrade.name.Contains("Midas"))
                {
                    if (GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>()
                        .AmeliorationJar == (Upgrade.name[Upgrade.name.Length - 1] - '0') - 1)
                    {
                        containLvLInferior = true;
                    }
                }
                
                else if (Upgrade.name.Contains("FeudecampStonks"))
                {
                    if ((-GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>()
                        .AmeliorationFeuDeCamps + 0.6f)*10 == (Upgrade.name[Upgrade.name.Length - 1] - '0') - 1)
                    {
                        containLvLInferior = true;
                    }
                }
                else if (Upgrade.name.Contains("Bank"))
                {
                    if ((GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>()
                        .AmelioriationBank)/25 == (Upgrade.name[Upgrade.name.Length - 1] - '0') - 1)
                    {
                        containLvLInferior = true;
                    }
                }
                
                else if (Upgrade.name.Contains("Oscillococcinum"))
                {
                    if ((GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>()
                        .AmeliorationStress-200)/20 == (Upgrade.name[Upgrade.name.Length - 1] - '0') - 1)
                    {
                        containLvLInferior = true;
                    }
                }
                
                else if (Upgrade.name.Contains("Random"))
                {
                    if (GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>().AmeliorationRandomLevel == (Upgrade.name[Upgrade.name.Length - 1] - '0') - 1)
                    {
                        containLvLInferior = true;
                    }
                }
                
               /* if (PhotonNetwork.IsConnected)
                {
                    foreach (var VARIABLE in upgradesInventory.instance.content)
                    {
                        if (VARIABLE.name.Contains(itemNameWithoutNumber))
                        {
                            if ((int) Upgrade.name[Upgrade.name.Length - 1] - 1 ==
                                (int) VARIABLE.name[VARIABLE.name.Length - 1])
                            {
                                containLvLInferior = true;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var upgrade in GameObject.FindGameObjectWithTag("UpInv").GetComponent<upgradesInventory>().content)
                    {
                        if (upgrade.name.Contains(itemNameWithoutNumber))
                        {
                            if ((int) Upgrade.name[Upgrade.name.Length - 1] - 1 ==
                                (int) upgrade.name[upgrade.name.Length - 1])
                            {
                                containLvLInferior = true;
                            }
                        }
                    }
                }*/
            }
            
            
            if (!Upgrade.name.Contains("Unavailable") && containLvLInferior) // ajout des items à l'inventaire upgrade etc
            {
                if (PhotonNetwork.IsConnected)
                {
                    GameObject[] inventories = GameObject.FindGameObjectsWithTag("InventaireM");
                    foreach (var inventory in inventories)
                    {
                        inventory.GetComponent<inventory>().AddRaspberries(-Upgrade.Price, true);
                    }

                    upgradesInventory Upinv = GameObject.FindGameObjectWithTag("UpInv").GetComponent<upgradesInventory>();
                    Upinv.GetComponent<upgradesInventory>().content.Add(Upgrade);
                    Upinv.GetComponent<upgradesInventory>().AddEffectAmelioration(Upgrade);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Inventaire").GetComponent<inventory>().AddRaspberries(-Upgrade.Price,false);
                    GameObject.FindGameObjectWithTag("Inventaire2").GetComponent<inventory>().AddRaspberries(-Upgrade.Price,false);
                    upgradesInventory UP1 = GameObject.FindGameObjectWithTag("UpInv").GetComponent<upgradesInventory>();
                    upgradesInventory UP2 = GameObject.FindGameObjectWithTag("UpInv2").GetComponent<upgradesInventory>();
                    UP1.content.Add(Upgrade);
                    UP2.content.Add(Upgrade);
                    UP1.AddEffectAmelioration(Upgrade);
                    
                }
                /*upgradesInventory.instance.content.Add(Upgrade);
                upgradesInventory.instance.AddEffectAmelioration(Upgrade);
                inventory.instance.NombreDeRaspberries -= Upgrade.Price;*/
                Upgrade.name = "Unavailable" + Upgrade.name;
            }
        }
    }
}
