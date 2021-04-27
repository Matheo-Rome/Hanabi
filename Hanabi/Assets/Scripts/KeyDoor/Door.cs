using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    private PlayerMovement player;

    //portefermée
    public SpriteRenderer theSR;
    //porte ouverte
    public Sprite doorOpenSprite;
    [SerializeField] private Transform _searchPlayer;
    private PlayerMovement Player;
    

    public bool doorOpen, waitingToOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
        float min = 1000000f;
        foreach (var player in players)
        { 
            if (Vector3.Distance(player.transform.position, _searchPlayer.position) < min)
            {
               Player = player;
               min =Vector3.Distance(player.transform.position, _searchPlayer.position);
           }
       }
       
    }

    // Ouvre la porte en changeant l'asset de la porte
    void Update()
    {
        if (waitingToOpen)
        {
            if (Vector3.Distance(Player.followingKey.transform.position, transform.position) < 0.1f)
            {
                waitingToOpen = false;
                doorOpen = true;
                theSR.sprite = doorOpenSprite;
                Player.followingKey.gameObject.SetActive(false);
                Player.followingKey = null;
                gameObject.SetActive(false);

            }
        }
        //pas sûr de cette ligne là
        //pour reload la scène pour retester
        if (doorOpen && Vector3.Distance(Player.transform.position, transform.position) < 1f && Input.GetAxis("Vertical") > 0.1f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Player.followingKey != null)
            {
                Player.followingKey.followTarget = transform;
                waitingToOpen = true;
            }
        }
    }
}
