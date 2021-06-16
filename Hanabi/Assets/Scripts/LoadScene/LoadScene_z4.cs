﻿using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;



public class LoadScene_z4 : MonoBehaviour
{
    
       List<int> scenes = new List<int>{46,47,48,49,50,51,52,53,54};
       List<int> history = new List<int>{57,58,59};
       private int shop = 55;
       private int firecamp = 56;
       private int i = 0;
       private int r;
       private int next;
       
       public PlantPlayer2 flower;
       
       private void OnTriggerEnter2D(Collider2D collision)
       {
           if (flower.IsTrigger)
           {
               tag = "Flower";
   
               if (collision.CompareTag("Player") || collision.CompareTag("Player1") || collision.CompareTag("Player2"))
               {
                   //salle défi renvoie vers last_scene
                   if (SceneManager.GetActiveScene().buildIndex == 64)
                   {
                       Destroy(GameObject.FindGameObjectWithTag("PlantsTP"));
                       new WaitForSeconds(0.3f);
                       PhotonNetwork.LoadLevel(60);
                       //SceneManager.LoadScene(14);
                   }

                   else
                   {
                       new WaitForSeconds(0.3f);
                       PhotonNetwork.LoadLevel(NextIndex());
                       //SceneManager.LoadScene(NextIndex());
                   }
               }
           }
       }
   
       private int NextIndex()
       {
           if (i == 13)
               return 64;
   
           //n'a pas encore visité le firecamp au bout de 7 salles
           if (i == 6 && firecamp != 0)
           {
               firecamp = 0;
               i++;
               return 56;
           }
   
           //n'a pas encore visité le shop au bout de 8 salles
           if (i == 7 && shop != 0)
           {
               shop = 0;
               i++;
               return 55;
           }
           
           //pas de firecamp ni de shop pour les 3 premières salles
           if (i < 4)
           {
               //si il y a encore des salles histoires
               if (history.Count > 0)
               {
                   // 1/5 chance d'avoir une salle histoire
                   r = Random.Range(0, 5);
                   if (r == 1)
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
           
           //au bout de 10 salles,  1/5 chance d'aller à la zone suivante
           if (i > 9)
           {
               r = Random.Range(0, 5);
               if (r == 1)
                   return 64;
           }
               
           
           //au dessus de 4 salles n'importe quelle salle peut tomber
           
           if (history.Count > 0)
           {
               // 1/5 chance d'avoir une salle histoire
               r = Random.Range(0, 5);
               if (r == 1)
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
               if (r == 1)
               {
                   shop = 0;
                   i++;
                   return 55;
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
                   return 56;
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