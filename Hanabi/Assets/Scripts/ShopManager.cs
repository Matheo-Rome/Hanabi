using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
   
    public Animator animator;

    public GameObject sellbuttonPrefab;
    public Transform sellbuttonsParent;
    
    public Text pnjNameText;
    
    //permet d'initialiser la file avec dedans les phrases du pnj à print
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ShopManager dans la scène");
            return;
        }

        instance = this;
    }

    public void OpenShop(Items[] items, string pnjName)
    {
        pnjNameText.text = pnjName;
        UpdateItemToSell(items);
        animator.SetBool("isOpen", true);

    }

    void UpdateItemToSell(Items[] items)
    {
        
        // supprime les potentiels boutons présent dans le parents
        for (int i = 0; i < sellbuttonsParent.childCount; i++)
        {
            Destroy(sellbuttonsParent.GetChild(i).gameObject);
        }
        
        // Instancie un bouton pour chaque item à vendre et le configure (peut-être créer un array ?)
        for (int i = 0; i < items.Length; i++)
        {
            GameObject button = Instantiate(sellbuttonPrefab, sellbuttonsParent);
            SellButtonItem buttonScript = button.GetComponent<SellButtonItem>();
            buttonScript.ItemName.text = items[i].name;
            buttonScript.ItemImage.sprite = items[i].image;
            buttonScript.ItemPrice.text = items[i].Price.ToString();

            buttonScript.item = items[i];
            
            button.GetComponent<Button>().onClick.AddListener(delegate { buttonScript.BuyItem(); });
        }
    }
    
    
    public void CloseShop()
    {
        animator.SetBool("isOpen", false);
    }
}
