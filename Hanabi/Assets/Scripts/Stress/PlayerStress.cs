using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStress : MonoBehaviourPunCallbacks
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

    public StressBar stressBar;

    public static PlayerStress instance;
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
        
        //if the room just changed and it is a fire place room then we update the stress accordingly
        if (fireScenes.Contains(SceneManager.GetActiveScene().buildIndex) && hasChangedRoom)
        {
            currentStress = (int) (currentStress * 0.6f);
        }
        
        //updates the stress each time the cd is up
        if (Time.time > StressCD && canGainStress)
        {
           TakeStress(1);
           if (currentStress > 200)
           {
               currentStress = 200;
           }
           StressCD = Time.time + nextStress;
        }
        
        List<Items> content = new List<Items>();;
        foreach (var objet in InventairePassif.instance.content)
        {
            reductiondestress += objet.StressRemoved;
            Pretzelcompteur.text = reductiondestress.ToString();
            if (objet.StressRemoved == 0)
            {
                content.Add(objet);
            }
        }
        
        InventairePassif.instance.content = content;    

        //cheat code UwU omg so cool
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeStress(20);
            if (currentStress > 200)
            {
                currentStress = 200;
            }
            base.photonView.RPC("RPC_TakeStress", RpcTarget.Others, 20);
            
        }
        
        if (currentStress == maxStress)
        {
            HealStressplayer(reductiondestress);
            base.photonView.RPC("RPC_HealStress", RpcTarget.Others, reductiondestress);
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
        stressBar.SetStress(currentStress);
    }

    public void HealStressplayer(int amount)
    {
        if ((currentStress -= amount)<0)
        {
            currentStress = minStress;
        }

        else
        {
            currentStress -= amount;
        }
        
        stressBar.SetStress(currentStress);
    }

    [PunRPC]
    private void RPC_HealStress(int stress)
    {
        HealStressplayer(stress);
    }
    
    
    [PunRPC]
    private void RPC_TakeStress(int stress)
    {
        TakeStress(stress);
    }

}
