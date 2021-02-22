using UnityEngine;
using UnityEngine.UI;

public class SellButtonItem : MonoBehaviour
{
    public Text ItemName;
    public Image ItemImage;
    public Text ItemPrice;

    public Items item;

    public bool ok;

    public void BuyItem()
    {
        if (inventory.instance.NombreDePièce >= item.Price && ok)
        {
            InventairePassif.instance.content.Add(item);
            InventairePassif.instance.Start();
            inventory.instance.NombreDePièce -= item.Price;
            inventory.instance.UpdateTextUI();
            ok = false;
        }
    }
}
