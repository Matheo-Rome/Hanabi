using UnityEngine;

public class PlayerStress : MonoBehaviour
{
    public int minStress = 0;
    public int currentStress;

    public StressBar stressBar;
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
}
