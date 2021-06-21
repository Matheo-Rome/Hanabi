using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LoadScene_z1 : MonoBehaviour
{
    List<int> scenes = new List<int>{2,3,4,5,6,7,8,9,10};
    private int shop = 11;
    private int firecamp = 12;
    private int history = 13;
    private int i = 0;
    private int r;
    private int next;

    public PlantPlayer2 flower;

    private void Start()
    {
        if(PhotonNetwork.IsConnected) 
            PhotonNetwork.AutomaticallySyncScene = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (flower.IsTrigger)
        {
            tag = "Flower";
            if (collision.CompareTag("Player")|| collision.CompareTag("Player1") || collision.CompareTag("Player2"))
            {
                flower.IsTrigger = false;
                //si c'est la salle histoire renvoie vers la salle défi
                if (SceneManager.GetActiveScene().buildIndex == history)
                {
                    new WaitForSeconds(0.3f);
                    PhotonNetwork.LoadLevel(61);
                    
                }
                
                //salle défi renvoie vers choice_room
                else if (SceneManager.GetActiveScene().buildIndex == 61)
                {
                    
                    Destroy(GameObject.FindGameObjectWithTag("PlantsTP"));
                    new WaitForSeconds(0.3f);
                    PhotonNetwork.LoadLevel(14);
                    
                }
                
                else 
                {
                    new WaitForSeconds(0.3f);
                    int n = NextIndex();
                    Debug.LogWarning(n);
                    PhotonNetwork.LoadLevel(n);
                    //SceneManager.LoadScene(NextIndex());
                }
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        new WaitForSeconds(0.5f);
        
        if (other.CompareTag("Player") || other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            tag = "Untagged";
        }
    }

    private int NextIndex()
    {
        if (i == 11)
            return history;

        //n'a pas encore visité le firecamp au bout de 7 salles
        if (i == 6 && firecamp != 0)
        {
            firecamp = 0;
            i++;
            return 12;
        }

        //n'a pas encore visité le shop au bout de 8 salles
        if (i == 7 && shop != 0)
        {
            shop = 0;
            i++;
            return 11;
        }
        
        //pas de firecamp ni de shop pour les 3 premières salles
        if (i < 4)
        {
            //renvoie une salle puzzle
            r = Random.Range(0, scenes.Count);
            next = scenes[r];
            scenes.RemoveAt(r);
            i++;
            return next;
        }
        
        //au bout de 10 salles,  1/5 chance d'aller à la zone suivante
        if (i > 9)
        {
            r = Random.Range(0, 5);
            if (r == 1)
                return history;
        }
            
        
        //au dessus de 4 salles n'importe quelle salle peut tomber
        if (shop != 0)
        {
            // 1/5 chance d'avoir le shop
            r = Random.Range(0, 5);
            if (r == 1)
            {
                shop = 0;
                i++;
                return 11;
            }
        }

        if (firecamp != 0)
        {
            // 1/5 chance d'avoir le firecamp
            r = Random.Range(0, 5);
            if (r == 1)
            {
                firecamp = 0;
                i++; 
                return 12;
            }
        }
        
        //renvoie une salle puzzle
        r = Random.Range(0, scenes.Count);
        next = scenes[r];
        scenes.RemoveAt(r);
        i++;
        return next;
        
    }
}
