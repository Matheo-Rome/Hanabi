using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CaptorTrigger : MonoBehaviour
{
    public SpriteRenderer theSR;
    public Sprite buttonOff;
    public Sprite buttonOn;
    private BoxCollider2D collider2D;

    public bool IsActive;
    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            theSR.sprite = buttonOn;
            IsActive = true;
            PlayerMovement.instance.hasDashed = false;
            collider2D.enabled = false;
        }
    }

    public void Desactivate()
    {
        IsActive = false;
        theSR.sprite = buttonOff;
        collider2D.enabled = true;
    }
}
