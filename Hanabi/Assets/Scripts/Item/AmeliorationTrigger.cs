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
            
            foreach (var Upgrade in AmeliorationTosell)
        {
            if (Upgrade.name.Contains("Midas"))
            {
                if (GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>()
                    .AmeliorationJar >= (Upgrade.name[Upgrade.name.Length - 1] - '0'))
                {
                    if (!Upgrade.name.Contains("Unavailable"))
                    {
                        Upgrade.name = "Unavailable" + Upgrade.name;
                    }
                }
            }
                
            else if (Upgrade.name.Contains("FeudecampStonks"))
            {
                if ((-GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>()
                    .AmeliorationFeuDeCamps + 0.6f) * 10 >= (Upgrade.name[Upgrade.name.Length - 1] - '0'))
                {
                    if (!Upgrade.name.Contains("Unavailable"))
                    {
                        Upgrade.name = "Unavailable" + Upgrade.name;
                    }
                }
            }
            else if (Upgrade.name.Contains("Bank"))
            {
                if ((GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>()
                    .AmelioriationBank) / 25 >= (Upgrade.name[Upgrade.name.Length - 1] - '0'))
                {
                    if (!Upgrade.name.Contains("Unavailable"))
                    {
                        Upgrade.name = "Unavailable" + Upgrade.name;
                    }
                }
            }
                
            else if (Upgrade.name.Contains("Oscillococcinum"))
            {
                if ((GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>()
                    .AmeliorationStress - 200) / 20 >= (Upgrade.name[Upgrade.name.Length - 1] - '0'))
                {
                    if (!Upgrade.name.Contains("Unavailable"))
                    {
                        Upgrade.name = "Unavailable" + Upgrade.name;
                    }
                }
            }
                
            else if (Upgrade.name.Contains("Random"))
            {
                if (GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>()
                    .AmeliorationRandomLevel >= (Upgrade.name[Upgrade.name.Length - 1] - '0') - 1)
                {
                    if (!Upgrade.name.Contains("Unavailable"))
                    {
                        Upgrade.name = "Unavailable" + Upgrade.name;
                    }
                }
            }
        }
        
            
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
