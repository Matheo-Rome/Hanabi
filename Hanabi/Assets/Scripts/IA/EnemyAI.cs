using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Pathfinding;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    //public Transform FantomeGFX;
    public SpriteRenderer SpriteRenderer;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    private Path Path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker _seeker;
    private Rigidbody2D rb;
    private List<Vector3> _positions = new List<Vector3>(); //Cette liste va me permettre de stocker la position du joueur 
    private int index = 0;//indexe de la liste au dessus
    
    void Start()
    {
        _seeker = GetComponent<Seeker>(); // Ces deux éléments sont du code pour la création d'un path
        rb = GetComponent<Rigidbody2D>();

        _positions.Add(target.position);// ici j'add la position du joueur à ma liste 

        
        _seeker.StartPath(rb.position, _positions[index]/*target.position*/, OnPathComplete); // ici je créé un premier path avec la première position de mon joueur
        index++;
        InvokeRepeating("AddPath",0f,.09f);//ici j'add la position du joueur tous les 0.09 secondes afin de faire une sorte de "positions précédentes"
        InvokeRepeating("UpdatePath",0f,.14f);//ici je créé le nouveau path que mon fantome devra suivre avec la position du joueur que j'ai stocker dans la liste
    }// cela me permet donc de créé un path a partir de la position du joueur précédente et ainsi de créé l'effet de suivre les mouvements du joueur alors qu'il ne fait que suivre les positions précédentes
     //La méthode updatepath se fera tous les 0.14 secondes afin qu'elle utilise des positions précédentes du joueur.
    void AddPath()
    {
        _positions.Add(target.position);
    }
    void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(rb.position,_positions[index]/*target.position*/, OnPathComplete);
            index++;//startpath possède comme paramètre la position du fontome et la position ou il doit aller
        }
    }
    
    // le reste du code était essentiel à la création du path (repris d'un code internet) j'ai simplement modifier le début pour modifier le path et avoir l'impression qu'il suit les positions précédente du joueur.
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            Path = p;
            currentWaypoint = 0;
        }
    }
    

    void FixedUpdate()
    {
        if(Path == null)
            return;
        if (currentWaypoint >= Path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2) Path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, Path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rb.velocity.x >= -0.01f)
        {
            //FantomeGFX.localScale = new Vector3(-1f, 1f, 1f);
            SpriteRenderer.flipX = false;   
        }
        else if (rb.velocity.x<= -0.01f)
        {
            //FantomeGFX.localScale = new Vector3(1f, 1f, 1f);
            SpriteRenderer.flipX = true;
        }
    }
}
