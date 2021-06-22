using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class InstantiateFantome : MonoBehaviour
{
    public bool Oneinstance = false;
    
    public GameObject Fantome;
    public GameObject instance;
    public float cooldown = 15f;
    public GameObject instanceFantome;

    void Update()
    {
        if (PhotonNetwork.IsConnected)
        {
            PlayerStress playerStress = gameObject.transform.parent.gameObject.GetComponentInChildren<PlayerStress>();
            if (playerStress.currentStress == playerStress.maxStress && !Oneinstance) //j'instancie le fantome la première fois que je suis à 200, le booléen me permet de ne pas instancier 36 fantome toutes les secondes mais uniquement la première fois que je passe à 200
            {
                instance = Instantiate(Fantome, transform.position, Quaternion.identity, instanceFantome.transform);
                Oneinstance = true;
            }

            if (playerStress.currentStress <= playerStress.maxStress -11 && Oneinstance) // cette méthode me permet de détruire l'instance du fantome créé lorsque notre stress passe en dessous de 189
            {
                Oneinstance = false;
                GameObject parent = instanceFantome.transform.parent.gameObject;
                Destroy(instanceFantome);
                instanceFantome = new GameObject("instanceFantome");
                instanceFantome.transform.parent = parent.transform;
                cooldown = 15f;
            }
        }
        else
        {
            PlayerStressSolo playerStressSolo = gameObject.transform.parent.gameObject.GetComponentInChildren<PlayerStressSolo>();
            if (playerStressSolo.currentStress == playerStressSolo.maxStress && !Oneinstance) //j'instancie le fantome la première fois que je suis à 200, le booléen me permet de ne pas instancier 36 fantome toutes les secondes mais uniquement la première fois que je passe à 200
            {
                instance = Instantiate(Fantome, transform.position, Quaternion.identity, instanceFantome.transform);
                Oneinstance = true;
            }

            if (playerStressSolo.currentStress  <= playerStressSolo.maxStress-11 && Oneinstance) // cette méthode me permet de détruire l'instance du fantome créé lorsque notre stress passe en dessous de 189
            {
                Oneinstance = false;
                GameObject parent = instanceFantome.transform.parent.gameObject;
                Destroy(instanceFantome);
                instanceFantome = new GameObject("instanceFantome");
                instanceFantome.transform.parent = parent.transform;
                cooldown = 15f;
            } 
        }

        if (Oneinstance)
        {
            if(cooldown > 0)
                cooldown -= Time.deltaTime;
            else
            {
                Oneinstance = false;
                cooldown = 15f;
            }
        }
    }
}