using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;
    private Queue<string> sentences;
    
    public static DialogueManager instance;
   
    
    //permet d'initialiser la file avec dedans les phrases du pnj à print
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DialogueManager dans la scène");
            return;
        }

        instance = this;

        sentences = new Queue<string>();
    }


    //Lance l'animation pour afficher la fenetre de dialogue
    //Affiche le nom du pnj
    //ajoute dans la file les phrases du pnj à print
    //Va print chacun des éléments de la file
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        
        nameText.text = dialogue.name;
        
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    
    //Va appeler TypeSentence qui affiche progressivement le texte sur chacune des phrases de la file
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
    }

    
    //Affiche progressivement les lettres des phrases
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }

    
    //Active l'animation qui va fermer la fenetre de dialogue
    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
}
