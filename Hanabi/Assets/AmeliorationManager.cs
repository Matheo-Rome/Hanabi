using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class AmeliorationManager : MonoBehaviour
{
    public static AmeliorationManager instance;

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


    public void UpdateItemToSell(Items[] items)
    {
        // supprime les potentiels boutons présent dans le parents
        for (int i = 0; i < sellbuttonsParent.childCount; i++)
        {
            Destroy(sellbuttonsParent.GetChild(i).gameObject);
        }

        // Instancie un bouton pour chaque item à vendre et le configure
        for (int i = 0; i < items.Length; i++)
        {
            GameObject button =  Instantiate(sellbuttonPrefab, sellbuttonsParent);
            AmeliorationButtonIteam buttonScript = button.GetComponent<AmeliorationButtonIteam>();
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