using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStressSolo : MonoBehaviourPunCallbacks
{
    List<int> storyScenes = new List<int>{13, 14, 27, 28, 29, 42, 43, 44, 57, 58, 59, 60};
    List<int> fireScenes = new List<int>{12, 26, 41, 56};
    public int minStress = 0;
    public int currentStress;
    public int maxStress = 200;
    public int reductiondestress = 0;
    public Text Pretzelcompteur;
    public float nextStress = 2f;
    public float StressCD;
    
    public bool canGainStress;
    public bool hasChangedRoom;
    public int previousRoom;
    public bool isTouchingFire;

    public StressBar stressBar;
    public float firecampValue = 0.6f;
    
    public static PlayerStressSolo instance;
    
    private void Awake()
    {
        // Il faut qu'il n'y ai qu'un seul et unique inventaire
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de player health dans la scène");
            return;
        }
        
        instance = this;
    }
    void Start()
    {
        currentStress = minStress;
        stressBar.SetMinStress(minStress);
    }
    
    void Update()
    {
        //checks if the player can gain stress based on the room he's in
        canGainStress = CanStress();

        //checks if the room has changed
        if (SceneManager.GetActiveScene().buildIndex != previousRoom)
        {
            hasChangedRoom = true;
            previousRoom = SceneManager.GetActiveScene().buildIndex;
        }
        
        //if the room just changed and you are near a fire place then we update the stress accordingly
        if (hasChangedRoom && isTouchingFire)
        {
            float reduc = GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>().AmeliorationFeuDeCamps;
            currentStress = (int) ((float) currentStress * reduc);
            gameObject.GetComponent<PlayerMovementSolo>().otherplayer.GetComponent<PlayerStressSolo>().currentStress = currentStress;
            hasChangedRoom = false;
        }
        List<Items> content = new List<Items>();;
        foreach (var objet in gameObject.transform.parent.gameObject.GetComponentInChildren<InventairePassif>().content)
        {
            reductiondestress += objet.StressRemoved;
            StressCD += objet.StressIntervalle;
            Pretzelcompteur.text = reductiondestress.ToString();
            if (objet.StressRemoved == 0 && objet.StressIntervalle == 0)
            {
                content.Add(objet);
            }
        }
        gameObject.transform.parent.gameObject.GetComponentInChildren<InventairePassif>().content = content;    
        
        //updates the stress each time the cd is up
        if (Time.time > StressCD && canGainStress)
        {
           TakeStress(1);
           StressCD = Time.time + nextStress;
        }
        
        

        //cheat code UwU omg so cool
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeStress(20);
            //gameObject.GetComponent<PlayerMovementSolo>().otherplayer.GetComponent<PlayerStressSolo>().TakeStress(20);
        }
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            HealStressplayer(20);
           // gameObject.GetComponent<PlayerMovementSolo>().otherplayer.GetComponent<PlayerStressSolo>().HealStressplayer(20);
           
        }
        
        if (currentStress == maxStress)
        {
            HealStressplayer(reductiondestress);
            gameObject.GetComponent<PlayerMovementSolo>().otherplayer.GetComponent<PlayerStressSolo>().HealStressplayer(reductiondestress);
            reductiondestress = 0;
            Pretzelcompteur.text = reductiondestress.ToString();
        }
        

    }

    public bool CanStress()
    {
        return !fireScenes.Contains(SceneManager.GetActiveScene().buildIndex) && !storyScenes.Contains(SceneManager.GetActiveScene().buildIndex);
    }

    public void TakeStress(int addstress)
    {
        currentStress += addstress;
        if (currentStress > maxStress)
        {
            currentStress = maxStress;
        }
        stressBar.SetStress(currentStress);
    }

    public void HealStressplayer(int amount)
    {
        if (currentStress - amount<0)
            currentStress = minStress;
        else
            currentStress = currentStress - amount;
        
        stressBar.SetStress(currentStress);
    }
    

}
