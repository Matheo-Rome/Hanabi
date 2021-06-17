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
        
        
        if (inventory.instance.NombreDeRaspberries >= Upgrade.Price) 
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
                foreach (var VARIABLE in upgradesInventory.instance.content)
                {
                    if (VARIABLE.name.Contains(itemNameWithoutNumber))
                    {
                        if ((int)Upgrade.name[Upgrade.name.Length - 1]-1 == (int)VARIABLE.name[VARIABLE.name.Length - 1])
                        {
                            containLvLInferior = true;
                        }
                    }
                }
            }
            
            
            if (!Upgrade.name.Contains("Unvailable") && containLvLInferior) // ajout des items à l'inventaire upgrade etc
            {
                upgradesInventory.instance.content.Add(Upgrade);
                upgradesInventory.instance.AddEffectAmelioration(Upgrade);
                inventory.instance.NombreDeRaspberries -= Upgrade.Price;
                Upgrade.name = "Unvailable" + Upgrade.name;
            }
        }
    }
}
