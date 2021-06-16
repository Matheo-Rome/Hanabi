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

    public void OpenShop(upgradesSO[] Upgrade, string pnjName)
    {
        pnjNameText.text = pnjName;
        UpdateUpgradeToSell(Upgrade);
        animator.SetBool("isOpen", true);
    }


    public void UpdateUpgradeToSell(upgradesSO[] Upgrade)
    {
        // supprime les potentiels boutons présent dans le parents
        for (int i = 0; i < sellbuttonsParent.childCount; i++)
        {
            Destroy(sellbuttonsParent.GetChild(i).gameObject);
        }

        // Instancie un bouton pour chaque item à vendre et le configure
        for (int i = 0; i < Upgrade.Length; i++)
        {
            GameObject button =  Instantiate(sellbuttonPrefab, sellbuttonsParent);
            AmeliorationButtonIteam buttonScript = button.GetComponent<AmeliorationButtonIteam>();
            buttonScript.ItemName.text = Upgrade[i].name;
            buttonScript.ItemImage.sprite = Upgrade[i].image;
            buttonScript.ItemPrice.text = Upgrade[i].Price.ToString();
            buttonScript.Upgrade = Upgrade[i];
            button.GetComponent<Button>().onClick.AddListener(delegate { buttonScript.BuyItem(); });
        }
    }


    public void CloseShop()
    {
        animator.SetBool("isOpen", false);
    }
}