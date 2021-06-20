using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
   public Dialogue dialogue;

   private bool isInRange;

   private Text interactUI;
   

   private void Awake()
   {
      interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
   }
      
   
   //Permet de lancer la fonction pour initialiser le dialogue quand le player est à proximité et qu'il appuie sur E
   void Update()
   {
      if (isInRange && Input.GetKeyDown(KeyCode.E))
      {
         TriggerDialogue();
      }
   }

   
   //Test la proximité du player et du pnj
   //Se met sur true si leur boites de collision sont en contact
   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.CompareTag("Player") && collision.transform.parent.gameObject.GetPhotonView().IsMine || collision.CompareTag("Player1") || collision.CompareTag("Player2"))
      {
         isInRange = true;
         interactUI.enabled = true;
      }
   }

   
   //Test la proximité du player et du pnj
   //Se met sur false si leur boites de collision ne sont pas en contact
   private void OnTriggerExit2D(Collider2D collision)
   {
      if (collision.CompareTag("Player") || collision.CompareTag("Player1") || collision.CompareTag("Player2"))
      {
         isInRange = false;
         interactUI.enabled = false;
         DialogueManager.instance.EndDialogue();
      }
   }

   
   //Lance la fonction StartDialogue du fichier DialogueManager
   void TriggerDialogue()
   {
      DialogueManager.instance.StartDialogue(dialogue);
   }
}
