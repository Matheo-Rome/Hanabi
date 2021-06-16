using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateFantome : MonoBehaviour
{
    public bool Oneinstance = false;
    
    public GameObject Fantome;
    public GameObject instance;
    public float cooldown = 15f;
    public GameObject instanceFantome;
    void Start()
    {
        
    }
    void Update()
    {
        if (PlayerStress.instance.currentStress == 200 && !Oneinstance)//j'instancie le fantome la première fois que je suis à 200, le booléen me permet de ne pas instancier 36 fantome toutes les secondes mais uniquement la première fois que je passe à 200
        {
            instance = Instantiate(Fantome, transform.position, Quaternion.identity, instanceFantome.transform);
            Oneinstance = true;
        }
        
        if (PlayerStress.instance.currentStress <= 189 && Oneinstance)// cette méthode me permet de détruire l'instance du fantome créé lorsque notre stress passe en dessous de 189
        {
            Oneinstance = false;
            Destroy(instanceFantome.transform.GetChild(0).parent.gameObject);
            instanceFantome = new GameObject();
            cooldown = 15f;
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