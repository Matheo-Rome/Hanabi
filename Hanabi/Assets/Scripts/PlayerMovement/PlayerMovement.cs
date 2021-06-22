using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
//using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Internal.VR;
using UnityEngine.SceneManagement;
using Object = System.Object;


public class PlayerMovement : MonoBehaviourPun
{

    public static PlayerMovement instance;
    private PlayerStress Stress;

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

    [SerializeField] private bool onGround;
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
    private bool go = false;

    [SerializeField] private SaveData _saveData;



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

         PhotonNetwork.AutomaticallySyncScene = true;
         Stress = GetComponent<PlayerStress>();

         if (photonView.IsMine) //Active la caméra du joueur est éteint celle de l'autre joueur
         {
             playerCamera.SetActive(true);
             canvas.SetActive(true);
             canvasPause.SetActive(true);
         }
         else
         {
             playerCamera.SetActive(false);
             canvas.SetActive(false);
             canvasPause.SetActive(false);
         }
     }


     void FixedUpdate()
     {

         if (!founded)
         {
             GameObject[] allPlayer = GameObject.FindGameObjectsWithTag("Player");
             Debug.Log(allPlayer.Length);
             foreach (var Player in allPlayer)
             {
                 if (Player != player)
                 {
                     otherplayer = Player;
                     founded = true;
                 }
             }
         }

         scenecheck = SceneManager.GetActiveScene().buildIndex;
         if (scene != scenecheck)
         {
              if (PhotonNetwork.IsMasterClient && scenecheck != 68)
                  photonView.RPC("RPC_TP",RpcTarget.Others,scenecheck);
             Reposition();
         }

         scene = scenecheck;

         if (scene == 68)
         {
             if (PhotonNetwork.IsMasterClient)
             {
                 gameObject.transform.parent.gameObject.GetComponent<SaveData>().Save();
                 photonView.RPC("RPC_TPF", RpcTarget.Others, 68);
             }
             else
                 photonView.RPC("RPC_TPF", RpcTarget.Others, 68);
             gameObject.GetComponent<PlayerStress>().HealStressplayer(100000);
             otherplayer.GetComponent<PlayerStress>().HealStressplayer(100000);
             StartCoroutine(Disconnect());
             List<DDOL> toDestroy = GameObject.FindObjectsOfType<DDOL>().ToList();
             toDestroy.ForEach(DDOL => Destroy(DDOL.gameObject));
         }


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
             DashLight();
         }


         //Reset du Dash quand le personnage touche le sol
         if (onGround && Time.time >= NextDash)
             hasDashed = false;

         if (rb.velocity.y < 0)
             isBouncydashing = false;

         if (onGround)
             hasFallen = false;
         
         if(Input.GetButtonDown("Respawn") && onGround)
             Reposition();

         if (go)
         {
             SceneManager.LoadScene(68);
         }
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
            Debug.Log("Exit " + isInsideEx);
            Debug.Log("Enter " + isInsideEn);
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
       /* if (other.CompareTag("Flower"))
        {
            Reposition();
            
        }*/
        if (other.CompareTag("Respawn"))
        {
            Reposition();
           
            //Reduces the amount of stress gained when falling of you have the long fall boots item
            fallResistance = 0;
            foreach (var objet in InventairePassif.instance.content)
            {
                fallResistance += objet.StressLoss;
            }
            //PlayerStress.instance.TakeStress(10 - fallResistance);
            otherplayer.GetComponent<PlayerStress>().photonView.RPC("RPC_TakeStress", RpcTarget.All, 10 - fallResistance);//TakeStress(10 - fallResistance);
            photonView.RPC("RPC_TakeStress", RpcTarget.All, 10 - fallResistance);
            hasFallen = true;
        }
        
        else if (other.CompareTag("IA"))
        {
            Reposition();
            SceneManager.LoadScene(68);
            photonView.RPC("RPC_HealStress", RpcTarget.All, 90000);
            otherplayer.GetComponent<PlayerStress>().photonView.RPC("RPC_HealStress", RpcTarget.All, 90000);
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

    [PunRPC]
    public void RPC_TP(int index)
    {
        if(/*PhotonNetwork.IsMasterClient &&*/ SceneManager.GetActiveScene().buildIndex != index)
            SceneManager.LoadScene(index);
    }
    
    [PunRPC]
    public void RPC_TPF(int index)
    {
        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(index);
        else
            PhotonNetwork.LoadLevel(index);
    }

    [PunRPC]
    public void RPC_Destroy()
    {
        List<DDOL> toDestroy = GameObject.FindObjectsOfType<DDOL>().ToList();
        toDestroy.ForEach(x => Destroy(x.gameObject));
        PhotonNetwork.Disconnect();
    }

    [PunRPC]
    public void RPC_Disconnect()
    {
        StartCoroutine(Disconnect());
    }

    IEnumerator Disconnect()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        {
            Debug.Log(PhotonNetwork.IsConnected);
            yield return null;
        }

        go = true;
    }
    
 }
