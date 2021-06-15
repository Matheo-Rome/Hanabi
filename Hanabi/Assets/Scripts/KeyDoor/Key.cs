using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isFollowing;

    public float followSpeed;

    public Transform followTarget;
    
    
    
    void Update()
    {
        
        if (isFollowing)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
            Destroy(GetComponent<BoxCollider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            if (!isFollowing)
            {
                if (PhotonNetwork.IsConnected)
                {
                    PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
                    float min = 10000000f;
                    PlayerMovement Closer = null;
                    foreach (var player in players)
                    {
                        if (Vector3.Distance(collision.transform.position, player.transform.position) < min)
                        {
                            min = Vector3.Distance(collision.transform.position, player.transform.position);
                            Closer = player;
                        }
                    }
                    followTarget = Closer.keyFollowPoint;

                    isFollowing = true;
                    Closer.followingKey = this;
                }
                else
                {
                    PlayerMovementSolo[] players = FindObjectsOfType<PlayerMovementSolo>();
                    float min = 10000000f;
                    PlayerMovementSolo Closer = null;
                    foreach (var player in players)
                    {
                        if (Vector3.Distance(collision.transform.position, player.transform.position) < min)
                        {
                            min = Vector3.Distance(collision.transform.position, player.transform.position);
                            Closer = player;
                        }
                    }
                    followTarget = Closer.keyFollowPoint;

                    isFollowing = true;
                    Closer.followingKey = this;
                }

            }
        }
        
    }
}
