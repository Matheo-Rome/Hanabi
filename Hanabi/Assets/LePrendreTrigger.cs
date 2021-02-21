using UnityEngine;
using UnityEngine.UI;

public class LePrendreTrigger : MonoBehaviour
{
    private bool isInRange;
    private Text interactUI;
    
    
    
    
    private void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            interactUI.enabled = true;
        }
    }

   
    //Test la proximité du player et du pnj
    //Se met sur false si leur boites de collision ne sont pas en contact
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            interactUI.enabled = false;
        }
    }

}
