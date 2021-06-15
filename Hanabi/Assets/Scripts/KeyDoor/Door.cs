using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    //private PlayerMovement player;
   //private PlayerMovementSolo playerS;

    //portefermée
    public SpriteRenderer theSR;
    //porte ouverte
    public Sprite doorOpenSprite;
    [SerializeField] private Transform _searchPlayer;
    
    public PlayerMovement Player;
    public PlayerMovementSolo PlayerS;

    public GameObject PlayerSS;

    public bool isDown;

    private bool founded = false;
    

    public bool doorOpen, waitingToOpen;
    
    // Start is called before the first frame update
    void Start()
    {
       /* if (PhotonNetwork.IsConnected)
        {
            PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
            float min = 1000000f;
            foreach (var player in players)
            {
                if (Vector3.Distance(player.transform.position, _searchPlayer.position) < min)
                {
                    Player = player;
                    min = Vector3.Distance(player.transform.position, _searchPlayer.position);
                }
            }
        }
        else
        {
            GameObject p1 = GameObject.FindGameObjectWithTag("Player1");
            if (isDown)
                PlayerSS = GameObject.FindGameObjectWithTag("Player1");
            else
                PlayerSS = GameObject.FindGameObjectWithTag("Player2");
        }*/
    }

    // Ouvre la porte en changeant l'asset de la porte
    void Update()
    {
        if (!founded)
        {
            if (PhotonNetwork.IsConnected)
            {
                PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
                float min = 1000000f;
                foreach (var player in players)
                {
                    if (Vector3.Distance(player.transform.position, _searchPlayer.position) < min)
                    {
                        Player = player;
                        min = Vector3.Distance(player.transform.position, _searchPlayer.position);
                    }
                }
            }
            else
            {
                if (isDown)
                    PlayerS = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerMovementSolo>();
                else
                    PlayerS = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerMovementSolo>();
            }

            if (PlayerS != null || Player != null)
                founded = true;
        }
        else
        {
            if (PhotonNetwork.IsConnected)
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
                if (doorOpen && Vector3.Distance(Player.transform.position, transform.position) < 1f &&
                    Input.GetAxis("Vertical") > 0.1f)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            else
            {
                if (waitingToOpen)
                {
                    if (Vector3.Distance(PlayerS.followingKey.transform.position, transform.position) < 0.1f)
                    {
                        waitingToOpen = false;
                        doorOpen = true;
                        theSR.sprite = doorOpenSprite;
                        PlayerS.followingKey.gameObject.SetActive(false);
                        PlayerS.followingKey = null;
                        gameObject.SetActive(false);

                    }
                }

                //pas sûr de cette ligne là
                //pour reload la scène pour retester
                if (doorOpen && Vector3.Distance(PlayerS.transform.position, transform.position) < 1f &&
                    Input.GetAxis("Vertical") > 0.1f)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PhotonNetwork.IsConnected)
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
        else
        {
            if (other.tag == "Player1" || other.tag == "Player2")
            {
                if (PlayerS.followingKey != null)
                {
                    PlayerS.followingKey.followTarget = transform;
                    waitingToOpen = true;
                }
            }
        }
    }
}
