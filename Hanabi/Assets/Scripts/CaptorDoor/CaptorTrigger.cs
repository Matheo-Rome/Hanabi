using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptorTrigger : MonoBehaviour
{
    public SpriteRenderer theSR;
    public Sprite buttonOff;
    public Sprite buttonOn;

    public bool IsActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive)
            Destroy(GetComponent<BoxCollider2D>());
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            theSR.sprite = buttonOn;
            IsActive = true;
            
        }
    }
}
