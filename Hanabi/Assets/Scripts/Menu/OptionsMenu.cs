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
    public bool founded = false;

    private void Start()
    {
       
        //audioSource = audioManager.GetComponent<AudioSource>();
        Slider = SliderGO.GetComponent<Slider>();
    }

    private void Update()
    {
        if (!founded)
        {
            audioSource = FindObjectOfType<AudioSource>();
            Debug.Log(audioSource.ToString());
            if (audioSource != null)
                founded = true;
        }
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
