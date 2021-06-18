using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject SliderGO;
    [SerializeField] private AudioManager audioManager;
    public AudioSource audioSource;
    public Slider Slider;


    private void Update()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioSource = audioManager.GetComponent<AudioSource>();
       // Slider = SliderGO.GetComponent<Slider>();
    }
    

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
