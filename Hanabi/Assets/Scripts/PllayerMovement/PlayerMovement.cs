﻿using System;
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
     public Transform groundCheckRight;
     public Transform wallCheckRight;
     public Transform wallCheckRight2;
     public Transform wallCheckLeft;
     public Transform wallCheckLeft2;

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
     private bool isDashing;
     private Vector2 dir;

     public bool ClassiqueDash;
     public bool BouncyDash;
     private bool isBouncydashing;
     public bool LightDash;

     public bool itemJump;

     public GameObject playerCamera;

     private Animator animator;
     private SpriteRenderer spriteRenderer;

     public bool player1;
     public bool player2;

     public GameObject player;
     private Collider2D collider;




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
         animator = GetComponent<Animator>();
         spriteRenderer = GetComponent<SpriteRenderer>();
         collider = GetComponent<PolygonCollider2D>();

         if (photonView.IsMine) //Active la caméra du joueur est éteint celle de l'autre joueur
         {
             playerCamera.SetActive(true);
         }
         else
         {
             playerCamera.SetActive(false);
         }
     }


     void FixedUpdate()
     {
         //Information sur la direction du déplacement selon les touches appuyé
         float x = Input.GetAxis("Horizontal");
         float y = Input.GetAxis("Vertical");
         float xRaw = Input.GetAxisRaw("Horizontal");
         float yRaw = Input.GetAxisRaw("Vertical");
         Vector2 dir = new Vector2(x, 0);

         //Vérifie la position du personnage par rapport au sol et aux murs.
         onGround = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
         onWall = Physics2D.OverlapArea(wallCheckRight.position, wallCheckRight2.position) ||
                  Physics2D.OverlapArea(wallCheckLeft.position, wallCheckLeft2.position);

         //Jump
         if (Input.GetButton("Jump") && onGround || itemJump)
             Jump();

         if (!isDashing)
             Walk(dir);

         animator.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
         animator.SetFloat("SpeedY", rb.velocity.y);
         animator.SetBool("GroundColl",onGround);
         animator.SetBool("WallColl",onWall);
         Flip(rb.velocity.x);

         //Réduction de la vitesse de déplacement sur l'axe x dans les airs 
         if (!onGround)
             SlowAir(dir);

         //Sliding
         if (onWall && !onGround && !Input.GetButton("Jump") && !isBouncydashing)
             Slide();

         //Dash Classique
         if (direction == 0 && ClassiqueDash)
             DashdirClassique();
         else if (ClassiqueDash)
             DashClassique();


         //Dash Bouncy
         if (direction == 0 && BouncyDash)
             DashdirBouncy();
         else if (BouncyDash)
             DashBouncy();

         //Dash Light
         if (direction == 0 && LightDash)
             DashdirLight();
         else if (LightDash)
             if (collTest(dir))
                 DashLight();
             else
             {
                 direction = 0;
                 dashTime = startDashTime;
                 rb.velocity = Vector2.zero;
                 isDashing = false;
                 spriteRenderer.enabled = true;
                 collider.enabled = true;
             }
         
         //Reset du Dash quand le personnage touche le sol
         if (onGround)
             hasDashed = false;

         if (rb.velocity.y < 0)
             isBouncydashing = false;
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

    private void DashdirClassique()
    {
        if (Input.GetButton("Dash") && !hasDashed && Time.time >= NextDash)
        {
            rb.velocity = Vector2.zero;
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
            isDashing = true;
        }   
    }

    private void DashClassique()
    {
        if (dashTime <= 0)
        {
            direction = 0;
            dashTime = startDashTime;
            rb.velocity /= 3;
            isDashing = false;
        }
        else
        {
            dashTime -= Time.deltaTime;
            hasDashed = true;
            NextDash = Time.time + dashCD;
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

    private void DashdirBouncy()
    {
        if (Input.GetButton("Dash") && !hasDashed && Time.time >= NextDash)
        {
            rb.velocity = Vector2.zero;

            if (Input.GetAxis("Horizontal") < 0) //gauche
                direction = 1;
            else if (Input.GetAxis("Horizontal") > 0) //droite
                direction = 2;
            isDashing = true;
        }   
    }

    private void DashBouncy()
    {
        if (dashTime <= 0)
        {
            direction = 0;
            dashTime = startDashTime;
            rb.velocity /= 3;
            isDashing = false;
            if (onWall)
            {
                rb.velocity = Vector2.up * jumpVelocity*2;
                isBouncydashing = true;
            }
        }
        else
        {
            dashTime -= Time.deltaTime;
            hasDashed = true;
            NextDash = Time.time + dashCD;
            switch (direction)
            {
                case 1:
                    rb.velocity = Vector2.left * dashSpeed; //gauche
                    break;
                case 2:
                    rb.velocity = Vector2.right * dashSpeed; //droite
                    break;
            }
        }
    }

    private void DashdirLight()
    {
        if (Input.GetButton("Dash") && !hasDashed && Time.time >= NextDash)
        {
            rb.velocity = Vector2.zero;

            if (Input.GetAxis("Horizontal") < 0) //gauche
            {
                direction = 1;
                dir = Vector2.left;
            }
            else if (Input.GetAxis("Horizontal") > 0) //droite
            {
                direction = 2;
                dir = Vector2.right;
            }
            else if (Input.GetAxis("Vertical") > 0) //haut
            {
                direction = 3;
                dir = Vector2.up;
            }
            spriteRenderer.enabled = false;
            collider.enabled = false;
            isDashing = true;
        }   
    }

    private void DashLight()
    {
        if (dashTime <= 0)
        {
            direction = 0;
            dashTime = startDashTime;
            rb.velocity = Vector2.zero;
            isDashing = false;
            spriteRenderer.enabled = true;
            collider.enabled = true;
        }
        else
        {
            dashTime -= Time.deltaTime;
            hasDashed = true;
            NextDash = Time.time + dashCD;
            switch (direction)
            {
                case 1:
                    rb.velocity = Vector2.left * dashSpeed; //gauche
                    break;
                case 2:
                    rb.velocity = Vector2.right * dashSpeed; //droite
                    break;
                case 3 :
                    rb.velocity = Vector2.up * dashSpeed; //haut
                    break;
            }
        }
    }

    private bool collTest(Vector2 dir)
    {
        var tab = Physics2D.RaycastAll(player.transform.position, dir, 2f);
        if (tab.Length % 2 == 1)
            return false;
        else
            return true;
    }
    private void SlowAir(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed / 1.4f, rb.velocity.y);
    }

    private void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
            spriteRenderer.flipX = false;
        else if (_velocity < -0.1f)
            spriteRenderer.flipX = true;
    }
 }
