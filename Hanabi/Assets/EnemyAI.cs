using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Pathfinding;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    private Path Path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker _seeker;
    private Rigidbody2D rb;
    private List<Vector3> _positions = new List<Vector3>();
    private int index = 0;
    
    void Start()
    {
        _seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        
        
        _positions.Add(target.position);

        
        _seeker.StartPath(rb.position, _positions[index]/*target.position*/, OnPathComplete);
        index++;
        InvokeRepeating("AddPath",0f,.1f);
        InvokeRepeating("UpdatePath",0f,.12f);
    }

    void AddPath()
    {
        _positions.Add(target.position);
    }
    void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(rb.position,_positions[index]/*target.position*/, OnPathComplete);
            index++;
        }
    }
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
    }
}
