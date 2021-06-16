using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TP_cheat_code : MonoBehaviour
{
    private bool NeverBeenZ2;
    private bool NeverBeenZ3;
    private bool NeverBeenZ4;
    
    
    // Start is called before the first frame update
    void Start()
    {
        NeverBeenZ2 = true;
        NeverBeenZ3 = true;
        NeverBeenZ4 = true;
    }

    // Update is called once per frame
    void Update()
    {
        //tp zone 2
        if (Input.GetKeyDown(KeyCode.Keypad2) && NeverBeenZ2)
        {
            Destroy(GameObject.FindGameObjectWithTag("PlantsTP"));
            
            new WaitForSeconds(0.3f);
            PhotonNetwork.LoadLevel(15);
                    
        }
        
        //tp zone 3
        if (Input.GetKeyDown(KeyCode.Keypad3) && NeverBeenZ3)
        {
            Destroy(GameObject.FindGameObjectWithTag("PlantsTP"));
            
            new WaitForSeconds(0.3f);
            PhotonNetwork.LoadLevel(30);
                    
        }
        
        //tp zone 4
        if (Input.GetKeyDown(KeyCode.Keypad4) && NeverBeenZ4)
        {
            Destroy(GameObject.FindGameObjectWithTag("PlantsTP"));
            
            new WaitForSeconds(0.3f);
            PhotonNetwork.LoadLevel(45);
                    
        }
        
        //tp salle défi
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            new WaitForSeconds(0.3f);
            PhotonNetwork.LoadLevel(64);
            
        }
        
        //tp last_scene
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            Destroy(GameObject.FindGameObjectWithTag("PlantsTP"));
            new WaitForSeconds(0.3f);
            PhotonNetwork.LoadLevel(60);
            
        }
        
    }

    void UpdateBool()
    {
        if (15 <= SceneManager.GetActiveScene().buildIndex && SceneManager.GetActiveScene().buildIndex < 30)
            NeverBeenZ2 = false;
        
        if (30 <= SceneManager.GetActiveScene().buildIndex && SceneManager.GetActiveScene().buildIndex < 45)
            NeverBeenZ3 = false;
        
        if (45 <= SceneManager.GetActiveScene().buildIndex && SceneManager.GetActiveScene().buildIndex < 59)
            NeverBeenZ4 = false;

    }
}
