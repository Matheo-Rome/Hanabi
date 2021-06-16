using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPackageTurnOn : MonoBehaviour
{
    [SerializeField] private GameObject sound;

     void Update()
     {
        if(Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Jump"))
            sound.SetActive(true);
    }
}
