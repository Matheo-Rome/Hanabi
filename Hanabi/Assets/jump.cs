using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
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
    
    public  bool onGround;
    public bool onWall;
    public bool sliding;




    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    

    void Update()
    {
        
        onGround = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        onWall = Physics2D.OverlapArea(wallCheckRight.position, wallCheckRight2.position) ||
                 Physics2D.OverlapArea(wallCheckLeft.position,wallChekLeft2.position);
        
        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.velocity = Vector2.up*jumpVelocity;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, 0);

        Walk(dir);

        if (onWall && !onGround && !Input.GetButtonDown("Jump"))
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -slideSpeed,float.MaxValue));

    }
    
    
    private void Walk(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
    }
}
