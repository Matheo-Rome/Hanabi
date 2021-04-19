using Cinemachine;
using UnityEngine;

public class AudioManager : MonoBehaviour
{ 
    public AudioClip[] playlist; 
    public AudioSource audioSource;
    private int musicIndex = 0;
    public AudioDistortionFilter _audioDistortionFilter;
    void Start()
    {
        audioSource.clip = playlist[0];
        audioSource.Play();
        if(PlayerStress.instance.currentStress >= 100)
        {
            _audioDistortionFilter.distortionLevel = (float)0.85;
            RenderSettings.ambientLight = Color.black;
        }
        if(PlayerStress.instance.currentStress < 100)
        {
            _audioDistortionFilter.distortionLevel = (float)0.5;
        }
    }

    void Update()   
    {
        if (!audioSource.isPlaying)
        {
            PlayNextSong();
            if(PlayerStress.instance.currentStress >= 100)
            {
                _audioDistortionFilter.distortionLevel = (float)1;
            }
        }
        if(PlayerStress.instance.currentStress >= 100)
        {
            _audioDistortionFilter.distortionLevel = (float)0.85;
            RenderSettings.ambientLight = Color.black;
        }
        if(PlayerStress.instance.currentStress < 100)
        {
            _audioDistortionFilter.distortionLevel = (float)0.5;
        }
    }

    void PlayNextSong()
    {
        musicIndex = (musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
        if(PlayerStress.instance.currentStress >= 100)
        {
            _audioDistortionFilter.distortionLevel = (float)0.85;
            RenderSettings.ambientLight = Color.black;
        }
        if(PlayerStress.instance.currentStress < 100)
        {
            _audioDistortionFilter.distortionLevel = (float)0.5;
        }
    }
    
}
