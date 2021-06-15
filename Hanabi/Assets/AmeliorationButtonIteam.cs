using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class AmeliorationButtonIteam : MonoBehaviour
{
    public Text ItemName;
    public Image ItemImage;
    public Text ItemPrice;
    public Items item;

    public void BuyItem()
    {
        bool containLvLInferior = false;
        bool containLvL1 = false;
        
        if (inventory.instance.NombreDePièce >= item.Price) 
        {
            if (item.name[item.name.Length - 1] == '1')
            {
                containLvLInferior = true;
            }

            else
            {
                containLvL1 = true;
            }
            
            string itemNameWithoutNumber = "";
            for (int i = 0; i < item.name.Length-2; i++)
            {
                itemNameWithoutNumber = itemNameWithoutNumber + item.name[i];
            }
            
            if (containLvL1)
            {
                foreach (var VARIABLE in InventairePassif.instance.content)
                {
                    if (VARIABLE.name.Contains(itemNameWithoutNumber))
                    {
                        if ((int)item.name[item.name.Length - 1]-1 == (int)VARIABLE.name[VARIABLE.name.Length - 1])
                        {
                            containLvLInferior = true;
                        }
                    }
                }
            }
            
            
            if (!item.name.Contains("Unvailable") && containLvLInferior)
            {
                InventairePassif.instance.content.Add(item);
                InventairePassif.instance.Start();
                InventairePassif.instance.AddEffectItem(item);
                inventory.instance.NombreDePièce -= item.Price;
                inventory.instance.UpdateTextUI();
                item.name = "Unvailable" + item.name;
            }
        }
    }
}
