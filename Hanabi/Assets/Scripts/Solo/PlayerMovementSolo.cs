using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
//using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Internal.VR;
using UnityEngine.SceneManagement;
using Object = System.Object;


public class PlayerMovementSolo : MonoBehaviourPun
{

    public static PlayerMovementSolo instance;
    public static PlayerMovementSolo otherinstance;
    private PlayerStressSolo Stress;

    //Mouvement
    public float jumpVelocity;
    public float speed;
    [SerializeField] private float slideSpeed;


    private Rigidbody2D rb;
    private Collider2D rc;

    //Checker de position
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private Transform wallCheckRight;
    [SerializeField] private Transform wallCheckRight2;
    [SerializeField] private Transform wallCheckLeft;
    [SerializeField] private Transform wallCheckLeft2;

    public Transform keyFollowPoint;
    [SerializeField] private Transform SpawnPoint;

    public Key followingKey;

    public bool onGround;
    [SerializeField] private bool onWall;
    public bool hasDashed;
    public bool hasFallen;
    public int fallResistance;

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject canvasPause;

    //Dash
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    public int direction;
    private float dashCD = 0.5f;
    private float NextDash;
    private bool isDashing;
    private Vector2 dir;
    private Vector3 stocktele;
    private Vector3 stockSpawn;
    private Vector3 stockCam;
    private int isInsideEn = 0;
    private int isInsideEx = 0;

    [SerializeField] private bool ClassiqueDash;
    [SerializeField] private bool BouncyDash;
    [SerializeField] private bool isBouncydashing;
    [SerializeField] private bool LightDash;

    public bool itemJump;
    public bool itemTp;
    [SerializeField] private bool isItemDashing;

    [SerializeField] private GameObject playerCamera;
    [SerializeField] private Transform CameraSpawn;

    private Animator animator;
    private SpriteRenderer spriteRenderer;


    [SerializeField] private GameObject player;
    private PolygonCollider2D Polycollider;
    private BoxCollider2D Boxcollider;

    private int scene = 1;
    private int scenecheck = 1;

    public GameObject otherplayer;
    public bool founded = false;

    [SerializeField] private SaveData _saveData;

    public bool playing;
    public bool J1;

    
    private void Awake()
     {
         // Il faut qu'il n'y ai qu'un seul et unique inventaire
         if (instance != null)
         {
             Debug.LogWarning("il y a plus d'une instance de mouvement dans la scène");
             return;
         }
         
     }

     private void Start()
     {
         /*if(PhotonNetwork.IsMasterClient)
         _saveData.Load();*/
         SpawnPoint.position = transform.position;
         CameraSpawn.position = playerCamera.transform.position;
         rb = GetComponent<Rigidbody2D>();
         dashTime = startDashTime;
         animator = GetComponent<Animator>();
         spriteRenderer = GetComponent<SpriteRenderer>();
         Polycollider = GetComponent<PolygonCollider2D>();
         if (LightDash)
         {
             Boxcollider = GetComponent<BoxCollider2D>();
             Boxcollider.enabled = false;
             Boxcollider.isTrigger = true;
         }
         Stress = GetComponent<PlayerStressSolo>();
         if (J1)
         {
             instance = this;
             otherinstance = otherplayer.GetComponent<PlayerMovementSolo>();
         }
         else
         {
             otherinstance = this;
             instance = otherinstance.GetComponent<PlayerMovementSolo>();
         }
     }

     private void Update()
     {
         if (Input.GetButtonDown("Switch") && onGround)
         {
             playing = false;
             otherplayer.GetComponent<PlayerMovementSolo>().playing = true;
             otherplayer.GetComponent<PlayerMovementSolo>().GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
             otherplayer.GetComponent<PlayerMovementSolo>().GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
         }
         
         if(Input.GetButtonDown("Respawn") && onGround)
             Reposition();
     }


     void FixedUpdate()
     {
        
         if (!founded)
         {
             GameObject[] allPlayer = new GameObject[]{GameObject.FindGameObjectWithTag("Player1"),GameObject.FindGameObjectWithTag("Player2")};
             foreach (var Player in allPlayer)
             {
                 if (Player != player)
                 {
                     otherplayer = Player;
                     founded = true;
                 }
             }
         }
         if (playing)
         {
             playerCamera.SetActive(true);
             canvas.SetActive(true);
             canvasPause.SetActive(true);
             otherplayer.GetComponent<PlayerMovementSolo>().canvas.SetActive(false);
             otherplayer.GetComponent<PlayerMovementSolo>().canvasPause.SetActive(false);
             otherplayer.GetComponent<PlayerMovementSolo>().enabled = false;
             otherplayer.GetComponent<PlayerMovementSolo>().playerCamera.SetActive(false);
         }
         else
         {
             otherplayer.GetComponent<PlayerMovementSolo>().enabled = true;
             gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
             //canvas.SetActive(false);
             //canvasPause.SetActive(false);
             //playerCamera.SetActive(false);
             this.enabled = false;
         }

         if (playing)
         {

             if (J1)
             {
                 GameObject[] IAs = GameObject.FindGameObjectsWithTag("IA");
                 GameObject[] IA2s = GameObject.FindGameObjectsWithTag("IA2");
                 foreach (var ia in IAs)
                 {
                     var rb2 = ia.GetComponent<Rigidbody2D>();
                     rb2.constraints = RigidbodyConstraints2D.None;
                     rb2.constraints = RigidbodyConstraints2D.FreezeRotation;
                 }

                 foreach (var ia2 in IA2s)
                 {
                     ia2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                 }

             }
             else
             {
                 GameObject[] IAs = GameObject.FindGameObjectsWithTag("IA");
                 GameObject[] IA2s = GameObject.FindGameObjectsWithTag("IA2");
                 foreach (var ia in IAs)
                 {
                     ia.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                 }

                 foreach (var ia2 in IA2s)
                 {
                     var rb2 = ia2.GetComponent<Rigidbody2D>();
                     rb2.constraints = RigidbodyConstraints2D.None;
                     rb2.constraints = RigidbodyConstraints2D.FreezeRotation;
                 }
             }
         }


         scenecheck = SceneManager.GetActiveScene().buildIndex;
         if (scene != scenecheck)
         {
             Reposition();
             otherplayer.GetComponent<PlayerMovementSolo>().enabled = true;
             otherplayer.GetComponent<PlayerMovementSolo>().Reposition();
             otherplayer.GetComponent<PlayerMovementSolo>().enabled = false;
         }

         otherplayer.GetComponent<PlayerMovementSolo>().scene = scenecheck;
         scene = scenecheck;


         //Information sur la direction du déplacement selon les touches appuyé
         float x = Input.GetAxis("Horizontal");
         float y = Input.GetAxis("Vertical");
         float xRaw = Input.GetAxisRaw("Horizontal");
         float yRaw = Input.GetAxisRaw("Vertical");
         Vector2 dir = new Vector2(x, 0);

         //Vérifie la position du personnage par rapport au sol et aux murs.
         onGround =Physics2D.OverlapArea(groundCheckLeft.position, groundCheckLeft.position);
         onWall = Physics2D.OverlapArea(wallCheckRight.position, wallCheckRight2.position) ||
                  Physics2D.OverlapArea(wallCheckLeft.position, wallCheckLeft2.position);

         //Jump
         if (Input.GetButton("Jump") && onGround || itemJump)
         {
             Jump();
         }

         if (!isDashing)
             Walk(dir);

         animator.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
         animator.SetFloat("SpeedY", rb.velocity.y);
         animator.SetBool("GroundColl", onGround);
         animator.SetBool("WallColl", onWall);
         if (BouncyDash)
             animator.SetInteger("Direction", direction);
         else if (ClassiqueDash)
             animator.SetInteger("Direction", direction);
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

         //Dash Light or The World
         if (direction == 0 && (LightDash || itemTp))
         {
             if (itemTp)
             {
                 itemTp = false;
                 isItemDashing = true;
             }

             DashdirLight();
         }
         else if (LightDash)
             DashLight();

         if (isItemDashing)
         {
             DashLight();
         }


         //Reset du Dash quand le personnage touche le sol
         if (onGround && Time.time >= NextDash)
             hasDashed = false;

         if (rb.velocity.y < 0)
             isBouncydashing = false;

         if (onGround)
             hasFallen = false;
         
         
     }


     //Fait sauter
    private void Jump()
    {
        rb.velocity = Vector2.up * jumpVelocity;
        itemJump = false;
    }

    //Permet de marcher
    private void Walk(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
    }

    //Ralentit la chute lorsque l'on est contre le mur.
    private void Slide()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -slideSpeed, float.MaxValue));
    }

    //Permet de sélectionner la direction du dash
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
            if(direction != 0)
                isDashing = true;
        }   
    }
    //Effectue le dash : dans un premier temps propulse le joueur dans une direction donné et à la fin fait sortir le joueur du mode dash
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
    //Permet de sélectionner la direction du dash
    private void DashdirBouncy()
    {
        if (Input.GetButton("Dash") && !hasDashed && Time.time >= NextDash)
        {
            rb.velocity = Vector2.zero;

            if (Input.GetAxis("Horizontal") < 0) //gauche
            {
                direction = 1;
                isDashing = true;
            }
            else if (Input.GetAxis("Horizontal") > 0) //droite
            {
                direction = 2;
                isDashing = true;
            }

        }   
    }
    //Effectue le dash : dans un premier temps propulse le joueur dans une direction donné et à la fin fait sortir le joueur du mode dash
    //Déclenche l'impulsion vers le haut dans le cas ou le joueur est contre un mur à la fin du dash
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
    
    //Permet de sélectionner la direction du dash
    private void DashdirLight()
    {
        if (Input.GetButton("Dash") && !hasDashed && Time.time >= NextDash || isItemDashing)
        {
            rb.velocity = Vector2.zero;
            if (Input.GetAxis("Vertical") > 0) //haut
            {
                direction = 3;
                dir = Vector2.up;
                spriteRenderer.enabled = false;
                Boxcollider.enabled = true;
                Polycollider.enabled = false;
                isDashing = true;
            }

            else if (Input.GetAxis("Horizontal") < 0) //gauche
            {
                direction = 1;
                dir = Vector2.left;
                spriteRenderer.enabled = false;
                Boxcollider.enabled = true;
                Polycollider.enabled = false;
                isDashing = true;
            }
            else if (Input.GetAxis("Horizontal") > 0) //droite
            {
                direction = 2;
                dir = Vector2.right;
                spriteRenderer.enabled = false;
                Boxcollider.enabled = true;
                Polycollider.enabled = false;
                isDashing = true;
            }
            stocktele = player.transform.position;
            stockSpawn = SpawnPoint.position;
            stockCam = playerCamera.transform.position;
        }   
    }
    //Effectue le dash : dans un premier temps propulse le joueur dans une direction donné et à la fin fait sortir le joueur du mode dash
    //Pendant le dash le joueur est invisible est possède un collider isTrigger qui compte le nombre de collision.
    //En cas de nombre impaire renvoie le joueur à sa position d'origine
    private void DashLight()
    {
        if (dashTime <= 0 || isItemDashing)
        {
            direction = 0;
            dashTime = startDashTime;
            rb.velocity = Vector2.zero;
            if (isInsideEn%2 != 0 || isInsideEx%2 != 0)
            {
                player.transform.position = stocktele;
                SpawnPoint.position = stockSpawn;
                playerCamera.transform.position = stockCam;
                hasDashed = false;
            }
            else
                rb.velocity += Vector2.up*5;
            isInsideEn = 0;
            isInsideEx = 0;
            isDashing = false;
            spriteRenderer.enabled = true;
            Boxcollider.enabled = false;
            Polycollider.enabled = true;
        }
        else
        {
            isItemDashing = false;
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
    
    // Test collider fin de niveau/tomber dans un trou/ collision avec l'ia de la mort qui tue/ collision lors d'une teleportation
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Respawn"))
        {
            Reposition();
           
            //Reduces the amount of stress gained when falling of you have the long fall boots item
            fallResistance = 0;
            foreach (var objet in InventairePassif.instance.content)
            {
                fallResistance += objet.StressLoss;
            }
            PlayerStressSolo.instance.TakeStress(10 - fallResistance);
            otherplayer.GetComponent<PlayerStressSolo>().TakeStress(10 - fallResistance);
            hasFallen = true;
        }
        
        else if (other.CompareTag("IAT"))
        {
            Reposition();
            player.GetComponent<PlayerStressSolo>().currentStress = 0;
            otherplayer.GetComponent<PlayerStressSolo>().currentStress = 0;
            gameObject.transform.parent.gameObject.GetComponent<SaveData>().Save();
            List<DDOL> toDestroy = GameObject.FindObjectsOfType<DDOL>().ToList();
            toDestroy.ForEach(x => Destroy(x.gameObject));
            SceneManager.LoadScene(68);
        }
        else
        {
            if (!(other.CompareTag("Door")||other.CompareTag("Button")))
            {
                if (other.CompareTag("Planche"))
                {
                    isInsideEn++;
                }
                isInsideEn++;
            }
        }
    }

    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Planche"))
        {
            isInsideEx++;
        }
        isInsideEx++;
    }

    //Ralentir la déplacement sur x quand on est en l'air
    private void SlowAir(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed / 1.4f, rb.velocity.y);
    }
    
    //Orient le personnage dans la bonne direction
    private void Flip(float _velocity)
    {
        
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
            
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
        
    }
    
    public void Reposition()
    {
        playerCamera.transform.position = new Vector3(CameraSpawn.position.x, CameraSpawn.position.y,CameraSpawn.position.z);
        gameObject.transform.position = new Vector3(SpawnPoint.position.x,SpawnPoint.position.y,SpawnPoint.position.z);
        CameraSpawn.position = new Vector3(playerCamera.transform.position.x,playerCamera.transform.position.y,playerCamera.transform.position.z);
        SpawnPoint.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }
 }

