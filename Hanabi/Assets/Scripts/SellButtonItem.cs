using UnityEngine;
using UnityEngine.UI;

public class SellButtonItem : MonoBehaviour
{
    public Text ItemName;
    public Image ItemImage;
    public Text ItemPrice;
    public Items item;


    public void BuyItem()
    {
        if (inventory.instance.NombreDePièce >= item.Price)
        {
            InventairePassif.instance.content.Add(item);
            InventairePassif.instance.Start();
            InventairePassif.instance.AddEffectItem(item);
            inventory.instance.NombreDePièce -= item.Price;
            inventory.instance.UpdateTextUI();
        }
    }
}
