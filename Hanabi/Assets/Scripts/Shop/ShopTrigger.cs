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

        if (!founded)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Package");
            foreach (var player in players)
            {
                if (!PhotonNetwork.IsMasterClient)
                    interactUI.text = "";
            }
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