using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Photon.PunBehaviour
{

        void Awake()
        {
       
            if (instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            instance = this;
        PhotonNetwork.automaticallySyncScene = true;
    }

        void Start()
        {
        PhotonNetwork.ConnectUsingSettings("Version_1.1");
        }
            DestroyImmediate(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("Version_1.0");
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void ChooseRoom()
    {
       
        PhotonNetwork.LoadLevel("ChooseRoom");
    }

    void OnLevelWasLoaded(int levelNumber)
    {
        if (!PhotonNetwork.inRoom) return;
        if(levelToLoad.Equals("Playground") && PhotonNetwork.isMasterClient)
        {
            ArenaManager.instance.photonView.RPC("CreateArena", PhotonTargets.AllBufferedViaServer, nbPlayers);
        }
    }
}

