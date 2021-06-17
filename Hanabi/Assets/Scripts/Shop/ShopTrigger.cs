using UnityEngine;
using UnityEngine.UI;

public class ShopTrigger : MonoBehaviour
{
    private bool isInRange;

    private Text interactUI;

    public string pnjName;
    public Items[] itemsToSell;

    private bool HasTalked;

    private void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !HasTalked)
        {
            ShopManager.instance.OpenShop(itemsToSell, pnjName);
            /*foreach (var item in itemsToSell)
            {
                item.Available = true;
            }*/
            HasTalked = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.CompareTag("Player1")) && !HasTalked)
        {
            isInRange = true;
            interactUI.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Player1"))
        {
            isInRange = false;
            interactUI.enabled = false;
            ShopManager.instance.CloseShop();
        }
    }
}