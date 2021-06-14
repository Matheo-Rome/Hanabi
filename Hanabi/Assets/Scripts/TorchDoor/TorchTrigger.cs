using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchTrigger : MonoBehaviour
{
    public bool IsActive;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    private void OnTriggerStay2D(Collider2D collider)
    {
        if ((collider.CompareTag("Player") || collider.CompareTag("Player1")) && Input.GetKey(KeyCode.E))
        {
            animator.SetBool("On",true);
            IsActive = true;
            
        }
    }

    public void Off()
    {
        animator.SetBool("On",false);
        IsActive = false;
    }
}

