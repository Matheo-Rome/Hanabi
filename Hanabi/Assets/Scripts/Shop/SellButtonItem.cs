using UnityEngine;
using UnityEngine.UI;

public class SellButtonItem : MonoBehaviour
{
    public Text ItemName;
    public Image ItemImage;
    public Text ItemPrice;
    public Items item;

    int reduction = 0;
    int nouveauxprix = 0;
    public void BuyItem()
    {
        if (inventory.instance.NombreDePièce >= item.Price)
        {

            reduction = 0;
            nouveauxprix = 0;

            foreach (var objet in InventairePassif.instance.content)
            {
                reduction += objet.ReducePrice;
            }

            nouveauxprix = (item.Price - reduction);
            if (nouveauxprix < 0)
            {
                nouveauxprix = 0;
            }

            InventairePassif.instance.content.Add(item);
            InventairePassif.instance.Start();
            InventairePassif.instance.AddEffectItem(item);
            inventory.instance.NombreDePièce -= nouveauxprix;
            inventory.instance.UpdateTextUI();
            
        }
    }
}
