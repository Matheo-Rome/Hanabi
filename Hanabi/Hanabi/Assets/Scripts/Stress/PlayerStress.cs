using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerStress : MonoBehaviour
{
    public int minStress = 0;
    public int currentStress;
    public int maxStress = 200;
    public int reductiondestress = 0;
    public Text Pretzelcompteur;
    public float nextStress = 2f;
    public float StressCD;
    

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
        if (Time.time > StressCD)
        {
           TakeStress(1);
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

        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeStress(20);
            if (currentStress > 200)
            {
                currentStress = 200;
            }
            
        }
        
        if (currentStress == maxStress)
        {
            HealStressplayer(reductiondestress);
            reductiondestress = 0;
            Pretzelcompteur.text = reductiondestress.ToString();
            
        }

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
}
