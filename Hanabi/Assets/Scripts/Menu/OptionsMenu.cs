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
    private AudioSource audioSource;
    private Slider Slider;

    private void Start()
    {
        audioSource = audioManager.GetComponent<AudioSource>();
        Slider = SliderGO.GetComponent<Slider>();
        

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
