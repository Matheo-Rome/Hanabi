using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviourPunCallbacks
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUi;
    public GameObject optionsUi;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        optionsUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUi.SetActive(true);
        //Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        if (PhotonNetwork.IsConnected)
        {
            photonView.RPC("RPC_ReturnToMenu", RpcTarget.Others);
            StartCoroutine(Disconnect());
        }
        pauseMenuUi.SetActive(false);
        optionsUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene(0);
        List<DDOL> toDestroy = GameObject.FindObjectsOfType<DDOL>().ToList();
        toDestroy.ForEach(x => Destroy(x.gameObject));
    }

    [PunRPC]
    public void RPC_ReturnToMenu()
    {
        StartCoroutine(Disconnect());
        pauseMenuUi.SetActive(false);
        optionsUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene(0);
        List<DDOL> toDestroy = GameObject.FindObjectsOfType<DDOL>().ToList();
        toDestroy.ForEach(x => Destroy(x.gameObject));
    }

    public void QuitGame()
    {
        if (PhotonNetwork.IsConnected)
        {
            photonView.RPC("RPC_ReturnToMenu", RpcTarget.Others);
            PhotonNetwork.Disconnect();
        }
        Application.Quit();
        pauseMenuUi.SetActive(false);
        optionsUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene(0);
    }
    
    IEnumerator Disconnect()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
    }
}
