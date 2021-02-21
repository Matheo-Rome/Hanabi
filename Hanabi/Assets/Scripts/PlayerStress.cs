using UnityEngine;

public class PlayerStress : MonoBehaviour
{
    public int minStress = 0;
    public int currentStress;

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
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeStress(20);
        }
    }

    void TakeStress(int addstress)
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
