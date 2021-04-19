using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnimation : MonoBehaviour
{
    private int stress;
    public Animator heartAnimation;
        
    void Start()
    {
        stress = 0;
    }

    
    void Update()
    {
        stress = PlayerStress.instance.currentStress;
        heartAnimation.SetInteger("Stress", stress);
    }
}
