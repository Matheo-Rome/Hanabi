﻿using System;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LoadScene_z2 : MonoBehaviour
{
    
    List<int> scenes = new List<int>{16,17,18,19,20,21,22,23,24};
    List<int> history = new List<int>{27,28,29};
    private int shop = 25;
    private int firecamp = 26;
    private int total = 14;
    private int i = 0;
    private int r;
    private int next;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            new WaitForSeconds(0.3f);
            SceneManager.LoadScene(NextIndex());
        }
    }

    private int NextIndex()
    {
        if (i == 14)
            return 30;

        //n'a pas encore visité le firecamp au bout de 7 salles
        if (i == 6 && firecamp != 0)
        {
            firecamp = 0;
            i++;
            return 26;
        }

        //n'a pas encore visité le shop au bout de 8 salles
        if (i == 7 && shop != 0)
        {
            shop = 0;
            i++;
            return 25;
        }
        
        //pas de firecamp ni de shop pour les 3 premières salles
        if (i < 4)
        {
            //si il y a encore des salles histoires
            if (history.Count > 0)
            {
                // 1/5 chance d'avoir une salle histoire
                r = Random.Range(0, 5);
                if (r == 0)
                {
                    r = Random.Range(0, history.Count);
                    next = history[r];
                    history.RemoveAt(r);
                    i++;
                    return next;
                }
                
            }
            
            //renvoie une salle puzzle
            r = Random.Range(0, scenes.Count);
            next = scenes[r];
            scenes.RemoveAt(r);
            i++;
            return next;
        }
        
        //au bout de 10 salles,  1/10 chance d'aller à la zone suivante
        if (i > 9)
        {
            r = Random.Range(0, 10);
            if (r == 0)
                return 30;
        }
            
        
        //au dessus de 4 salles n'importe quelle salle peut tomber
        
        if (history.Count > 0)
        {
            // 1/5 chance d'avoir une salle histoire
            r = Random.Range(0, 5);
            if (r == 0)
            {
                r = Random.Range(0, history.Count);
                next = history[r];
                history.RemoveAt(r);
                i++;
                return next;
            }
            
        }

        if (shop != 0)
        {
            // 1/5 chance d'avoir le shop
            r = Random.Range(0, 5);
            if (r == 0)
            {
                shop = 0;
                i++;
                return 25;
            }
        }
        
        
        if (firecamp != 0)
        {
            
            // 1/5 chance d'avoir le firecamp
            r = Random.Range(0, 5);
            if (r == 0)
            {
                firecamp = 0;
                i++; 
                return 26;
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
