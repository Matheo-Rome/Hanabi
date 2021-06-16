using UnityEngine;
using UnityEngine.UI;

public class AmeliorationTrigger : MonoBehaviour
{
    private bool isInRange;

    private Text interactUI;

    public string pnjName;
    public Items[] AmeliorationTosell;

    public bool Istalking = false;

    private void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

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
        if (collision.CompareTag("Player") || collision.CompareTag("Player1") || collision.CompareTag("Player2"))
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

