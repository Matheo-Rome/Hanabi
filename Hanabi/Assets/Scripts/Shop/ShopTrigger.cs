using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ShopTrigger : MonoBehaviourPunCallbacks
{
    private bool isInRange;

    private Text interactUI;

    public string pnjName;
    public Items[] itemsToSell;

    private bool HasTalked;
    private bool founded = false;

    private void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
        
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !HasTalked)
        {
            if (interactUI.text != "")
            {
                ShopManager.instance.OpenShop(itemsToSell, pnjName);
                HasTalked = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((collision.CompareTag("Player") && collision.transform.parent.gameObject.GetPhotonView().IsMine)|| collision.CompareTag("Player1")) && !HasTalked)
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