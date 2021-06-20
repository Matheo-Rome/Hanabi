using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class AmeliorationTrigger : MonoBehaviour
{
    private bool isInRange;

    private Text interactUI;

    public string pnjName;
    public upgradesSO[] AmeliorationTosell;

    public bool Istalking = false;

    private void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }
// script basique pour parlé à un pnj et démarrer son interface
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !Istalking)
        {
            AmeliorationManager.instance.OpenShop(AmeliorationTosell, pnjName);
            Istalking = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.transform.parent.gameObject.GetPhotonView().IsMine|| collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            isInRange = true;
            interactUI.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Player1") || collision.CompareTag("Player2")) 
        {
            isInRange = false;
            interactUI.enabled = false;
            AmeliorationManager.instance.CloseShop();
            Istalking = false;
        }
    }
}

