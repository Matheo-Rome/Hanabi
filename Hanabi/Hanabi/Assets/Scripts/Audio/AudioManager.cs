using Cinemachine;
using UnityEngine;

public class AudioManager : MonoBehaviour
{ 
    public AudioClip[] playlist; 
    public AudioSource audioSource;
    private int musicIndex = 0;
    public AudioDistortionFilter _audioDistortionFilter;
    public GameObject Blur1;
    public GameObject Blur2;
    
    void Start()
    {
        audioSource.clip = playlist[0];
        audioSource.Play();
        if(PlayerStress.instance.currentStress >= 100 && PlayerStress.instance.currentStress < 150)
        {
            _audioDistortionFilter.distortionLevel = (float)0.85;
            Blur2.SetActive(false);
            Blur1.SetActive(true);
        }
        else if(PlayerStress.instance.currentStress < 100)
        {
            _audioDistortionFilter.distortionLevel = (float)0.5;
            Blur2.SetActive(false);
            Blur1.SetActive(false);
        }
        else if(PlayerStress.instance.currentStress >= 150)
        {
            _audioDistortionFilter.distortionLevel = (float)0.85;
            Blur2.SetActive(true);
            Blur1.SetActive(false);
        }
    }

    void Update()   
    {
        if (!audioSource.isPlaying)
        {
            PlayNextSong();
            if(PlayerStress.instance.currentStress >= 100 && PlayerStress.instance.currentStress < 150)
            {
                _audioDistortionFilter.distortionLevel = (float)1;
                Blur2.SetActive(false);
                Blur1.SetActive(true);
            }
            else if(PlayerStress.instance.currentStress < 100)
            {
                _audioDistortionFilter.distortionLevel = (float)0.5;
                Blur2.SetActive(false);
                Blur1.SetActive(false);
            }
            else if(PlayerStress.instance.currentStress >= 150)
            {
                _audioDistortionFilter.distortionLevel = (float)0.85;
                Blur2.SetActive(true);
                Blur1.SetActive(false);
            }
        }
        if(PlayerStress.instance.currentStress >= 100 && PlayerStress.instance.currentStress < 150)
        {
            _audioDistortionFilter.distortionLevel = (float)0.85;
            Blur2.SetActive(false);
            Blur1.SetActive(true);
        }
        if(PlayerStress.instance.currentStress < 100)
        {
            _audioDistortionFilter.distortionLevel = (float)0.5;
            Blur2.SetActive(false);
            Blur1.SetActive(false);
        }
        if(PlayerStress.instance.currentStress >= 150)
        {
            _audioDistortionFilter.distortionLevel = (float)0.85;
            Blur2.SetActive(true);
            Blur1.SetActive(false);
        }
    }

    void PlayNextSong()
    {
        musicIndex = (musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
        if(PlayerStress.instance.currentStress >= 100 && PlayerStress.instance.currentStress < 150)
        {
            _audioDistortionFilter.distortionLevel = (float)0.85;
            Blur2.SetActive(false);
            Blur1.SetActive(true);
        }
        if(PlayerStress.instance.currentStress < 100)
        {
            _audioDistortionFilter.distortionLevel = (float)0.5;
            Blur2.SetActive(false);
            Blur1.SetActive(false);
        }
        if(PlayerStress.instance.currentStress >= 150)
        {
            _audioDistortionFilter.distortionLevel = (float)0.85;
            Blur2.SetActive(true);
            Blur1.SetActive(false);
        }
    }
    
}
