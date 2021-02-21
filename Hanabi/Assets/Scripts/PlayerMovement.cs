using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public static PlayerMovement instance;
    
    [Range(1, 10)] public float jumpVelocity;
    public float speed;
    public float slideSpeed;

    private Rigidbody2D rb;
    private Collider2D rc;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public Transform wallCheckRight;
    public Transform wallCheckRight2;

    public Transform wallCheckLeft;
    public Transform wallChekLeft2;

    public Transform keyFollowPoint;

    public Key followingKey;

    public bool onGround;
    public bool onWall;
    private bool hasDashed;
    
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    public int direction;
    private float dashCD = 0.5f;
    private float NextDash;


    
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
    }


    void Update()
    {

        onGround = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        onWall = Physics2D.OverlapArea(wallCheckRight.position, wallCheckRight2.position) ||
                 Physics2D.OverlapArea(wallCheckLeft.position, wallChekLeft2.position);

        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.velocity = Vector2.up * jumpVelocity;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, 0);

        Walk(dir);

        if (onWall && !onGround && !Input.GetButtonDown("Jump"))
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -slideSpeed, float.MaxValue));

        
        if (direction == 0)
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
        else
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

        if (onGround)
            hasDashed = false;


    }


    private void Walk(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
    }

}

   
