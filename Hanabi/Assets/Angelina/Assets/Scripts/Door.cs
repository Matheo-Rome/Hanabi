using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    private PlayerMovement player;

    //portefermée
    public SpriteRenderer theSR;
    //porte ouverte
    public Sprite doorOpenSprite;
    

    public bool doorOpen, waitingToOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Ouvre la porte en changeant l'asset de la porte
    void Update()
    {
        if (waitingToOpen)
        {
            if (Vector3.Distance(player.followingKey.transform.position, transform.position) < 0.1f)
            {
                waitingToOpen = false;
                doorOpen = true;
                theSR.sprite = doorOpenSprite;
                player.followingKey.gameObject.SetActive(false);
                player.followingKey = null;
                gameObject.SetActive(false);
                
               
            }
        }

        
        //pas sûr de cette ligne là
        //pour reload la scène pour retester
        if (doorOpen && Vector3.Distance(player.transform.position, transform.position) < 1f && Input.GetAxis("Vertical") > 0.1f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (player.followingKey != null)
            {
                player.followingKey.followTarget = transform;
                waitingToOpen = true;
            }
        }
    }
}
