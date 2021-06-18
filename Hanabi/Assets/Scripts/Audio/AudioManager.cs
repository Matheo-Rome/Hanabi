using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{ 
    public AudioClip[] playlist; 
    public AudioSource audioSource;
    private int musicIndex = 0;
    public AudioDistortionFilter _audioDistortionFilter;
    public GameObject Blur1;
    public GameObject Blur2;
    private bool InPuzzleRoom = false;
    private int ActualIndex;
    
    /*void Start()
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
*/

    
    //Menu principal
    
    void Start()
    {
        ActualIndex = 0;
        musicIndex = 0;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
        int mid = PlayerStressSolo.instance.maxStress / 2;
        int third = (3 * PlayerStressSolo.instance.maxStress) / 4;
        if (!PhotonNetwork.IsConnected)
        {
            if (PlayerStressSolo.instance.currentStress >= mid  && PlayerStressSolo.instance.currentStress < third)
            {
                _audioDistortionFilter.distortionLevel = (float)0.70;
                Blur2.SetActive(false);
                Blur1.SetActive(true);
            }
            else if(PlayerStressSolo.instance.currentStress < mid)
            {
                _audioDistortionFilter.distortionLevel = (float)0.5;
                Blur2.SetActive(false);
                Blur1.SetActive(false);
            }
            else if(PlayerStressSolo.instance.currentStress >= third)
            {
                _audioDistortionFilter.distortionLevel = (float)0.85;
                Blur2.SetActive(true);
                Blur1.SetActive(false);
            }  
        }
        else
        {
            if (PlayerStress.instance.currentStress >= mid && PlayerStress.instance.currentStress < third)
            {
                _audioDistortionFilter.distortionLevel = (float) 0.70;
                Blur2.SetActive(false);
                Blur1.SetActive(true);
            }
            else if (PlayerStress.instance.currentStress < mid)
            {
                _audioDistortionFilter.distortionLevel = (float) 0.5;
                Blur2.SetActive(false);
                Blur1.SetActive(false);
            }
            else if (PlayerStress.instance.currentStress >= third)
            {
                _audioDistortionFilter.distortionLevel = (float) 0.85;
                Blur2.SetActive(true);
                Blur1.SetActive(false);
            }
        }
    }

   
    void Update()   
    {
        if (HasChangedRoom())
        {
            ActualIndex = SceneManager.GetActiveScene().buildIndex;
            NewSong();
        }

        if (!PhotonNetwork.IsConnected)
        {
            int mid = PlayerStressSolo.instance.maxStress / 2;
            int third = (3 * PlayerStressSolo.instance.maxStress) / 4;
            if (!audioSource.isPlaying)
            {
                RepeatTheSong();
                if(PlayerStressSolo.instance.currentStress >= mid && PlayerStressSolo.instance.currentStress < third)
                {
                    _audioDistortionFilter.distortionLevel = (float)0.70;
                    Blur2.SetActive(false);
                    Blur1.SetActive(true);
                }
                else if(PlayerStressSolo.instance.currentStress < mid)
                {
                    _audioDistortionFilter.distortionLevel = (float)0.5;
                    Blur2.SetActive(false);
                    Blur1.SetActive(false);
                }
                else if(PlayerStressSolo.instance.currentStress >= third)
                {
                    _audioDistortionFilter.distortionLevel = (float)0.85;
                    Blur2.SetActive(true);
                    Blur1.SetActive(false);
                }
            }
            if(PlayerStressSolo.instance.currentStress >= mid && PlayerStressSolo.instance.currentStress < third)
            {
                _audioDistortionFilter.distortionLevel = (float)0.70;
                Blur2.SetActive(false);
                Blur1.SetActive(true);
            }
            if(PlayerStressSolo.instance.currentStress < mid)
            {
                _audioDistortionFilter.distortionLevel = (float)0.5;
                Blur2.SetActive(false);
                Blur1.SetActive(false);
            }
            if(PlayerStressSolo.instance.currentStress >= third)
            {
                _audioDistortionFilter.distortionLevel = (float)0.85;
                Blur2.SetActive(true);
                Blur1.SetActive(false);
            }
        }
        else
        {int mid = PlayerStress.instance.maxStress / 2;
            int third = (3 * PlayerStress.instance.maxStress) / 4;
            if (!audioSource.isPlaying)
            {
                RepeatTheSong();
                if (PlayerStress.instance.currentStress >= mid && PlayerStress.instance.currentStress < third)
                {
                    _audioDistortionFilter.distortionLevel = (float) 0.70;
                    Blur2.SetActive(false);
                    Blur1.SetActive(true);
                }
                else if (PlayerStress.instance.currentStress < mid)
                {
                    _audioDistortionFilter.distortionLevel = (float) 0.5;
                    Blur2.SetActive(false);
                    Blur1.SetActive(false);
                }
                else if (PlayerStress.instance.currentStress >= third)
                {
                    _audioDistortionFilter.distortionLevel = (float) 0.85;
                    Blur2.SetActive(true);
                    Blur1.SetActive(false);
                }
            }

            if (PlayerStress.instance.currentStress >= mid && PlayerStress.instance.currentStress < third)
            {
                _audioDistortionFilter.distortionLevel = (float) 0.70;
                Blur2.SetActive(false);
                Blur1.SetActive(true);
            }

            if (PlayerStress.instance.currentStress < mid)
            {
                _audioDistortionFilter.distortionLevel = (float) 0.5;
                Blur2.SetActive(false);
                Blur1.SetActive(false);
            }

            if (PlayerStress.instance.currentStress >= third)
            {
                _audioDistortionFilter.distortionLevel = (float) 0.85;
                Blur2.SetActive(true);
                Blur1.SetActive(false);
            }
        }
    }

    void NewSong()
    {
        ActualIndex = SceneManager.GetActiveScene().buildIndex;
        //si il faut changer de musique
        if (IndexSong() != 15)
        {
            musicIndex = IndexSong();
            audioSource.clip = playlist[musicIndex];
            audioSource.Play();
            if (!PhotonNetwork.IsConnected)
            {
                int mid = PlayerStressSolo.instance.maxStress / 2;
                int third = (3 * PlayerStressSolo.instance.maxStress) / 4;
                if (PlayerStressSolo.instance.currentStress >= mid && PlayerStressSolo.instance.currentStress < third)
                {
                    _audioDistortionFilter.distortionLevel = (float) 0.70;
                    Blur2.SetActive(false);
                    Blur1.SetActive(true);
                }

                if (PlayerStressSolo.instance.currentStress < mid)
                {
                    _audioDistortionFilter.distortionLevel = (float) 0.5;
                    Blur2.SetActive(false);
                    Blur1.SetActive(false);
                }

                if (PlayerStressSolo.instance.currentStress >= third)
                {
                    _audioDistortionFilter.distortionLevel = (float) 0.85;
                    Blur2.SetActive(true);
                    Blur1.SetActive(false);
                } 
            }
            else
            {
                int mid = PlayerStress.instance.maxStress / 2;
                int third = (3 * PlayerStress.instance.maxStress) / 4;
                if (PlayerStress.instance.currentStress >= mid && PlayerStress.instance.currentStress < third)
                {
                    _audioDistortionFilter.distortionLevel = (float) 0.70;
                    Blur2.SetActive(false);
                    Blur1.SetActive(true);
                }

                if (PlayerStress.instance.currentStress < mid)
                {
                    _audioDistortionFilter.distortionLevel = (float) 0.5;
                    Blur2.SetActive(false);
                    Blur1.SetActive(false);
                }

                if (PlayerStress.instance.currentStress >= third)
                {
                    _audioDistortionFilter.distortionLevel = (float) 0.85;
                    Blur2.SetActive(true);
                    Blur1.SetActive(false);
                }
            }
        }
    }
    void RepeatTheSong()
    {
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
        if (!PhotonNetwork.IsConnected)
        {
            int mid = PlayerStressSolo.instance.maxStress / 2;
            int third = (3 * PlayerStressSolo.instance.maxStress) / 4;
            if (PlayerStressSolo.instance.currentStress >= mid && PlayerStress.instance.currentStress < third)
            {
                _audioDistortionFilter.distortionLevel = (float) 0.70;
                Blur2.SetActive(false);
                Blur1.SetActive(true);
            }

            if (PlayerStressSolo.instance.currentStress < mid)
            {
                _audioDistortionFilter.distortionLevel = (float) 0.5;
                Blur2.SetActive(false);
                Blur1.SetActive(false);
            }

            if (PlayerStressSolo.instance.currentStress >= third)
            {
                _audioDistortionFilter.distortionLevel = (float) 0.85;
                Blur2.SetActive(true);
                Blur1.SetActive(false);
            }
        }
        else
        {
            int mid = PlayerStress.instance.maxStress / 2;
            int third = (3 * PlayerStress.instance.maxStress) / 4;
            if (PlayerStress.instance.currentStress >= mid && PlayerStress.instance.currentStress < third)
            {
                _audioDistortionFilter.distortionLevel = (float) 0.70;
                Blur2.SetActive(false);
                Blur1.SetActive(true);
            }

            if (PlayerStress.instance.currentStress < mid)
            {
                _audioDistortionFilter.distortionLevel = (float) 0.5;
                Blur2.SetActive(false);
                Blur1.SetActive(false);
            }

            if (PlayerStress.instance.currentStress >= third)
            {
                _audioDistortionFilter.distortionLevel = (float) 0.85;
                Blur2.SetActive(true);
                Blur1.SetActive(false);
            }
        }
    }

    bool HasChangedRoom()
    {
        return ActualIndex != SceneManager.GetActiveScene().buildIndex;
    }
    
    int IndexSong()
    {
        //menu principal
        int index = 0;
        
        //première zone
        if (0 < SceneManager.GetActiveScene().buildIndex && SceneManager.GetActiveScene().buildIndex < 15) 
            index = IndexSongZ1();
        
            
        //deuxième zone
        if (14 < SceneManager.GetActiveScene().buildIndex && SceneManager.GetActiveScene().buildIndex < 30)
            index = IndexSongZ2();
        
        
        //troisième zone
        if (29 < SceneManager.GetActiveScene().buildIndex && SceneManager.GetActiveScene().buildIndex < 45)
            index = IndexSongZ3();
        
            
        //quatrième zone
        if (44 < SceneManager.GetActiveScene().buildIndex && SceneManager.GetActiveScene().buildIndex < 60)
            index = IndexSongZ4();
        
        //actuellement dans une salle défi
        if (SceneManager.GetActiveScene().buildIndex == 61 || SceneManager.GetActiveScene().buildIndex == 62 ||
            SceneManager.GetActiveScene().buildIndex == 63 || SceneManager.GetActiveScene().buildIndex == 64)
            index = SalleDefi();

        return index;
    }

    int IndexSongZ1()
    {
        //lance une première fois la musique de la première zone à jump 1.1
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            InPuzzleRoom = true;
            return 1;
        }
        
        //salle histoire
        if (SceneManager.GetActiveScene().buildIndex == 13)
        {
            InPuzzleRoom = false;
            return 5;
        }
        
        //cas où il ne faut pas modifier la musique 
        return 15;
    }

    int IndexSongZ2()
    {
        //lance une première fois la musique de la deuxième zone à jump 2.1
        if (SceneManager.GetActiveScene().buildIndex == 15)
        {
            InPuzzleRoom = true;
            return 2;
        }
        
        //salle histoire
        if (SceneManager.GetActiveScene().buildIndex == 27 ||
            SceneManager.GetActiveScene().buildIndex == 28 || SceneManager.GetActiveScene().buildIndex == 29)
        {
            InPuzzleRoom = false;
            return 5;
            
        }
        if (!InPuzzleRoom)
        {
            InPuzzleRoom = true;
            return 2;
        }
        
        //cas où il ne faut pas modifier la musique
        return 15;
        
    }
    
    int IndexSongZ3()
    {
        //lance une première fois la musique de la deuxième zone à jump 3.1
        if (SceneManager.GetActiveScene().buildIndex == 30)
        {
            InPuzzleRoom = true;
            return 3;
        }
        
        //salle histoire
        if (SceneManager.GetActiveScene().buildIndex == 42 || SceneManager.GetActiveScene().buildIndex == 43 || 
            SceneManager.GetActiveScene().buildIndex == 44)
        {
            InPuzzleRoom = false;
            return 5;
            
        }
        if (!InPuzzleRoom)
        {
            InPuzzleRoom = true;
            return 3;
        }
        
        //cas où il ne faut pas modifier la musique
        return 15;
        
    }
    
    int IndexSongZ4()
    {
        //lance une première fois la musique de la deuxième zone à jump 4.1
        if (SceneManager.GetActiveScene().buildIndex == 45)
        {
            InPuzzleRoom = true;
            return 4;
        }
        
        //salle histoire
        if (SceneManager.GetActiveScene().buildIndex == 57 || SceneManager.GetActiveScene().buildIndex == 58 || 
            SceneManager.GetActiveScene().buildIndex == 59)
        {
            InPuzzleRoom = false;
            return 5;
            
        }
        if (!InPuzzleRoom)
        {
            InPuzzleRoom = true;
            return 4;
        }
        
        //cas où il ne faut pas modifier la musique
        return 15;
        
    }

    int SalleDefi()
    {
        InPuzzleRoom = false;
        return 6;
    }
    
    
        
    
}
    

