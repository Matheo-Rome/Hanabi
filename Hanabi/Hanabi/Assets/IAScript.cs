using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAScript : MonoBehaviour
{
    public float speed;
    public  List<Transform> waypoints = new List<Transform>();
    
    private Transform target;
    public int index = 0;
    private Transform PrecPose;
    

    void Start()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>(); 
        waypoints.Add(player.transform);
        //j'ajoute la position du joueur actuelle à ma liste de waypoints à atteindre
        PrecPose = waypoints[index]; 
        //Je prend la précédente position et la garde en mémoire pour la suivre tant que je n'y suis pas arrivé
        transform.position = Vector3.Lerp(transform.position, PrecPose.position, speed * Time.deltaTime);
        // permet d'aller du pts A au pts B avec une vitesse T
        Destroy(GetComponent<BoxCollider2D>());
    }

    void Update()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        waypoints.Add(player.transform);
        // Idem qu'avant
        if (Vector3.Distance(transform.position, PrecPose.position)<0.5f)
        {
            //waypoints.Remove(PrecPose);
            index += 1;
            PrecPose = waypoints[index]; 
            // Une fois que je suis arrivé à ma position, je la remove puis je modifie ma précédente position pour prendre la suivante.
        }
        transform.position = Vector3.Lerp(transform.position, PrecPose.position, speed * Time.deltaTime);
        // je suis toujours la précédente position
    }
}