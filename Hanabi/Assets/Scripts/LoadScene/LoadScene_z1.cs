using System;
using System.Collections.Generic;
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
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //si c'est la salle histoire renvoie vers la room_choice
            if (SceneManager.GetActiveScene().buildIndex == history)
            {
                Destroy(gameObject);
                new WaitForSeconds(0.3f);
                SceneManager.LoadScene(14); 
                
            }
            
            else
            {
               new WaitForSeconds(0.3f);
               SceneManager.LoadScene(NextIndex()); 
            }
            
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
