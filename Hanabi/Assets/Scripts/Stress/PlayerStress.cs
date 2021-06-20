using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStress : MonoBehaviourPunCallbacks
{
    List<int> storyScenes = new List<int>{13, 14, 27, 28, 29, 42, 43, 44, 57, 58, 59, 60, 65};
    List<int> fireScenes = new List<int>{12, 26, 41, 56};
    List<int> shopScenes = new List<int>(){11, 25, 40, 55, 66};
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
    public float firecampValue = 0.6f;

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
        
        //if the room just changed and you are near a fire place then we update the stress accordingly
        if (hasChangedRoom && isTouchingFire)
        {
            currentStress = (int) ((float) currentStress * GameObject.FindGameObjectWithTag("Upgrader").GetComponent<ValueOfUpgrade>().AmeliorationFeuDeCamps);
            gameObject.GetComponent<PlayerMovement>().otherplayer.GetComponent<PlayerStress>().currentStress = currentStress;
            photonView.RPC("RPC_SetStress",RpcTarget.Others, currentStress);
            hasChangedRoom = false;
        }
        List<Items> content = new List<Items>();;
        foreach (var objet in gameObject.transform.parent.gameObject.GetComponentInChildren<InventairePassif>().content/*InventairePassif.instance.content*/)
        {
            reductiondestress += objet.StressRemoved;
            StressCD += objet.StressIntervalle;
            Pretzelcompteur.text = reductiondestress.ToString();
            if (objet.StressRemoved == 0 && objet.StressIntervalle == 0)
            {
                content.Add(objet);
            }
        }
        gameObject.transform.parent.gameObject.GetComponentInChildren<InventairePassif>().content= content;    
        
        //updates the stress each time the cd is up
        if (Time.time > StressCD && canGainStress)
        {
            photonView.RPC("RPC_TakeStress", RpcTarget.Others, 1);
            StressCD = Time.time + nextStress;
        }
        
        

        //cheat code UwU omg so cool
        if (Input.GetKeyDown(KeyCode.H))
        {
            photonView.RPC("RPC_TakeStress", RpcTarget.All, 20);
        }
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            /*TakeStress(20);
            PlayerMovement.instance.otherplayer.GetComponent<PlayerStress>().TakeStress(20);*/
            photonView.RPC("RPC_HealStress", RpcTarget.All, 20);
        }
        
        if (currentStress == maxStress)
        {
            /*HealStressplayer(reductiondestress);
            PlayerMovement.instance.otherplayer.GetComponent<PlayerStress>().HealStressplayer(reductiondestress);*/
            photonView.RPC("RPC_HealStress", RpcTarget.All, reductiondestress);
            reductiondestress = 0;
            Pretzelcompteur.text = reductiondestress.ToString();
        }
        

    }

    public bool CanStress()
    {
        return !fireScenes.Contains(SceneManager.GetActiveScene().buildIndex) && !storyScenes.Contains(SceneManager.GetActiveScene().buildIndex) && !shopScenes.Contains(SceneManager.GetActiveScene().buildIndex);
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
        {
            currentStress = minStress;
        }
        else
        {
            currentStress -= amount;
        }
        
        stressBar.SetStress(currentStress);
    }

    public void UpdateMaxStress(int UpdateStress)
    {
        maxStress += UpdateStress;

        if (PhotonNetwork.IsConnected)
        {
            photonView.RPC("RPC_UpdateMaxStress",RpcTarget.Others,UpdateStress);
        }
    }

    [PunRPC]
    public void RPC_UpdateMaxStress(int UpdateStress)
    {
        maxStress += UpdateStress;
    }

    [PunRPC]
    public void RPC_HealStress(int stress)
    {
        HealStressplayer(stress);
        //PlayerMovement.instance.otherplayer.GetComponent<PlayerStress>().HealStressplayer(stress);
    }
    
    
    [PunRPC]
    public void RPC_TakeStress(int stress)
    {
        TakeStress(stress);
        //PlayerMovement.instance.otherplayer.GetComponent<PlayerStress>().TakeStress(stress);
    }

    [PunRPC]
    public void RPC_SetStress(int stress)
    {
        currentStress = stress;
        gameObject.GetComponent<PlayerMovement>().otherplayer.GetComponent<PlayerStress>().currentStress = stress;
    }

}
