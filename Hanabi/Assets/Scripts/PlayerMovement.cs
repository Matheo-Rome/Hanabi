using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using Object = System.Object;


public class PlayerMovement : MonoBehaviourPun
{
    
    public static PlayerMovement instance;
    
    //Mouvement
    public float jumpVelocity;
    public float speed;        
    public float slideSpeed; 
       

    private Rigidbody2D rb;
    private Collider2D rc;

    //Checker de position
    public Transform groundCheckLeft;   
    //public Transform groundCheckRight;
    public Transform wallCheckRight; 
    //public Transform wallCheckRight2;
    public Transform wallCheckLeft;
    //public Transform wallChekLeft2;

    public Transform keyFollowPoint;

    public Key followingKey;

    public bool onGround;
    public bool onWall;
    public bool hasDashed;
    
    //Dash
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    public int direction;
    private float dashCD = 0.5f;
    private float NextDash;

	public bool itemJump;

    public GameObject playerCamera;
    



    private void Awake()
    {
        // Il faut qu'il n'y ai qu'un seul et unique inventaire
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de mouvement dans la scène");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        dashTime = startDashTime;
        
        if (photonView.IsMine)  //Active la caméra du joueur est éteint celle de l'autre joueur
        {
            playerCamera.SetActive(true);
        }
        else
        {
            playerCamera.SetActive(false);
        }
    }


    void Update()
    {
        //Information sur la direction du déplacement selon les touches appuyé
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, 0);
        
        //Vérifie la position du personnage par rapport au sol et aux murs.
        onGround = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckLeft.position);
        onWall = Physics2D.OverlapArea(wallCheckRight.position, wallCheckRight.position) || 
                 Physics2D.OverlapArea(wallCheckLeft.position, wallCheckLeft.position);
        
        //Jump
       if (Input.GetButtonDown("Jump") && onGround || itemJump)
           Jump();

       
        Walk(dir);

       //Réduction de la vitesse de déplacement sur l'axe x dans les airs 
       if (!onGround)
           SlowAir(dir);

       //Sliding
        if (onWall && !onGround && !Input.GetButtonDown("Jump"))
            Slide();
        
        //Dash
        if (direction == 0)
            Dashdir();
        else
            Dash();
        
        //Reset du Dash quand le personnage touche le sol
        if (onGround)
            hasDashed = false;
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpVelocity;
        itemJump = false;
    }

    private void Walk(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
    }

    private void Slide()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -slideSpeed, float.MaxValue));
    }

    private void Dashdir()
    {
        if (Input.GetButton("Dash") && !hasDashed && Time.time > NextDash)
        {
            if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") < 0) //diagonal haute gauche
                direction = 1;
            else if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") > 0) //diagonal haute droite
                direction = 2;
            else if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") < 0) // diagonal basse gauche
                direction = 3;
            else if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") > 0) //diagonal basse droite
                direction = 4;
            else if (Input.GetAxis("Vertical") > 0) //haut
                direction = 5;
            else if (Input.GetAxis("Vertical") < 0) //bas
                direction = 6;
            else if (Input.GetAxis("Horizontal") < 0) //gauche
                direction = 7;
            else if (Input.GetAxis("Horizontal") > 0) //droite
                direction = 8;
            NextDash = Time.time + dashCD;
        }   
    }

    private void Dash()
    {
        if (dashTime <= 0)
        {
            direction = 0;
            dashTime = startDashTime;
            rb.velocity /= 3;
        }
        else
        {
            dashTime -= Time.deltaTime;
            hasDashed = true;
            switch (direction)
            {
                case 1 :
                    rb.velocity = (Vector2.up + Vector2.left).normalized * dashSpeed; //diagonal haute gauche
                    break;
                case 2 :
                    rb.velocity = (Vector2.up + Vector2.right).normalized * dashSpeed;  //diagonal haute droite
                    break;
                case 3 :
                    rb.velocity = (Vector2.down + Vector2.left).normalized * dashSpeed; // diagonal basse gauche
                    break;
                case 4:
                    rb.velocity = (Vector2.down + Vector2.right).normalized * dashSpeed; //diagonal basse droite
                    break;
                case 5 :
                    rb.velocity = Vector2.up * dashSpeed; //haut
                    break;
                case 6 :
                    rb.velocity = Vector2.down * dashSpeed;  //bas
                    break;
                case 7 :
                    rb.velocity = Vector2.left * dashSpeed; //gauche
                    break;
                case 8 :
                    rb.velocity = Vector2.right * dashSpeed; //droite
                    break;
            }
        }
    }

    private void SlowAir(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed / 1.4f, rb.velocity.y);
    }
}

   
